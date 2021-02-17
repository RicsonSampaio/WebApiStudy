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
        public async Task<IHttpActionResult> GetAllCamps()
        {
            try
            {
                var result = await _repository.GetAllCampsAsync();
                var mapperResult = _mapper.Map<IEnumerable<CampModel>>(result);
                return Ok(mapperResult);
            }
            catch (Exception)
            {
                // TODO logging
                return InternalServerError();
            }
            
        } 

    }
}
