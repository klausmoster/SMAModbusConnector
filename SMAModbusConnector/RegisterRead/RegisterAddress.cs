namespace SMAModbusConnector.RegisterRead
{
    public class RegisterAddress
    {
        public ushort Register { get; }
        public RegisterAddressType Type { get; }
        public RegisterAddressFormat Format { get; }
        public RegisterDescription[] Descriptions { get; }

        public RegisterAddress(ushort register, RegisterAddressType type, RegisterAddressFormat format,
            params RegisterDescription[] descriptions)
        {
            Register = register;
            Type = type;
            Format = format;
            Descriptions = descriptions;
        }
    }
}
