using Fiver.Api.Crud.Models.Movies;
using Fiver.Api.Crud.OtherLayers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiver.Api.Crud.Controllers
{
    [Route("movies")]
    public class MoviesController : Controller
    {
        private readonly IMovieService service;

        public MoviesController(IMovieService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = service.GetMovies();
            var outputModel = ToOutputModel(model);

            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetMovie")]
        public IActionResult Get(int id)
        {
            var model = service.GetMovie(id);
            var outputModel = ToOutputModel(model);

            if (outputModel == null)
                return NotFound();

            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult Create([FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            var model = FromInputModel(inputModel);
            service.AddMovie(model);

            var outputModel = ToOutputModel(model);

            return CreatedAtRoute("GetMovie", new { id = outputModel.Id }, outputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]MovieInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!service.MovieExists(id))
                return BadRequest();

            var model = FromInputModel(inputModel);
            service.UpdateMovie(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!service.MovieExists(id))
                return BadRequest();

            service.DeleteMovie(id);

            return NoContent();
        }

        #region " Mappings "

        private MovieOutputModel ToOutputModel(Movie model)
        {
            if (model == null) return null;

            return new MovieOutputModel
            {
                Id = model.Id,
                Title = model.Title,
                ReleaseYear = model.ReleaseYear,
                Summary = model.Summary,
                LastReadAt = DateTime.Now
            };
        }

        private List<MovieOutputModel> ToOutputModel(List<Movie> model)
        {
            return model.Select(item => ToOutputModel(item))
                        .ToList();
        }

        private Movie FromInputModel(MovieInputModel inputModel)
        {
            if (inputModel == null) return null;

            return new Movie
            {
                Id = inputModel.Id,
                Title = inputModel.Title,
                ReleaseYear = inputModel.ReleaseYear,
                Summary = inputModel.Summary
            };
        }

        #endregion
    }
}
