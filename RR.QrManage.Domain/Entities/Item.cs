namespace RR.QrManage.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string QrCode { get; set; }
        public string PhysicalCode { get; set; }
        public string ModelCode { get; set; }
        public string Description { get; set; }
        public string PictureItem { get; set; }
        public DateTime DateAcquisition { get; set; }
        public string AccountingNumber { get; set; }
        public string CodeAssignedArea { get; set; }
        public string AssignedArea { get; set; }
        public string CodeResponsible { get; set; }
        public string Responsible { get; set; }
        public DateTime AssignedDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal CostPrice { get; set; }
        public string ActualState { get; set; }
        public decimal DepreciationPercentage { get; set; }
        public string CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool State { get; set; }
        public Item()
        {
            QrCode = string.Empty;
            PhysicalCode = string.Empty;
            ModelCode = string.Empty;
            Description = string.Empty;
            PictureItem = string.Empty;
            AccountingNumber = string.Empty;
            CodeAssignedArea = string.Empty;
            AssignedArea = string.Empty;
            CodeResponsible = string.Empty;
            Responsible = string.Empty;
            ActualState = string.Empty;
            CreationUser = string.Empty;
            ModificationUser = string.Empty;
        }
    }
}
