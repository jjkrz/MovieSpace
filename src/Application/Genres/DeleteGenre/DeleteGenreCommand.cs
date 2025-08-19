using Application.Abstractions;

namespace Application.Genres.DeleteGenre
{
    public record DeleteGenreCommand(Guid Id) : ICommand
    {

    }
}
