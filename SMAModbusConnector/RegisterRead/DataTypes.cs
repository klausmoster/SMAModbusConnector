namespace SMAModbusConnector.RegisterRead
{
    public enum DataType
    {
        S32,
        U32
    }

    public abstract class ResultTypeBase<TResultType> where TResultType : struct
    {
        public DataType DataType { get; }
        public TResultType Result { get; set; }

        public ResultTypeBase(DataType dataType)
        {
            DataType = dataType;
        }
    }

    public class U32 : ResultTypeBase<uint>
    {
        public U32() : base(DataType.U32)
        {
        }
    }

    public class S32 : ResultTypeBase<int>
    {
        public S32() : base(DataType.S32)
        {
        }
    }
}
