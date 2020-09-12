using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RubiconTest.Infrastructure.EF;
using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RubiconTest.Infrastructure.Services.TagService
{
    public class TagService : ITagService
    {

        private readonly RubiconContext db;
        private readonly IMapper mapper;

        public TagService(RubiconContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<TagModel>> GetTags()
        {
            return mapper.Map<List<TagModel>>(await db.Tags.AsNoTracking().ToListAsync());
        }
    }
}
