namespace SMAModbusConnector.RegisterRead
{
    public class RegisterAddress
    {
        public ushort Register { get; }
        public RegisterAddressType RegisterAddressType { get; }
        public RegisterDescription[] Descriptions { get; }

        public RegisterAddress(ushort register, RegisterAddressType registerAddressType, params RegisterDescription[] descriptions)
        {
            Register = register;
            RegisterAddressType = registerAddressType;
            Descriptions = descriptions;
        }
    }
}
