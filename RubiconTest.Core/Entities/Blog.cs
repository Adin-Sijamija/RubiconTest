using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RubiconTest.Core.Entities
{
    public class Blog
    {
        [Key]
        public string Slug { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description{ get; set; }
        [Required]
        public string Body { get; set; }


        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }


  

        public List<BlogTag> BlogTags { get; set; }

    }
}
