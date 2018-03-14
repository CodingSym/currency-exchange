using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.DTOs;
using CurrencyExchange.Repositories;
using System.Collections.Generic;

namespace CurrencyExchange.Services
{
    public class ConfigurationService : IConfigurationService
    {
        readonly ISymbolConfigurationRepository Repository;

        public ConfigurationService(ISymbolConfigurationRepository repository)
        {
            Repository = repository;
        }

        public SymbolConfigurationDTO AddCurrency(SymbolConfigurationDTO model)
        {
            var entity = Repository.Add(model);
            return new SymbolConfigurationDTO(entity.Id, entity.BaseSymbol, entity.TargetSymbol);
        }

        public SymbolConfigurationDTO UpdateCurrency(SymbolConfigurationDTO model)
        {
            var entity = Repository.Update(model);
            return new SymbolConfigurationDTO(entity.Id, entity.BaseSymbol, entity.TargetSymbol);
        }

        public SymbolConfigurationDTO DeleteCurrency(SymbolConfigurationDTO model)
        {
            Repository.Delete(model.Id);
            return model;
        }

        public SymbolConfigurationDTO GetCurrency(SymbolConfigurationDTO model)
        {
            var entity = Repository.Get(model.Id);
            return new SymbolConfigurationDTO(entity.Id, entity.BaseSymbol, entity.TargetSymbol);
        }

        public List<SymbolConfigurationDTO> GetAll()
        {
            var allEntities = Repository.GetAll();

            var listOfDTOs = new List<SymbolConfigurationDTO>();

            foreach (var entity in allEntities)
                listOfDTOs.Add(new SymbolConfigurationDTO(entity.Id, entity.BaseSymbol, entity.TargetSymbol));

            return listOfDTOs;
        }
    }
}
