using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        
        [NotMappedAttribute]
        [Required(ErrorMessage = "Photo is required.")]
        public List<IFormFile> filePhot { get; set; }
    }
}
