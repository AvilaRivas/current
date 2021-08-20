using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Models;
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
            return Ok(objList);
        }
    }
}
