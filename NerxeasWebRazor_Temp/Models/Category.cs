﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NerxeasWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 1000, ErrorMessage = "The Display Order must be between 1-1000.")]
        public int DisplayOrder { get; set; }
    }
}
