using api.Contracts;
using api.Models;
using DataTables.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/v1/[controller]")]
    public class PatientController : Controller
    {
        private readonly IDemo _demoService;
        public PatientController(IDemo demoService)
        {

            _demoService = demoService;
        }
        [Route("get-test")]
        [HttpGet]

        public ActionResult test()
        {
            return StatusCode(StatusCodes.Status200OK, null);
        }
        [Route("get-all")]
        [HttpPost]
        public async Task<IActionResult> LoadDataAsync([FromBody]DTParameters param)
        {
            try
            {
                var data = await _demoService.GetResultsAsync(param);
                return new JsonResult(data);

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }

        }

    }
}
