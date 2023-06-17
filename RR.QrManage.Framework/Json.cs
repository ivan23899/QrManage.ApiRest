using Newtonsoft.Json;
using System.Reflection;

namespace RR.QrManage.Framework
{
    public class Json
    {
        public static string Serialize(object _object)
        {
            try
            {
                return JsonConvert.SerializeObject(_object);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static T Deserealize<T>(string _object)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(_object)!;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodBase.GetCurrentMethod() + " - A problem occurred: ", ex);
            }
        }
    }
}