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
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    public class NationalParksV2Controller : Controller
    {
        private INationalParkRepository<NationalPark> _npRepo;
        private readonly IMapper _mapper;

        public NationalParksV2Controller(INationalParkRepository<NationalPark> npRepo, IMapper mapper)
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


    }
}
