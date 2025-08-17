using Application.Abstractions;

namespace Application.Genres.AddGenre
{
    public record class AddGenreCommand(string GenreName): ICommand<AddGenreResponse>
    {
    }
}
