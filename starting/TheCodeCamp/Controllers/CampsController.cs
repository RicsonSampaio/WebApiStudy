using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TheCodeCamp.Data;
using TheCodeCamp.Models;

namespace TheCodeCamp.Controllers
{
    [RoutePrefix("api/camps")]
    public class CampsController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public CampsController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route()]
        public async Task<IHttpActionResult> GetAllCamps(bool includeTalks = false)
        {
            try
            {
                var result = await _repository.GetAllCampsAsync(includeTalks);
                var mapperResult = _mapper.Map<IEnumerable<CampModel>>(result);
                return Ok(mapperResult);
            }
            catch (Exception)
            {
                // TODO logging
                return InternalServerError();
            }
            
        }

        [HttpGet]
        [Route("{moniker}")]
        public async Task<IHttpActionResult> GetSingleCamp(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);
                if (result == null) return NotFound();
                return Ok(_mapper.Map<CampModel>(result)) ;
            }
            catch (Exception)
            {
                // TODO logging
                return InternalServerError();
            }
        }

    }
}
