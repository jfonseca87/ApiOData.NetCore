using System;
using System.Collections.Generic;

namespace ApiODATACore.Models
{
    public partial class People
    {
        public People()
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
    }
}
