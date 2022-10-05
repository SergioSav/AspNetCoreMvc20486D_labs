using ButterfliesShop.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ButterfliesShop.Models
{
    public class Butterfly
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the butterfly name")]
        [Display(Name = "Common Name:")]
        public string CommonName { get; set; }

        [Required(ErrorMessage = "Please select the butterfly family")]
        [Display(Name = "Butterfly Family:")]
        public Family? ButterflyFamily { get; set; }

        [MaxButterflyQuantityValidation(50)]
        [Required(ErrorMessage = "Please select the butterfly quantity")]
        [Display(Name = "Butterflies Quantity:")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Please type the characteristics")]
        [StringLength(50)]
        [Display(Name = "Characteristics:")]
        public string Characteristics { get; set; }

        [Display(Name = "Update on")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Please select the butterfly picture")]
        [Display(Name = "Butterflies Picture:")]
        public IFormFile PhotoAvatar { get; set; }

        public string ImageName { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] PhotoFile { get; set; }
    }
}
