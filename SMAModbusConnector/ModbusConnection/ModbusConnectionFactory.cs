namespace SMAModbusConnector.ModbusConnection
{
    internal class ModbusConnectionFactory : IModbusConnectionFactory
    {
        public IModbusConnection GetConnection()
        {
            return new ModbusConnection();
        }
    }
}
