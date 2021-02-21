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
        [Route("{moniker}", Name = "GetSingleCamp")]
        public async Task<IHttpActionResult> GetSingleCamp(string moniker, bool includeTalks = false)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker, includeTalks);
                if (result == null) return NotFound();
                return Ok(_mapper.Map<CampModel>(result));
            }
            catch (Exception)
            {
                // TODO logging
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("searchByDate/{eventDate:datetime}")]
        public async Task<IHttpActionResult> SearchByEventDate(DateTime eventDate, bool includetalks = false)
        {
            try
            {
                var result = await _repository.GetAllCampsByEventDate(eventDate, includetalks);
                return Ok(_mapper.Map<CampModel[]>(result));
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("createCamp")]
        public async Task<IHttpActionResult> CreateCamp(CampModel campModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var camp = _mapper.Map<Camp>(campModel);
                    _repository.AddCamp(camp);

                    if (await _repository.SaveChangesAsync())
                    {
                        var newCampModel = _mapper.Map<CampModel>(camp);

                        var location = Url.Link("GetSingleCamp", new { moniker = newCampModel.Moniker });

                        return Created(location , newCampModel);
                    }
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{moniker}")]
        public async Task<IHttpActionResult> UpdateCamp(string moniker, CampModel campModel)
        {
            try
            {
                var camp = await _repository.GetCampAsync(moniker);
                _mapper.Map(campModel, camp);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok(_mapper.Map<CampModel>(camp));
                } 
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        [HttpDelete]
        [Route("{moniker}")]
        public async Task<IHttpActionResult> DeleteCamp(string moniker, CampModel campModel)
        {
            try
            {
                var camp = await _repository.GetCampAsync(moniker);

                _repository.DeleteCamp(camp);


                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

    }
}
