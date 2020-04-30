using System.Net;
using FluentModbus;

namespace SMAModbusConnector.ModbusConnection
{
    internal class ModbusConnection : IModbusConnection
    {
        private readonly ModbusTcpClient _connection;

        public ModbusConnection()
        {
            _connection = new ModbusTcpClient();
        }

        public void Connect(IPAddress address)
        {
            _connection.Connect(address);
        }

        public byte[] Read(byte unit, ushort register)
        {
            return _connection.ReadHoldingRegisters<byte>(unit, register, 4).ToArray();
        }
    }
}
