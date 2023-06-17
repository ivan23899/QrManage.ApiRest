using System.Data;

namespace RR.QrManage.Domain.Models
{
    public class StoreProcedureInsert
    {
        public DataTable DataTable { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
        public int? Identity { get; set; }
        public StoreProcedureInsert()
        {
            DataTable = new DataTable();
            Result = 0;
            Message = string.Empty;
        }
    }
}
