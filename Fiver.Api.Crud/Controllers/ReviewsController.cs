using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fiver.Api.Crud.Models.Reviews;
using Fiver.Api.Crud.OtherLayers;

namespace Fiver.Api.Crud.Controllers
{
    [Route("movies/{movieId}/reviews")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IMovieService movieService;

        public ReviewsController(
            IReviewService reviewService,
            IMovieService movieService)
        {
            this.reviewService = reviewService;
            this.movieService = movieService;
        }

        [HttpGet]
        public IActionResult Get(int movieId)
        {
            var model = reviewService.GetReviews(movieId);

            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetReview")]
        public IActionResult Get(int movieId, int id)
        {
            var model = reviewService.GetReview(movieId, id);
            if (model == null)
                return NotFound();

            var outputModel = ToOutputModel(model);
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create(int movieId, [FromBody]ReviewInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!movieService.MovieExists(movieId))
                return NotFound();

            var model = ToDomainModel(inputModel);
            reviewService.AddReview(model);

            var outputModel = ToOutputModel(model);
            return CreatedAtRoute("GetReview", 
                new { movieId = outputModel.MovieId, id = outputModel.Id }, 
                outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int movieId, int id, [FromBody]ReviewInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!reviewService.ReviewExists(movieId, id))
                return NotFound();

            var model = ToDomainModel(inputModel);
            reviewService.UpdateReview(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int movieId, int id)
        {
            if (!reviewService.ReviewExists(movieId, id))
                return NotFound();

            reviewService.DeleteReview(id);

            return NoContent();
        }

        #region " Mappings "

        private ReviewOutputModel ToOutputModel(Review model)
        {
            return new ReviewOutputModel
            {
                Id = model.Id,
                Reviewer = model.Reviewer,
                Comments = model.Comments,
                MovieId = model.MovieId,
                LastReadAt = DateTime.Now
            };
        }

        private List<ReviewOutputModel> ToOutputModel(List<Review> model)
        {
            return model.Select(item => ToOutputModel(item))
                        .ToList();
        }

        private Review ToDomainModel(ReviewInputModel inputModel)
        {
            return new Review
            {
                Id = inputModel.Id,
                Reviewer = inputModel.Reviewer,
                Comments = inputModel.Comments,
                MovieId = inputModel.MovieId
            };
        }

        #endregion
    }
}
