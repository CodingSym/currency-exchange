using CurrencyExchange.Contracts;
using CurrencyExchange.Contracts.DTOs;
using CurrencyExchange.ErrorModels;
using CurrencyExchange.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.WebApi.Controllers
{
    [Route("api/configurations")]
    public class ConfigurationController : Controller
    {


        /// <summary>
        ///     Gets all configuration pairs
        /// </summary>
        /// <returns>Configuration pair objects</returns>
        /// <response code="200">Configuration pair objects</response>
        /// <response code="400">Error model</response> 
        [HttpGet("", Name = "GetSymbolConfigurations")]
        [ProducesResponseType(typeof(List<SymbolConfigurationDTO>), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public IActionResult GetSymbolConfigurations(
            [FromServices] IConfigurationService configurationService)
        {
            var response = configurationService.GetAll();

            return new JsonResult(response);
        }

        /// <summary>
        ///     Gets a configuration pair
        /// </summary>
        /// <returns>Configuration pair object</returns>
        /// <response code="200">Configuration pair object</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{configurationId}", Name = "GetSymbolConfiguration")]
        [ProducesResponseType(typeof(SymbolConfigurationDTO), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public IActionResult GetSymbolConfiguration(
            [FromRoute] Guid configurationId,
            [FromServices] IConfigurationService configurationService)
        {
            var dto = new SymbolConfigurationDTO(configurationId);

            var response = InvokeMethod(configurationService.GetCurrency, dto);

            if (response is SymbolConfigurationDTO)
                return new JsonResult(response as SymbolConfigurationDTO);

            return response as IActionResult;
        }

        /// <summary>
        ///     Deletes a configuration pair
        /// </summary>
        /// <returns></returns>
        /// <response code="204">No content</response>
        /// <response code="400">Error model</response> 
        [HttpDelete("{configurationId}", Name = "DeleteSymbolConfiguration")]
        [ProducesResponseType(typeof(SymbolConfigurationDTO), 204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public IActionResult DeleteSymbolConfiguration(
            [FromRoute] Guid configurationId,
            [FromServices] IConfigurationService configurationService)
        {
            var dto = new SymbolConfigurationDTO(configurationId);

            var response = InvokeMethod(configurationService.DeleteCurrency, dto);

            if (response is SymbolConfigurationDTO)
                return NoContent();

            return response as IActionResult;
        }

        /// <summary>
        ///     Updates a configuration pair
        /// </summary>
        /// <returns></returns>
        /// <response code="204">No content</response>
        /// <response code="400">Error model</response> 
        [HttpPut("{configurationId}", Name = "UpdateSymbolConfiguration" )]
        [ProducesResponseType(typeof(SymbolConfigurationDTO), 204)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> UpdateSymbolConfiguration(
            [FromRoute] Guid configurationId,
            [FromBody] SymbolsPairConfigurationDTO model,
            [FromServices] IConfigurationService configurationService,
            [FromServices] IValidationService validationService)
        {
            try
            {
                await ValidateModelData(model, validationService);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            var response = InvokeMethod(configurationService.UpdateCurrency, 
                new SymbolConfigurationDTO(configurationId, model.BaseSymbol, model.TargetSymbol));

            if (response is SymbolConfigurationDTO)
                return NoContent();

            return response as IActionResult;
        }

        /// <summary>
        ///     Adds new configuration pair
        /// </summary>
        /// <returns>Id of the configuration</returns>
        /// <response code="201">Guid showing the configuration id</response>
        /// <response code="400">Error model</response> 
        [HttpPost("")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> ConfigurePair(
            [FromBody] SymbolsPairConfigurationDTO model,
            [FromServices] IConfigurationService configurationService,
            [FromServices] IValidationService validationService)
        {
            try
            {
                await ValidateModelData(model, validationService);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            var dto = new SymbolConfigurationDTO(model.BaseSymbol, model.TargetSymbol);
            var response = InvokeMethod(configurationService.AddCurrency, dto);

            if(response is SymbolConfigurationDTO)
            {
                var responseDTO = response as SymbolConfigurationDTO;
                return Created($"api/configuration/{responseDTO.Id}", responseDTO);
            }

            return response as IActionResult;
        }

        object InvokeMethod(Func<SymbolConfigurationDTO, SymbolConfigurationDTO> method, SymbolConfigurationDTO dto)
        {
            try
            {
                return method(dto);
            }
            catch (EntityNotFoundException entityNotFound)
            {
                return BadRequest(new ErrorModel(entityNotFound.Message));
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

        async Task ValidateModelData(SymbolsPairConfigurationDTO model, IValidationService validationService)
        {
            if (model == null) throw new ValidationException("Invalid data");

            if (model.BaseSymbol.ToLower() == model.TargetSymbol.ToLower())
                throw new ValidationException("Such a configuration is not acceptable");

            if (validationService.DoesConfigurationPairExist(model.BaseSymbol, model.TargetSymbol))
                throw new ValidationException("Such a configuration already exists");

            if (!validationService.IsProperBaseSymbol(model.BaseSymbol))
                throw new ValidationException("Incorrect base symbol, only EUR is supported");

            if (!await validationService.DoesSuchASymbolExist(model.TargetSymbol))
                throw new ValidationException($"Incorrect target symbol: {model.TargetSymbol}");
        }
    }
}
