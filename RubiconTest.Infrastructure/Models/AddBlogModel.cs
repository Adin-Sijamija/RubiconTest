using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconTest.Infrastructure.Models
{
    public class AddBlogModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<String> TagList { get; set; }


    }
}
