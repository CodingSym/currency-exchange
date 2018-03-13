using System.Threading.Tasks;

namespace CurrencyExchange.Contracts
{
    public interface IConfigurationService
    {
        Task AddCurrency(string symbol);
        Task DeleteCurrency(string symbol);
        Task ValidateCurrency(string symbol);
    }
}
