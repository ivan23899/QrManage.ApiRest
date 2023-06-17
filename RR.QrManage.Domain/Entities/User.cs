namespace RR.QrManage.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string PictureProfile { get; set; }
        public string CreationUser { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool State { get; set; }
        public User()
        {
            Id = 0;
            Name = string.Empty;
            Alias = string.Empty;
            PictureProfile = string.Empty;
            CreationUser = string.Empty;
            ModificationUser = string.Empty;
            State = false;
        }
    }
}
