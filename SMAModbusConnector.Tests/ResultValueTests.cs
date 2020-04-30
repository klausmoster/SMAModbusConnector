using System;
using System.Configuration;
using System.Net;
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
    public class When_parsing_the_value_of_an_register
    {
        /// <summary>
        /// 443 -> 443 as FIX0
        /// </summary>
        [Test]
        public void FIX0_is_supported()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            var address = new RegisterAddress(12345, RegisterAddressType.S32, RegisterAddressFormat.FIX0,
                new RegisterDescription(Language.English, "Description"));

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();
            connector.TryRegisterDevice(1, IPAddress.Any, out var id);

            var result = connector.GetDataForAddress(id, address);
            result.Value.GetType().Should().Be(typeof(long));
            result.Value.Should().Be(443);
        }

        /// <summary>
        /// 37725 -> 377.32 as FIX2
        /// </summary>
        [Test]
        public void FIX2_is_supported()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 147, 100
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            var address = new RegisterAddress(12345, RegisterAddressType.S32, RegisterAddressFormat.FIX2,
                new RegisterDescription(Language.English, "Description"));

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();
            connector.TryRegisterDevice(1, IPAddress.Any, out var id);

            var result = connector.GetDataForAddress(id, address);
            result.Value.GetType().Should().Be(typeof(double));
            result.Value.Should().Be(377.32);
        }

        /// <summary>
        /// 443 -> 44.3 as TEMP
        /// </summary>
        [Test]
        public void TEMP_is_supported()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            var address = new RegisterAddress(12345, RegisterAddressType.S32, RegisterAddressFormat.TEMP,
                new RegisterDescription(Language.English, "Description"));

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();
            connector.TryRegisterDevice(1, IPAddress.Any, out var id);

            var result = connector.GetDataForAddress(id, address);
            result.Value.GetType().Should().Be(typeof(double));
            result.Value.Should().Be(44.3);
        }

        /// <summary>
        /// 443 -> 443 as RAW
        /// </summary>
        [Test]
        public void RAW_is_supported()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();

            var modbusConnection = Substitute.For<IModbusConnection>();
            modbusConnection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);

            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            var address = new RegisterAddress(12345, RegisterAddressType.S32, RegisterAddressFormat.RAW,
                new RegisterDescription(Language.English, "Description"));

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();
            connector.TryRegisterDevice(1, IPAddress.Any, out var id);

            var result = connector.GetDataForAddress(id, address);
            result.Value.GetType().Should().Be(typeof(long));
            result.Value.Should().Be(443);
        }
    }
}
