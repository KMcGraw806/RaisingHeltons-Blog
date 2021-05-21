using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaisingHeltons.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Body { get; set; }

        public string MediaURL { get; set; }

        public bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public BlogPost()
        {
            Comments = new HashSet<Comment>();
        }
    }
}