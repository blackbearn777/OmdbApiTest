using System.Collections.Generic;


namespace OmdbApiProject.Entities
{
    public class MoviesResponse
    {
        public string TotalResults { get; set; }
        public bool Response { get; set; }
        public List<ItemMovieResponse> Search { get; set; }
    }
}
