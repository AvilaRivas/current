using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dto;
using ParkyAPI.Repository.INationalParkRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private INationalParkRepository<NationalPark> _npRepo;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository<NationalPark> npRepo, IMapper mapper)
        {
            this._npRepo = npRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var objList = _npRepo.GetNatiolanParks();
            var objDto = new List<NationalParkDto>();
            _mapper.Map(objList, objDto);
            return Ok(objDto);
        }

        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalPark = _npRepo.GetNationalPark(nationalParkId);
            if (nationalPark == null) return NotFound(nationalParkId);
            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalParkDto);
        }

        [HttpPost]
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
            if (!_npRepo.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {nationalPark.Id}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);
            //Esta forma es mas sencilla y me guysta mas
            //return Ok(nationalPark);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {nationalPark.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_npRepo.NationalParkExists(nationalParkId)) return NotFound();
            var nationalPark = _npRepo.GetNationalPark(nationalParkId);
            if (!_npRepo.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {nationalPark.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
