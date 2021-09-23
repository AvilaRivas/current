using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dto;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    public class NationalParksController : Controller
    {
        private INationalParkRepository<NationalPark> _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository<NationalPark> npRepo, IMapper mapper)
        {
            this._npRepo = npRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<NationalParkDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetAll();
            var objDto = new List<NationalParkDto>();
            _mapper.Map(objList, objDto);
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual natrional park
        /// </summary>
        /// <param name="nationalParkId">The Id of the antional park</param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _npRepo.Get(nationalParkId);
            if (nationalPark == null) return NotFound(nationalParkId);
            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalParkDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest(ModelState);
            if (_npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.Create(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalPark.Id}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { version=HttpContext.GetRequestedApiVersion().ToString(),
                nationalParkId = nationalPark.Id }, nationalPark);
            //Esta forma es mas sencilla y me guysta mas
            //return Ok(nationalPark);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.Update(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {nationalPark.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_npRepo.NationalParkExists(nationalParkId)) return NotFound();
            var nationalPark = _npRepo.Get(nationalParkId);
            if (!_npRepo.Delete(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {nationalPark.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
