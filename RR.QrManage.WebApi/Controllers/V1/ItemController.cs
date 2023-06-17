using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RR.QrManage.DataAccess.BD_QRMANAGE.V1;
using RR.QrManage.Domain.Models;
using RR.QrManage.Domain.Models.V1.Requests;

namespace RR.QrManage.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem _item;
        public ItemController(IItem item)
        {
            _item = item;
        }
        [AllowAnonymous]
        [Route("Item/Register")]
        [HttpPost]
        public IActionResult Register([FromBody] List<ItemRegisterRequest> listRequest)
        {
            try
            {
                List<ItemRegisterRequest> listItemError = new();
                foreach (var request in listRequest)
                {
                    string qrCode = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Concat(request.UserId, "-", Guid.NewGuid().ToByteArray())));
                    Domain.Entities.Item item = new()
                    {
                        UserId = request.UserId,
                        QrCode = qrCode,
                        PhysicalCode = request.PhysicalCode,
                        ModelCode = request.ModelCode,
                        Description = request.Description,
                        PictureItem = request.PictureItem,
                        DateAcquisition = request.DateAcquisition,
                        AccountingNumber = request.AccountingNumber,
                        CodeAssignedArea = request.CodeAssignedArea,
                        AssignedArea = request.AssignedArea,
                        CodeResponsible = request.CodeResponsible,
                        Responsible = request.Responsible,
                        AssignedDate = request.AssignedDate,
                        PurchasePrice = request.PurchasePrice,
                        CostPrice = request.CostPrice,
                        ActualState = request.ActualState,
                        DepreciationPercentage = request.DepreciationPercentage,
                        CreationUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                    };
                    var responseItemInsert = _item.Insert(item);
                    if (!responseItemInsert.Code!.Equals("00"))
                    {
                        listItemError.Add(request);
                    }
                }
                return Ok(Response<List<ItemRegisterRequest>>.Success(listItemError));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("Item/List")]
        [HttpPost]
        public IActionResult List([FromBody] ItemListRequest request)
        {
            try
            {
                var responseItemList = _item.List(request.UserId);
                return Ok(responseItemList);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
