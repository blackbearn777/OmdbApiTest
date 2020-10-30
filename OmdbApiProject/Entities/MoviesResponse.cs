using System.Collections.Generic;

namespace OmdbApiProject.Entities
{
    public class MoviesResponse
    {
        public bool Response { get; set; }
        public List<ItemMovieResponse> Search { get; set; }
        public string TotalResults { get; set; }
    }
}