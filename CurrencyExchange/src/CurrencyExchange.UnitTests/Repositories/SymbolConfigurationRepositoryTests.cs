using CurrencyExchange.Contracts.DTOs;
using CurrencyExchange.Exceptions;
using CurrencyExchange.Repositories;
using System;
using System.Linq;
using Xunit;

namespace CurrencyExchange.UnitTests.Repositories
{
    [Trait(COLLECTION_NAME, COLLECTION_DESCRIPTION)]
    [Collection(DEFINITION_NAME)]
    public class SymbolConfigurationRepositoryTests
    {
        const string COLLECTION_NAME = "Symbol configuration repository tests";
        const string COLLECTION_DESCRIPTION = "Testing proper functioning of the repository";
        const string DEFINITION_NAME = "unit-tests";


        [Fact(DisplayName = "Get By Not Existing Id Throws EntityNotFoundException")]
        public void Throws_EntityNotFoundException()
        {
            // Arrange
            var repository = new SymbolConfigurationRepository();

            // Act & Assert
            Assert.Throws<EntityNotFoundException>(() => repository.Get(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Add New Configuration")]
        public void Add_New_Configuration()
        {
            // Arrange
            var repository = new SymbolConfigurationRepository();

            // Act
            repository.Add(new SymbolConfigurationDTO());

            // Assert
            Assert.NotEmpty(repository.GetAll());
        }

        [Fact(DisplayName = "Add And Then Delete Configuration")]
        public void Add_And_Delete_New_Configuration()
        {
            // Arrange
            var repository = new SymbolConfigurationRepository();
            var allData = repository.GetAll();
            foreach (var data in allData) repository.Delete(data.Id);

            // Act
            var dto = new SymbolConfigurationDTO("EUR", "PLN");

            var entity = repository.Add(dto);
            repository.Delete(entity.Id);

            // Assert
            Assert.False(repository.GetAll().Any());
        }

        [Fact(DisplayName = "Empty Delete Throws Not Found Exception")]
        public void Delete_Throws_NotFoundException()
        {
            // Arrange
            var repository = new SymbolConfigurationRepository();

            // Act & Assert
            Assert.Throws<EntityNotFoundException>(() => repository.Delete(Guid.NewGuid()));
        }
    }
}
