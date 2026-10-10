namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS;

public class Response<T>
{
    private Response(T? data, int status, string message, bool isSuccess)
    {
        Data = data;
        Status = status;
        Message = message;
        IsSuccess = isSuccess;
    }

    public T? Data { get; }

    public int Status { get; }

    public string Message { get; }

    public bool IsSuccess { get; }

    public static Response<T> Error(string message = "Error", int status = StatusCodes.Status500InternalServerError)
    {
        return new Response<T>(default, status, message, false);
    }

    public static Response<T> Success(T data, string message = "Success", int status = StatusCodes.Status200OK)
    {
        return new Response<T>(data, status, message, true);
    }
}
