using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodeCamp.Models;

namespace TheCodeCamp.Data
{
    public class CampingMappingProfile : Profile
    {
        public CampingMappingProfile()
        {
            CreateMap<Camp, CampModel>();
        }
    }
}