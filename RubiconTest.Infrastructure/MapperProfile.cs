using AutoMapper;
using RubiconTest.Core.Entities;
using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiconTest.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Blog, BlogModel>().ReverseMap();

            //Map the Tag 2-n tier property to a Json array via for member mapper
            CreateMap<Blog, BlogModel>()
                .ForMember(dest => dest.TagList, src => src.MapFrom(s => s.BlogTags.Select(x=>x.Tag.Name).ToList()));

            CreateMap<BlogModel, Blog>();

            CreateMap<AddBlogModel,Blog>();

            CreateMap<Tag, TagModel>().ReverseMap();
        }
    }
}
