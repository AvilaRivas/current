using ParkyAPI.Models.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkyAPI.Models.Trail;

namespace ParkyAPI.Models.Dtos
{
    public class TrailDtoWithRelations : TrailDtoUpdate
    {
        public NationalParkDto NationalPark { get; set; }
    }
}
