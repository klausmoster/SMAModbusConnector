using System;
using System.Linq;
using SMAModbusConnector.RegisterRead;

namespace SMAModbusConnector.Models
{
    public class Result
    {
        public Guid Id { get; }
        public RegisterAddress RegisterAddress { get; }
        public int Value { get; }

        public string FriendlyDescription { get; private set; }

        internal Result(Guid id, RegisterAddress registerAddress, Language language, int value)
        {
            Id = id;
            RegisterAddress = registerAddress;
            Value = value;

            SetFriendlyDescription(language);
        }

        private void SetFriendlyDescription(Language language)
        {
            var description = RegisterAddress.Descriptions.FirstOrDefault(d => d.Language == language);
            if (description == null)
            {
                description = RegisterAddress.Descriptions.First();
            }

            FriendlyDescription = $"{description.Description} ({RegisterAddress.Register}) = {Value}";
        }

        public override string ToString()
        {
            return
                $"Register: {RegisterAddress.Register}{Environment.NewLine}Description: {RegisterAddress.Descriptions.First().Description}{Environment.NewLine}Value: {Value}";
        }
    }
}
