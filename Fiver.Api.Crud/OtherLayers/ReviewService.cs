using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiver.Api.Crud.OtherLayers
{
    public class ReviewService : IReviewService
    {
        private readonly List<Review> reviews;

        public ReviewService()
        {
            this.reviews = new List<Review>
            {
                new Review { Id = 1, MovieId = 1, Reviewer = "Tahir", Comments = "Excellent" },
                new Review { Id = 2, MovieId = 1, Reviewer = "Tony", Comments = "Good" },
                new Review { Id = 3, MovieId = 2, Reviewer = "Tahir", Comments = "Very Good" },
                new Review { Id = 4, MovieId = 3, Reviewer = "Tahir", Comments = "Average" },
                new Review { Id = 5, MovieId = 3, Reviewer = "Robert", Comments = "Interesting" },
            };
        }

        public List<Review> GetReviews(int movieId)
        {
            return this.reviews.Where(r => r.MovieId == movieId).ToList();
        }

        public Review GetReview(int movieId, int id)
        {
            return this.reviews.Where(r => r.MovieId == movieId && r.Id == id).FirstOrDefault();
        }

        public void AddReview(Review item)
        {
            this.reviews.Add(item);
        }

        public void UpdateReview(Review item)
        {
            var model = this.reviews.Where(r => r.Id == item.Id).FirstOrDefault();

            model.Reviewer = item.Reviewer;
            model.Comments = item.Comments;
        }

        public void DeleteReview(int id)
        {
            var model = this.reviews.Where(r => r.Id == id).FirstOrDefault();

            this.reviews.Remove(model);
        }

        public bool ReviewExists(int movieId, int id)
        {
            return this.reviews.Any(r => r.MovieId == movieId && r.Id == id);
        }
    }
}
