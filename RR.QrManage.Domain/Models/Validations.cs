namespace RR.QrManage.Domain.Models
{
    public class Validations
    {
        public enum Response
        {
            Success = 0,
            Error = 1
        };
        public static string ResponseMessages(Response message)
        {
            return message switch
            {
                Response.Success => "Successfully completed.",
                _ => "An error has occurred.",
            };
        }
    }
}
