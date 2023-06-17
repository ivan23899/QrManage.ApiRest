namespace RR.QrManage.DataAccess
{
    public class Connection
    {
        public static string SqlUser(string server, string db, string user, string password)
        {
            string connection;
            try
            {
                connection = "Persist Security Info=True;User ID=" + user + ";Pwd=" + password + ";Server=" + server + ";Database=" + db;
            }
            catch (Exception)
            {
                throw;
            }
            return connection;
        }



        public static string DomainUser(string server, string db)
        {
            string connection;
            try
            {
                connection = "Data Source=" + server + ";Initial Catalog=" + db + ";Integrated Security=SSPI";
            }
            catch (Exception)
            {
                throw;
            }
            return connection;
        }
    }
}