﻿using System.ComponentModel.DataAnnotations;

namespace Theme_Implemenet.Models
{
    public class ProductSizeModel
    {
        [Required]
        [MaxLength]
        public string productsizeName { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Lastmodifield { get; set; }
    }
}
