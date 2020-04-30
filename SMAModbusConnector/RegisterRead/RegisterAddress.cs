namespace SMAModbusConnector.RegisterRead
{
    public class RegisterAddress
    {
        public ushort Register { get; }
        public DataType DataType { get; }
        public RegisterDescription[] Descriptions { get; }

        public RegisterAddress(ushort register, DataType dataType, params RegisterDescription[] descriptions)
        {
            Register = register;
            DataType = dataType;
            Descriptions = descriptions;
        }
    }
}
