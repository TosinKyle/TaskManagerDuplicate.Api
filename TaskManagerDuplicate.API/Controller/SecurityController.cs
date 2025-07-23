using Microsoft.AspNetCore.Mvc;
using TaskManagerDuplicate.Service.Interface;

namespace TaskManagerDuplicate.API.Controller
{
    // [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : BaseController
    {
        private readonly IOTPService _oTPService;

        public SecurityController(IOTPService oTPService)
        {
            _oTPService = oTPService;
        }

    }
}
