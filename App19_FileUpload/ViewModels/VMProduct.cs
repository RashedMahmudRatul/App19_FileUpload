using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App19_FileUpload.ViewModels
{
    public class VMProduct
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
        public double Price { get; set; }
        public IFormFile ProductImage { get; set; }


    }
}
