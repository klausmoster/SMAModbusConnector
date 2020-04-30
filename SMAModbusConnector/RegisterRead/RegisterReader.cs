using System;
using SMAModbusConnector.ModbusConnection;

namespace SMAModbusConnector.RegisterRead
{
    internal class RegisterReader
    {
        private readonly IModbusConnection _modbusConnection;

        public RegisterReader(IModbusConnection modbusConnection)
        {
            _modbusConnection = modbusConnection;
        }

        public int ReadS32(byte unit, ushort register)
        {
            var buf = _modbusConnection.Read(unit, register);
            return buf[0] << 24 | buf[1] << 16 | buf[2] << 8 | buf[3];
        }

        public uint ReadU32(byte unit, ushort register)
        {
            var buf = _modbusConnection.Read(unit, register);
            return (UInt32) (buf[0] << 24 | buf[1] << 16 | buf[2] << 8 | buf[3]);
            //var val2 = BitConverter.ToUInt32(buf.ToArray().Reverse().ToArray(), 0);
        }
    }
}
