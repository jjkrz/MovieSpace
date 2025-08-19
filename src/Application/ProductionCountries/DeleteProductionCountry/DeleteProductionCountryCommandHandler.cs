using Application.Abstractions;
using Domain.Common;

namespace Application.ProductionCountries.DeleteProductionCountry
{
    public class DeleteProductionCountryCommandHandler : ICommandHandler<DeleteProductionCountryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductionCountryRepository _countryRepository;

        public DeleteProductionCountryCommandHandler(IUnitOfWork unitOfWork, IProductionCountryRepository countryRepository)
        {
            _unitOfWork = unitOfWork;
            _countryRepository = countryRepository;
        }

        public async Task<Result> Handle(DeleteProductionCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);

            if (country == null)
            {
                return Result.Failure(ProductionCountryApplicationErrors.CountryNotFound(request.Id));
            }

            _countryRepository.Delete(country);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(country.Id);
        }
    }
}


