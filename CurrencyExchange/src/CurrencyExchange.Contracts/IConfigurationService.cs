using CurrencyExchange.Contracts.DTOs;
using System.Collections.Generic;

namespace CurrencyExchange.Contracts
{
    public interface IConfigurationService
    {
        SymbolConfigurationDTO GetCurrency(SymbolConfigurationDTO model);
        SymbolConfigurationDTO AddCurrency(SymbolConfigurationDTO model);
        SymbolConfigurationDTO UpdateCurrency(SymbolConfigurationDTO model);
        SymbolConfigurationDTO DeleteCurrency(SymbolConfigurationDTO model);
        List<SymbolConfigurationDTO> GetAll();
    }
}
