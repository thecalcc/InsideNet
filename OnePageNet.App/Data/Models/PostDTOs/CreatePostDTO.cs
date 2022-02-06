﻿using System.ComponentModel.DataAnnotations;

namespace OnePageNet.App.Data.Models.PostDTOs
{
    public class CreatePostDto : BaseDto
    {
        [Required]
        public ApplicationUser ApplicationUser { get; set; }
        [Required(ErrorMessage = "You cannot subimt an empty post")]
        public string Text { get; set; }
        public string MediaUri { get; set; }
    }
}
