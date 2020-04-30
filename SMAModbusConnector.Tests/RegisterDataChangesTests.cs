using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SMAModbusConnector.Exceptions;
using SMAModbusConnector.ModbusConnection;
using SMAModbusConnector.Models;
using SMAModbusConnector.RegisterRead;

namespace SMAModbusConnector.Tests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class After_starting_the_listener_for_data_changes
    {
        private readonly RegisterAddress _register =
            new RegisterAddress(0, RegisterAddressType.S32, new RegisterDescription(Language.English, "Description 1"));

        private readonly RegisterAddress _register2 =
            new RegisterAddress(1, RegisterAddressType.U32, new RegisterDescription(Language.English, "Description 2"));

        [Test]
        public void An_exception_throws_when_no_register_address_is_added_yet()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();

            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out _);

            Assert.Throws<NoRegisterAddressesAddedException>(() => { connector.StartDataChange(null); });
        }

        [Test]
        public void An_exception_throws_when_no_device_is_added_yet()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;

            var connector = new Connector();

            Assert.Throws<NoDevicesRegisteredException>(() => { connector.StartDataChange(null); });
        }

        [Test]
        public async Task The_connector_queries_every_device_for_changes_every_second()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();

            var modbusConnection1 = Substitute.For<IModbusConnection>();
            var modbusConnection2 = Substitute.For<IModbusConnection>();

            modbusConnection1.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);
            modbusConnection2.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            var callCount = 0;
            factory.GetConnection().Returns(t =>
            {
                callCount++;
                return callCount == 1 ? modbusConnection1 : modbusConnection2;
            });

            Connector.ModbusConnectionFactory = factory;

            var connector = new Connector();
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out var id1);
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.2"), out var id2);

            connector.AddRegisterAddressForDataChanges(id1, _register);
            connector.AddRegisterAddressForDataChanges(id2, _register);

            connector.StartDataChange((deviceId, result) => { });

            await Task.Delay(5000);

            modbusConnection1.Received().Read(Arg.Any<byte>(), Arg.Any<ushort>());
            modbusConnection2.Received().Read(Arg.Any<byte>(), Arg.Any<ushort>());
        }

        [Test]
        public void The_connector_stores_the_last_change_result_for_every_device()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 8, 131, 154
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;

            var connector = new Connector();
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out var id);

            // Ensure, that the last change result is zero at start
            connector.Devices.First().Value.ModbusDataChangeCount.Should().Be(0);

            connector.AddRegisterAddressForDataChanges(id, _register);

            var mre = new ManualResetEvent(false);

            connector.StartDataChange((deviceId, result) => { });

            // Wait 2 seconds to receive at least one call
            mre.WaitOne(2000);

            connector.Devices.First().Value.ModbusDataChangeCount.Should().Be(557978);
        }

        [Test]
        public void The_connector_returns_the_result_for_every_device_and_every_register_address()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;

            var connector = new Connector();
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out var id1);
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.2"), out var id2);

            connector.AddRegisterAddressForDataChanges(id1, _register, _register2);
            connector.AddRegisterAddressForDataChanges(id2, _register, _register2);

            var mre = new ManualResetEvent(false);
            var device1Register1IsQueried = false;
            var device1Register2IsQueried = false;
            var device2Register1IsQueried = false;
            var device2Register2IsQueried = false;

            connector.StartDataChange((deviceId, result) =>
            {
                if (deviceId == id1)
                {
                    if (result.RegisterAddress == _register)
                    {
                        device1Register1IsQueried = true;
                    }

                    else if (result.RegisterAddress == _register2)
                    {
                        device1Register2IsQueried = true;
                    }
                }

                else if (deviceId == id2)
                {
                    if (result.RegisterAddress == _register)
                    {
                        device2Register1IsQueried = true;
                    }

                    else if (result.RegisterAddress == _register2)
                    {
                        device2Register2IsQueried = true;
                    }
                }

                if (device1Register1IsQueried && device1Register2IsQueried && device2Register1IsQueried &&
                    device2Register2IsQueried)
                {
                    mre.Set();
                }
            });

            mre.WaitOne(5000);

            device1Register1IsQueried.Should().BeTrue();
            device1Register2IsQueried.Should().BeTrue();
            device2Register1IsQueried.Should().BeTrue();
            device2Register2IsQueried.Should().BeTrue();
        }
    }
}
