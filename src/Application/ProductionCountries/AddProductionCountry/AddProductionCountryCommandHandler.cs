using Application.Abstractions;
using Domain.Common;
using Domain.Movies;

namespace Application.ProductionCountries.AddProductionCountry
{
    public sealed class AddProductionCountryCommandHandler : ICommandHandler<AddProductionCountryCommand, AddProductionCountryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductionCountryRepository _countryRepository;

        public AddProductionCountryCommandHandler(IUnitOfWork unitOfWork, IProductionCountryRepository countryRepository)
        {
            _unitOfWork = unitOfWork;
            _countryRepository = countryRepository;
        }

        public async Task<Result<AddProductionCountryResponse>> Handle(AddProductionCountryCommand request, CancellationToken cancellationToken)
        {
            var exists = await _countryRepository.ExistsWithName(request.Name, cancellationToken);

            if (exists)
            {
                return Result.Failure<AddProductionCountryResponse>(ProductionCountryApplicationErrors.CountryAlreadyExists(request.Name));
            }

            var countryResult = ProductionCountry.CreateProductionCountry(request.Name);

            if (countryResult.IsFailure)
            {
                return Result.Failure<AddProductionCountryResponse>(countryResult.Error);
            }

            await _countryRepository.CreateAsync(countryResult.Value, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(new AddProductionCountryResponse(countryResult.Value.Id));
        }
    }
}


