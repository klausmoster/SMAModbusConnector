namespace SMAModbusConnector.ModbusConnection
{
    internal interface IModbusConnectionFactory
    {
        IModbusConnection GetConnection();
    }
}
