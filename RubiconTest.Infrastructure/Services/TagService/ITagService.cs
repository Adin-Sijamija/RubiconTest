using RubiconTest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RubiconTest.Infrastructure.Services.TagService
{
    public interface ITagService
    {
        Task<List<TagModel>> GetTags();
    }
}
