using System;
using System.Net;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SMAModbusConnector.Exceptions;
using SMAModbusConnector.ModbusConnection;
using SMAModbusConnector.RegisterRead;

namespace SMAModbusConnector.Tests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class When_reading_the_value_of_an_register
    {
        private readonly RegisterAddress _registerAddress =
            new RegisterAddress(12345, DataType.S32, new RegisterDescription("en", "Description"));

        [Test]
        public void A_device_must_be_registered()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();

            Assert.Throws<DeviceNotFoundException>(() =>
                connector.GetDataForAddress(Guid.NewGuid(), _registerAddress));
        }

        [Test]
        public void The_register_needs_at_least_one_description()
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
            connector.TryRegisterDevice(1, IPAddress.Any, out var id);

            Assert.Throws<MissingDescriptionAttributeException>(() =>
                connector.GetDataForAddress(id, new RegisterAddress(12345, DataType.S32)));

            Assert.DoesNotThrow(() =>
                connector.GetDataForAddress(id, _registerAddress));
        }

        [Test]
        public void The_result_contains_all_data()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(3, 12345).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();

            connector.TryRegisterDevice(3, IPAddress.Parse("192.168.1.1"), out var id);

            var result = connector.GetDataForAddress(id, _registerAddress);
            result.Id.Should().Be(id);
            result.RegisterAddress.Should().Be(_registerAddress);
            result.Value.Should().Be(443);
        }
    }
}
