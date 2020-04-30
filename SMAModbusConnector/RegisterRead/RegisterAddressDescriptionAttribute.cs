using System;

namespace SMAModbusConnector.RegisterRead
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RegisterAddressDescriptionAttribute : Attribute
    {
        public string Language { get; }
        public string Description { get; }

        public RegisterAddressDescriptionAttribute(string language, string description)
        {
            Language = language;
            Description = description;
        }
    }
}
