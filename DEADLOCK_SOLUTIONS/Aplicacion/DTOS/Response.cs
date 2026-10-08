

namespace DEADLOCK_SOLUTIONS.Aplicacion.DTOS
{
    public class Response<T>
    {
        private Response(T? data, int status, string message, bool isSuccess)
        {
            Data = data;
            Status = status;
            Message = message;
            IsSuccess = isSuccess;
        }

        public T? Data { get; private set; }
        public int Status { get; private set; }
        public string Message { get; private set; }
        public bool IsSuccess {  get; private set; }

        public static Response<T> Error(string message = "Error", int status = 500) 
        {
            return new Response<T>(default, status, message, false);
        }

        public static Response<T> Success( T data, string message = "Success", int status = 200)
        {
            return new Response<T>(data, status, message, false);
        }

    }
}
