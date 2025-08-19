using Domain.Movies;

namespace Application.Abstractions
{
    public interface IProductionCountryRepository
    {
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);

        Task<ICollection<ProductionCountry>> GetPaginatedAsync(
            int page,
            int size,
            string? search = null,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsWithName(string name, CancellationToken cancellationToken = default);
        Task CreateAsync(ProductionCountry country, CancellationToken cancellationToken = default);
        Task<ProductionCountry?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        void Delete(ProductionCountry country);
    }
}


