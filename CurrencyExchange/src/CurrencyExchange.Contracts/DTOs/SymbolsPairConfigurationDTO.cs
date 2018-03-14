namespace CurrencyExchange.Contracts.DTOs
{
    public class SymbolsPairConfigurationDTO
    {
        public SymbolsPairConfigurationDTO(string baseSymbol, string targetSymbol)
        {
            BaseSymbol = baseSymbol;
            TargetSymbol = targetSymbol;
        }

        public string BaseSymbol { get; set; }
        public string TargetSymbol { get; set; }
    }
}
