namespace RR.QrManage.Domain.Models
{
    public class Response<T>
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public static Response<T> Error(string message)
        {
            Response<T> response = new()
            {
                Code = ((int)Validations.Response.Error).ToString().PadLeft(2,'0'),
                Message = message
            };
            return response;
        }
        public static Response<T> Error(string code, string message)
        {
            Response<T> response = new()
            {
                Code = code,
                Message = message
            };
            return response;
        }
        public static Response<T> Success(string message, T data)
        {
            Response<T> response = new()
            {
                Code = ((int)Validations.Response.Success).ToString().PadLeft(2, '0'),
                Message = message,
                Data = data
            };
            return response;
        }
        public static Response<T> Success(T data)
        {
            Response<T> response = new()
            {
                Code = ((int)Validations.Response.Success).ToString().PadLeft(2, '0'),
                Message = Validations.ResponseMessages(Validations.Response.Success),
                Data = data
            };
            return response;
        }
    }
}
