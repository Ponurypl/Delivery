namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public sealed class Result<TValue> : Result
{
    private readonly TValue _value; 
    
    public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    internal Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);
}
