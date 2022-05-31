﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyNoteSample.Models.Entities
{
    public class EntityLogBase
    {
        [Required, StringLength(50), Display(Name ="Oluşturan")]
        public string CreatedUser { get; set; }

        [Display(Name = "Oluşturma Tarihi")]
        public DateTime CreatedAt { get; set; }

        [StringLength(50), Display(Name = "Güncelleyen")]
        public string ModifiedUser { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public DateTime? ModifiedAt { get; set; }
    }
}
