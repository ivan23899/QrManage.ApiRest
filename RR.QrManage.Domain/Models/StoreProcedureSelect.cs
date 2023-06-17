using System.Data;

namespace RR.QrManage.Domain.Models
{
    public class StoreProcedureSelect
    {
        public DataTable DataTable { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
        public StoreProcedureSelect()
        {
            DataTable = new DataTable();
            Result = 0;
            Message = string.Empty;
        }

    }
}
