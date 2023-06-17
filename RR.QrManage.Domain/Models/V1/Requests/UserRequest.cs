namespace RR.QrManage.Domain.Models.V1.Requests
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public string PictureProfile { get; set; }
        public UserRequest()
        {
            Name = string.Empty;
            Alias = string.Empty;
            PictureProfile = string.Empty;
        }
    }
}
