﻿using System;

namespace Fiver.Api.Crud.Models.Movies
{
    public class MovieOutputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Summary { get; set; }

        public DateTime LastReadAt { get; set; }
    }
}
