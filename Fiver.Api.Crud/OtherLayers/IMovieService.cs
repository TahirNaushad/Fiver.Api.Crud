using System.Collections.Generic;

namespace Fiver.Api.Crud.OtherLayers
{
    public interface IMovieService
    {
        List<Movie> GetMovies();
        Movie GetMovie(int id);
        void AddMovie(Movie item);
        void UpdateMovie(Movie item);
        void DeleteMovie(int id);
        bool MovieExists(int id);
    }
}
