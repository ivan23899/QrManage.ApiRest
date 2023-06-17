using RR.QrManage.Domain.Models;
using RR.QrManage.Framework;
using RR.QrManage.Log;
using System.Data;

namespace RR.QrManage.DataAccess.BD_QRMANAGE.V1
{
    public interface IItem
    {
        public Response<int?> Insert(Domain.Entities.Item item);
        public Response<List<Domain.Entities.Item>> List(int userId);
    }
    public class Item : IItem
    {
        private readonly string _connection;
        private readonly int _timeOut;
        public Item(string connection, int timeOut)
        {
            _connection = connection;
            _timeOut = timeOut;
        }
        public Response<int?> Insert(Domain.Entities.Item item)
        {
            try
            {
                StoreProcedure storeProcedure = new("dbo.SPItem_Insert");
                storeProcedure.AddParameter("@USER_ID_IN", item.UserId);
                storeProcedure.AddParameter("@QR_CODE_VC", item.QrCode);
                storeProcedure.AddParameter("@PHYSICAL_CODE_VC", item.PhysicalCode);
                storeProcedure.AddParameter("@MODEL_CODE_VC", item.ModelCode);
                storeProcedure.AddParameter("@DESCRIPTION_NVC", item.Description);
                storeProcedure.AddParameter("@PICTURE_ITEM_VC", item.PictureItem);
                storeProcedure.AddParameter("@DATE_ACQUISITION_DT", item.DateAcquisition);
                storeProcedure.AddParameter("@ACCOUNTING_NUMBER_VC", item.AccountingNumber);
                storeProcedure.AddParameter("@CODE_ASSIGNED_AREA_VC", item.CodeAssignedArea);
                storeProcedure.AddParameter("@ASSIGNED_AREA_VC", item.AssignedArea);
                storeProcedure.AddParameter("@CODE_RESPONSIBLE_VC", item.CodeResponsible);
                storeProcedure.AddParameter("@RESPONSIBLE_NVC", item.Responsible);
                storeProcedure.AddParameter("@ASSIGNED_DATE_DT", item.AssignedDate);
                storeProcedure.AddParameter("@PURCHASE_PRICE_DC", item.PurchasePrice);
                storeProcedure.AddParameter("@COST_PRICE_DC", item.CostPrice);
                storeProcedure.AddParameter("@ACTUAL_STATE_NVC", item.ActualState);
                storeProcedure.AddParameter("@DEPRECIATION_PERCENTAGE_DC", item.DepreciationPercentage);
                storeProcedure.AddParameter("@CREATION_USER_NVC", item.CreationUser);
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

        public Response<List<Domain.Entities.Item>> List(int userId)
        {
            try
            {
                List<Domain.Entities.Item> listItem = new();

                StoreProcedure storeProcedure = new("dbo.SPItem_List");
                storeProcedure.AddParameter("@USER_ID_IN", userId);
                var responseReturnData = storeProcedure.Select(_connection, _timeOut);
                if (!responseReturnData.Code!.Equals("00"))
                {
                    return Response<List<Domain.Entities.Item>>.Error(responseReturnData.Message!);
                }
                if (responseReturnData.Data!.Result == 0)
                {
                    Logger.Error("MessageError: {0}", responseReturnData.Data!.Message);
                    return Response<List<Domain.Entities.Item>>.Error(responseReturnData.Data!.Message);
                }
                DataTable dataTable = responseReturnData.Data.DataTable;
                var a = Json.Serialize(dataTable);
                if (dataTable.Rows.Count == 0)
                {
                    Logger.Error("MessageError: {0}", responseReturnData.Data!.Message);
                    return Response<List<Domain.Entities.Item>>.Success("", null);
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Domain.Entities.Item item = new()
                    {
                        QrCode = dataTable.Rows[i]["QR_CODE_VC"].ToString()!,
                        PhysicalCode = dataTable.Rows[i]["PHYSICAL_CODE_VC"]!.ToString()!,
                        ModelCode = dataTable.Rows[i]["MODEL_CODE_VC"]!.ToString()!,
                        Description = dataTable.Rows[i]["DESCRIPTION_NVC"]!.ToString()!,
                        PictureItem = dataTable.Rows[i]["PICTURE_ITEM_VC"]!.ToString()!,
                        DateAcquisition = Convert.ToDateTime(dataTable.Rows[i]["DATE_ACQUISITION_DT"]!),
                        AccountingNumber = dataTable.Rows[i]["ACCOUNTING_NUMBER_VC"]!.ToString()!,
                        CodeAssignedArea = dataTable.Rows[i]["CODE_ASSIGNED_AREA_VC"]!.ToString()!,
                        AssignedArea = dataTable.Rows[i]["ASSIGNED_AREA_VC"]!.ToString()!,
                        CodeResponsible = dataTable.Rows[i]["CODE_RESPONSIBLE_VC"]!.ToString()!,
                        Responsible = dataTable.Rows[i]["RESPONSIBLE_NVC"]!.ToString()!,
                        AssignedDate = Convert.ToDateTime(dataTable.Rows[i]["ASSIGNED_DATE_DT"]!),
                        PurchasePrice = Convert.ToDecimal(dataTable.Rows[i]["PURCHASE_PRICE_DC"]!),
                        CostPrice = Convert.ToDecimal(dataTable.Rows[i]["COST_PRICE_DC"]!),
                        ActualState = dataTable.Rows[i]["ACTUAL_STATE_NVC"]!.ToString()!,
                        DepreciationPercentage = Convert.ToDecimal(dataTable.Rows[i]["DEPRECIATION_PERCENTAGE_DC"]!),
                    };
                    listItem.Add(item);
                }
                return Response<List<Domain.Entities.Item>>.Success(listItem);
            }
            catch (Exception ex)
            {
                Logger.Fatal("MessageException: {0} Exception: {1}", ex.Message, Json.Serialize(ex));
                return Response<List<Domain.Entities.Item>>.Error(ex.Message);
            }
        }
    }
}
