using System.Threading.Tasks;

namespace CurrencyExchange.Contracts
{
    public interface IValidationService
    {
        bool IsProperBaseSymbol(string baseSymbol);
        bool DoesConfigurationPairExist(string baseSymbol, string targetSymbol);
        Task<bool> DoesSuchASymbolExist(string symbol);
    }
}
