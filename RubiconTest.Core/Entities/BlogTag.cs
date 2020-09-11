using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconTest.Core.Entities
{
    public class BlogTag
    {

        public string  BlogSlug { get; set; }
        public Blog Blog { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }


    }
}
