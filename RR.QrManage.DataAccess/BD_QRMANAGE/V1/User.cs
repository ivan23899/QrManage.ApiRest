using RR.QrManage.Domain.Models;
using RR.QrManage.Framework;
using RR.QrManage.Log;

namespace RR.QrManage.DataAccess.BD_QRMANAGE.V1
{
    public interface IUser
    {
        Response<int?> Insert(Domain.Entities.User user);
        //Response<string> Update(XmlDocument xml);
    }
    public class User : IUser
    {
        private readonly string _connection;
        private readonly int _timeOut;
        public User(string connection, int timeOut)
        {
            _connection = connection;
            _timeOut = timeOut;
        }

        public Response<int?> Insert(Domain.Entities.User user)
        {
            try
            {
                StoreProcedure storeProcedure = new("dbo.SPUser_Insert");
                storeProcedure.AddParameter("@NAME_NVC", user.Name);
                storeProcedure.AddParameter("@ALIAS_NVC", user.Alias);
                storeProcedure.AddParameter("@PICTURE_PROFILE_VC", user.PictureProfile);
                storeProcedure.AddParameter("@CREATION_USER_NVC", user.CreationUser);
                var responseReturnData = storeProcedure.Insert(_connection, _timeOut);
                if (!responseReturnData.Code!.Equals("00"))
                {
                    return Response<int?>.Error(responseReturnData.Message!);
                }
                if (responseReturnData.Data!.Result == 0)
                {
                    Logger.Error("MessageError: {0}", responseReturnData.Data!.Message);
                    return Response<int?>.Error(responseReturnData.Data!.Message);
                }
                if (responseReturnData.Data!.Identity == 0)
                {
                    Logger.Error("MessageError: {0}", responseReturnData.Data!.Message);
                    return Response<int?>.Error(responseReturnData.Data!.Message);
                }
                return Response<int?>.Success(Convert.ToInt32(responseReturnData.Data!.Identity));
            }
            catch (Exception ex)
            {
                Logger.Fatal("MessageException: {0} Exception: {1}", ex.Message, Json.Serialize(ex));
                return Response<int?>.Error(ex.Message);
            }
        }

        #region Update
        //public Response<string> Update(XmlDocument xml)
        //{
        //    try
        //    {
        //        StoreProcedure storeProcedure = new("dbo.ServiceLog_Update");
        //        storeProcedure.AddParameter("@RequestXml", xml.InnerXml);
        //        var responseReturnData = storeProcedure.Update(_connection, _timeOut);
        //        if (responseReturnData == 0)
        //        {
        //            return Response<string>.CreateRespuestaError(Validation.ErrorMessage(Validation.ErrorMessages.SqlError));
        //        }
        //        return Response<string>.CreateRespuestaSuccess(null!);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Fatal("MessageException: {0} Exception: {1}", ex.Message, Json.Serialize(ex));
        //        return Response<string>.CreateRespuestaError(ex.Message);
        //    }
        //}
        #endregion
    }
}
