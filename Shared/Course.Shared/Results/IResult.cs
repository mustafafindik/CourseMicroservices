namespace Course.Shared.Results
{

    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
