﻿using System;

namespace MiniUApi.Models.ViewModels
{
    public class RatingTakeViewModel
    {
        public string Comment { get; set; }

        public int Rating { get; set; }

        public Guid VideoId { get; set; }
    }
}