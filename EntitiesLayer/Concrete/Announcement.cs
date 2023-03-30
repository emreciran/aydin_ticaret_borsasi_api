using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EntitiesLayer.Concrete
{
    public class Announcement
    {
        [Key]
        public int ID { get; set; }

        public string? Title { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string? CreatedDate { get; set; }

        public string? Details { get; set; }

        public string? Link { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; } = string.Empty;
    }
}
