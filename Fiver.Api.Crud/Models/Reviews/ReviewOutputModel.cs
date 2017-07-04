using System;

namespace Fiver.Api.Crud.Models.Reviews
{
    public class ReviewOutputModel
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Comments { get; set; }
        public int MovieId { get; set; }

        public DateTime LastReadAt { get; set; }
    }
}
