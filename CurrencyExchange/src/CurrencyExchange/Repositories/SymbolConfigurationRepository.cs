using CurrencyExchange.Contracts.DTOs;
using CurrencyExchange.Data.Entities;
using CurrencyExchange.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyExchange.Repositories
{
    public interface ISymbolConfigurationRepository
    {
        SymbolConfiguration Add(SymbolConfigurationDTO model);
        void Delete(Guid id);
        SymbolConfiguration Update(SymbolConfigurationDTO model);
        SymbolConfiguration Get(Guid Id);
        SymbolConfiguration Get(string baseSymbol, string targetSymbol);
        List<SymbolConfiguration> GetAll();
    }
    public class SymbolConfigurationRepository : ISymbolConfigurationRepository
    {
        static readonly List<SymbolConfiguration> dbList = new List<SymbolConfiguration>();

        public SymbolConfiguration Add(SymbolConfigurationDTO model)
        {
            var symbolConfiguration = new SymbolConfiguration(Guid.NewGuid(), model.BaseSymbol, model.TargetSymbol);
            dbList.Add(symbolConfiguration);

            return symbolConfiguration;
        }

        public void Delete(Guid id)
        {
            var symbolConfiguration = Get(id);

            dbList.Remove(symbolConfiguration);
        }

        public SymbolConfiguration Get(Guid Id)
        {
            var model = dbList.Where(symbolConfiguration => symbolConfiguration.Id == Id).SingleOrDefault();
            if (model == null)
                throw new EntityNotFoundException($"Entity {nameof(SymbolConfiguration)} with id: {Id} was not found");

            return model;
        }

        public SymbolConfiguration Get(string baseSymbol, string targetSymbol)
        {
            return dbList.Where(symbolConfiguration => 
                        symbolConfiguration.BaseSymbol == baseSymbol && 
                        symbolConfiguration.TargetSymbol == targetSymbol)
                        .SingleOrDefault();
        }

        public List<SymbolConfiguration> GetAll()
        {
            return dbList.ToList();
        }

        public SymbolConfiguration Update(SymbolConfigurationDTO model)
        {
            var symbolConfiguration = Get(model.Id);

            symbolConfiguration.UpdateSymbols(model.BaseSymbol, model.TargetSymbol);
            return symbolConfiguration;
        }
    }
}
