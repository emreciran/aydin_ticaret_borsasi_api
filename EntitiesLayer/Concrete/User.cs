using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Concrete
{
    public class User
    {
        [Key]
        public int USER_ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string? Email { get; set; }

        public string? Username { get; set; }

        public string? Role { get; set; }

        public string? CreatedDate { get; set; }

        public string? UpdatedDate { get; set; }

        public bool Status { get; set; }
    }
}
