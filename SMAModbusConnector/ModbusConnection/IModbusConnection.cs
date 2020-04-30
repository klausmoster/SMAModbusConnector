using System.Net;

namespace SMAModbusConnector.ModbusConnection
{
    internal interface IModbusConnection
    {
        void Connect(IPAddress address);
        byte[] Read(byte unit, ushort register);
    }
}
