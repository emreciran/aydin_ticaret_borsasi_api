using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.Concrete
{
    public class Event
    {
        [Key]
        public int ID { get; set; }

        public string? Details { get; set; }

        public string? Title { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public string UpdatedDate { get; set; } = string.Empty;

        public string StartDate { get; set; } = string.Empty;

        public string EndDate { get; set; } = string.Empty;
    }
}
