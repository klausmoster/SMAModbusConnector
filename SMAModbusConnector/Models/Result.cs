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

        public Result(Guid id, RegisterAddress registerAddress, int value)
        {
            Id = id;
            RegisterAddress = registerAddress;
            Value = value;
        }

        public override string ToString()
        {
            return
                $"Register: {RegisterAddress.Register}{Environment.NewLine}Description: {RegisterAddress.Descriptions.First().Description}{Environment.NewLine}Value: {Value}";
        }
    }
}
