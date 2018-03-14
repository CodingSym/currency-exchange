using System;

namespace CurrencyExchange.Contracts.DTOs
{
    public class SymbolConfigurationDTO
    {
        public SymbolConfigurationDTO()
        {
        }

        public SymbolConfigurationDTO(Guid id)
        {
            Id = id;
        }

        public SymbolConfigurationDTO(string baseSymbol, string targetSymbol)
        {
            BaseSymbol = baseSymbol;
            TargetSymbol = targetSymbol;
        }

        public SymbolConfigurationDTO(Guid id, string baseSymbol, string targetSymbol)
        {
            Id = id;
            BaseSymbol = baseSymbol;
            TargetSymbol = targetSymbol;
        }

        public Guid Id { get; set; }
        public string BaseSymbol { get; set; }
        public string TargetSymbol { get; set; }
    }
}
