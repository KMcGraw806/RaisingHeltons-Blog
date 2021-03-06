using RaisingHeltons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaisingHeltons.ViewModels
{
    public class BlogPostDetailsVM
    {
        public BlogPost BlogPost { get; set; }
        public ICollection<BlogPost> SidePosts { get; set; }

        public BlogPostDetailsVM()
        {
            SidePosts = new HashSet<BlogPost>();
        }
    }
}