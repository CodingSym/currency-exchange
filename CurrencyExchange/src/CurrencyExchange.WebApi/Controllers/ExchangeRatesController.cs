using CurrencyExchange.Contracts;
using CurrencyExchange.ErrorModels;
using CurrencyExchange.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CurrencyExchange.Controllers
{
    [Route("api/exchange-rates")]
    public class ExchangeRatesController : Controller
    {
        /// <summary>
        ///     Gets exchange rate
        /// </summary>
        /// <returns>Exchange rate</returns>
        /// <response code="200">String showing the exchange rate</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{baseSymbol}/{targetSymbol}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> ExchangeRate(
            [FromRoute] string baseSymbol,
            [FromRoute] string targetSymbol,
            [FromServices] IExchangeRateService exchangeRateService,
            [FromServices] IValidationService validationService)
        {
            if (!validationService.DoesConfigurationPairExist(baseSymbol, targetSymbol))
                return BadRequest($"Not configured symbols {baseSymbol} and {targetSymbol} ");

            return await HandleRequest(exchangeRateService.CurrentExchangeRate(baseSymbol, targetSymbol));
        }

        /// <summary>
        ///     Gets average exchange rate for the past 7 days
        /// </summary>
        /// <returns>Average exchange rate</returns>
        /// <response code="200">String showing the average exchange rate</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{baseSymbol}/{targetSymbol}/average")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> AverageExchangeRate(
            [FromRoute] string baseSymbol,
            [FromRoute] string targetSymbol,
            [FromServices] IExchangeRateService exchangeRateService,
            [FromServices] IValidationService validationService)
        {
            if (!validationService.DoesConfigurationPairExist(baseSymbol, targetSymbol))
                return BadRequest($"Not configured symbols {baseSymbol} and {targetSymbol} ");

            return await HandleRequest(exchangeRateService.AverageExchangeRateFor7Days(baseSymbol, targetSymbol));
        }

        /// <summary>
        ///     Gets minimum exchange rate for the past 7 days
        /// </summary>
        /// <returns>Minimum exchange rate</returns>
        /// <response code="200">String showing the minimum exchange rate</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{baseSymbol}/{targetSymbol}/minimum")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> MinimumExchangeRate(
            [FromRoute] string baseSymbol,
            [FromRoute] string targetSymbol,
            [FromServices] IExchangeRateService exchangeRateService,
            [FromServices] IValidationService validationService)
        {
            if (!validationService.DoesConfigurationPairExist(baseSymbol, targetSymbol))
                return BadRequest($"Not configured symbols {baseSymbol} and {targetSymbol} ");

            return await HandleRequest(exchangeRateService.MinimumExchangeRateDuring7Days(baseSymbol, targetSymbol));
        }

        /// <summary>
        ///     Gets maximum exchange rate for the past 7 days
        /// </summary>
        /// <returns>String array</returns>
        /// <response code="200">String showing the maximum exchange rate</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{baseSymbol}/{targetSymbol}/maximum")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> MaximumExchangeRate(
            [FromRoute] string baseSymbol,
            [FromRoute] string targetSymbol,
            [FromServices] IExchangeRateService exchangeRateService,
            [FromServices] IValidationService validationService)
        {
            if (!validationService.DoesConfigurationPairExist(baseSymbol, targetSymbol))
                return BadRequest($"Not configured symbols {baseSymbol} and {targetSymbol} ");

            return await HandleRequest(exchangeRateService.MaximumExchangeRateDuring7Days(baseSymbol, targetSymbol));
        }

        /// <summary>
        ///     Gets all exchange rate
        /// </summary>
        /// <returns>String array</returns>
        /// <response code="200">List showing the exchange rates</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{baseSymbol}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> GetAllRates(
            [FromRoute] string baseSymbol,
            [FromServices] IExchangeRateService exchangeRateService,
            [FromServices] IValidationService validationService)
        {
            if (!validationService.IsProperBaseSymbol(baseSymbol))
                return BadRequest($"Incorrect Base Symbol: {baseSymbol}. Only EUR is supported");

            try
            {
                var allRates = await exchangeRateService.GetAllRates(baseSymbol);
                return new JsonResult(allRates);
            }
            catch(ExchangeRateException exception)
            {
                return BadRequest(new ErrorModel($"{exception.Message}. {baseSymbol} is not supported. Only EUR is allowed as base currency"));
            }
        }

        async Task<IActionResult> HandleRequest(Task<string> task)
        {
            try
            {
                var response = await task;
                return new JsonResult(response);
            }
            catch (ExchangeRateException exchangeRateException)
            {
                return BadRequest(new ErrorModel(exchangeRateException.Message));
            }
            catch (ConfigurationSetupException configurationSetupException)
            {
                return BadRequest(new ErrorModel(configurationSetupException.Message));
            }
            catch (FixerException fixerException)
            {
                return BadRequest(new ErrorModel(fixerException.Message));
            }
            catch (Exception)
            {
                // Some logging
                return new StatusCodeResult(500);
            }
        }
    }
}
