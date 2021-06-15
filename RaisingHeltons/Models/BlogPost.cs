using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaisingHeltons.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public string Abstract { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        [Display(Name = "Image")]
        public string MediaPath { get; set; }

        [Display(Name = "Publish?")]
        public bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public BlogPost()
        {
            Comments = new HashSet<Comment>();
            Categories = new HashSet<Category>();
        }
    }
}