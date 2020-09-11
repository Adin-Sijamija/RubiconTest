using AutoMapper;
using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconTest.Infrastructure
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Blog, BlogModel>().ReverseMap();

        }
    }
}
