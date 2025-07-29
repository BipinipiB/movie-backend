namespace movie_backend.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Releaseyear { get; set; } = string.Empty;
    }
}
