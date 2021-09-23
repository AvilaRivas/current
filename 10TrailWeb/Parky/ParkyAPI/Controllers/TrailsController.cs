using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
using ParkyAPI.Models.Dto;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/trails")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    public class TrailsController : Controller
    {
        private readonly ITrailRepository<Trail> _npRepo;
        private readonly IMapper _mapper;

        public TrailsController(ITrailRepository<Trail> npRepo, IMapper mapper)
        {
            this._npRepo = npRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get list of national parks.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<TrailDtoWithRelations>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var objList = _npRepo.GetAllWithNationalPark();
            var objDto = new List<TrailDtoWithRelations>();
            _mapper.Map(objList, objDto);
            return Ok(objDto);
        }

        /// <summary>
        /// Get individual natrional park
        /// </summary>
        /// <param name="trailId">The Id of the antional park</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDtoWithRelations))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _npRepo.GetWithNationalPark(trailId);
            if (trail == null) return NotFound(trailId);
            var trailDto = _mapper.Map<TrailDtoWithRelations>(trail);
            return Ok(trailDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDtoBase))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        public IActionResult CreateTrail([FromBody] TrailDtoBase trailDto)
        {
            if (trailDto == null) return BadRequest(ModelState);
            
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var trail = _mapper.Map<Trail>(trailDto);
            if (!_npRepo.Create(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trail.Id}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trail.Id }, trail);
            //Esta forma es mas sencilla y me guysta mas
            //return Ok(Trail);
        }

        [HttpPatch("{TrailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(204, Type = typeof(TrailDtoUpdate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailDtoUpdate trailDto)
        {
            if (trailDto == null || trailId != trailDto.Id) return BadRequest(ModelState);
            var trail = _mapper.Map<Trail>(trailDto);
            if (!_npRepo.Update(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {trail.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{TrailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            var trail = _npRepo.Get(trailId);
            if (!_npRepo.Delete(trail))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {trail.Id}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
