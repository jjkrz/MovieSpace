using Application.Abstractions;

namespace Application.Movies.AddMovie
{
    public sealed record AddMovieCommand(string Title, 
        string Description, 
        Uri? PosterUri, 
        TimeOnly Duration, 
        DateTime ReleaseDate) : ICommand<AddMovieResponse>
    {
    }
}
