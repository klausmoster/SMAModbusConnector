using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SMAModbusConnector.ModbusConnection;

namespace SMAModbusConnector.Tests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class When_adding_devices
    {
        [Test]
        public void A_unique_identifier_returns()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;

            var connector = new Connector();
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out var id);
            connector.TryRegisterDevice(4, IPAddress.Parse("192.168.1.2"), out var id2);

            id.Should().NotBe(Guid.Empty);
            id2.Should().NotBe(Guid.Empty);

            id.Should().NotBe(id2);
        }

        [Test]
        public void The_device_is_in_the_devicelist()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;

            var connector = new Connector();
            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out var id);
            connector.TryRegisterDevice(4, IPAddress.Parse("192.168.1.2"), out var id2);

            connector.Devices.Count.Should().Be(2);
            connector.Devices.Any(d => d.Key == id && d.Value.Unit == 3).Should().BeTrue();
            connector.Devices.Any(d => d.Key == id2 && d.Value.Unit == 4).Should().BeTrue();
        }
    }
}
