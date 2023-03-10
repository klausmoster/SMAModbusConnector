using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SMAModbusConnector.ModbusConnection;
using SMAModbusConnector.RegisterRead;

namespace SMAModbusConnector.Tests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class When_reading_calculated_values
    {
        [Test]
        public void The_register_reader_reads_U32()
        {
            var bytes = new Span<byte>(new byte[]
            {
                0, 0, 1, 187
            }).ToArray();
        
            var connection = Substitute.For<IModbusConnection>();
            connection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);
        
            var reader = new RegisterReader(connection);
            var result = reader.ReadU32(1, 12345);
        
            result.Should().Be(443);
        }
        
        [Test]
        public void The_register_reader_reads_S32()
        {
            var bytes = new byte[]
            {
                0, 0, 6, 77
            }.ToArray();
        
            var connection = Substitute.For<IModbusConnection>();
            connection.Read(Arg.Any<byte>(), Arg.Any<ushort>()).Returns(bytes);
        
            var reader = new RegisterReader(connection);
            var result = reader.ReadU32(1, 12345);
        
            result.Should().Be(1613);
        }
    }
}
