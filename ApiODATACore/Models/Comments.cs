using System;
using System.Collections.Generic;

namespace ApiODATACore.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? IdPerson { get; set; }

        public virtual People IdPersonNavigation { get; set; }
    }
}
