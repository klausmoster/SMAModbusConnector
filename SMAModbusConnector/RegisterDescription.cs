namespace SMAModbusConnector
{
    public class RegisterDescription
    {
        public string Language { get; }
        public string Description { get; }

        public RegisterDescription(string language, string description)
        {
            Language = language;
            Description = description;
        }
    }
}
