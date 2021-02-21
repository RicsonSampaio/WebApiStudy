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
            CreateMap<Camp, CampModel>()
                .ReverseMap();

            CreateMap<Talk, TalkModel>()
                .ReverseMap()
                .ForMember(t => t.Speaker, opt => opt.Ignore())
                .ForMember(t => t.Camp, opt => opt.Ignore());

            CreateMap<Speaker, SpeakerModel>()
                .ReverseMap();

            //
        }
    }
}