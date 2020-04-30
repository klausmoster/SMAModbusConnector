using SMAModbusConnector.Models;

namespace SMAModbusConnector
{
    public class RegisterDescription
    {
        public Language Language { get; }
        public string Description { get; }

        public RegisterDescription(Language language, string description)
        {
            Language = language;
            Description = description;
        }
    }
}
