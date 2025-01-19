using Microsoft.AspNetCore.Mvc;

namespace AquariumAutomationAPI.Controllers
{
    public class TestController : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public string Test()
        {
            return "The API is running fine";
        }
    }
}
