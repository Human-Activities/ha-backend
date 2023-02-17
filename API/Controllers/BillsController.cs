using API.Models.Activities;
using API.Models.Bills;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/bills")]
    [ApiController]
    public class BillsController : Controller
    {
        private readonly BillService _billService;

        public BillsController(BillService activityService)
        {
            _billService = activityService;
        }

        [HttpPost("create")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(CreateBillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateBill(CreateBillRequest request)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _billService.CreateBill(request, userId);

            return Ok(result);
        }

        [HttpGet("get/{billGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(CreateBillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBill(string billGuid)
        {
            var result = await _billService.GetBill(billGuid);

            return Ok(result);
        }

        [HttpGet("get-all/{groupGuid?}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(IEnumerable<CreateBillResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBills(string? groupGuid = null)
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("id"));
            var result = await _billService.GetBills(userId, groupGuid);

            return Ok(result);
        }

        [HttpPut("edit")]
        [ProducesResponseType(typeof(CreateBillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EditBill(EditBillRequest request)
        {
            var result = await _billService.EditBill(request);

            return Ok(result);
        }

        [HttpDelete("delete/{billGuid}")]
        [Authorize(Roles = "loggedUser")]
        [ProducesResponseType(typeof(DeleteBillResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBill(string billGuid)
        {
            var result = await _billService.DeleteBill(billGuid);

            return Ok(result);
        }

        // jakis endpoint, get wszystkie billitemy, zeby moc zrobic podsumowanie
        // podsumwoanie dla grupy, podsumowanie jednego usera, podsuwowanie w obliczu grupy
        // podsumowanie dla user, w ramach tylko siebie, w ramach poszczegolnych grup
    }
}
