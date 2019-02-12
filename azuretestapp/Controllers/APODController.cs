using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using azuretestapp.Model;
using azuretestapp.Service;
using Microsoft.Extensions.Logging;
using azuretestapp.DataAccess;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace azuretestapp.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class APODController : ControllerBase
    {
        private readonly APODService _service;
        private readonly ILogger<APODController> _logger;
        private readonly MongoDBRepository _mongoDBRepository;

        public APODController(ILogger<APODController> logger, APODService service, MongoDBRepository mongoDBRepository)
        {
            _service = service;
            _logger = logger;
            _mongoDBRepository = mongoDBRepository;
        }

        // GET: api/APOD
        [HttpGet]
        public List<APOD> GetAllAPOD()
        {
            _logger.LogInformation("Inside GetAllAPOD method at controller APODController");
            return _mongoDBRepository.GetAPODs(AppSetting.Database_Collection);             
        }

        // GET: api/APOD/5
        [HttpGet("GetAPODByDate")]
        public List<APOD> Get([FromQuery] DateTime APODDate)
        {
            var APODObject = _mongoDBRepository.FindAPODFromDate(AppSetting.Database_Collection, APODDate);
            if (APODObject == null)
            {
                var APODList = JsonConvert.DeserializeObject<APODList>(_service.GetAPOD(APODDate).Result.ToString());
                if (APODList.APODS.Count > 0)
                {
                    _mongoDBRepository.InsertAPOD(AppSetting.Database_Collection, APODList.APODS[0]);
                    APODObject = APODList.APODS[0];
                }
            }
            var listapod = new List<APOD>();
            listapod.Add(APODObject);
            return listapod;
        }

    }
}
