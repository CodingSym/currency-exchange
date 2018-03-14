using System;

namespace CurrencyExchange.Data.Entities
{
    public class SymbolConfiguration
    {
        public SymbolConfiguration(Guid id, string baseSymbol, string targetSymbol)
        {
            Id = id;
            BaseSymbol = baseSymbol;
            TargetSymbol = targetSymbol;
        }

        public Guid Id { get; private set; }
        public string BaseSymbol { get; private set; }
        public string TargetSymbol { get; private set; }

        public void UpdateSymbols(string baseSymbol, string targetSymbol)
        {
            BaseSymbol = baseSymbol;
            TargetSymbol = targetSymbol;
        }
    }
}
