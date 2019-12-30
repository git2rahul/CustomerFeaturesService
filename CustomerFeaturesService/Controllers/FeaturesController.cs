using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CustomerFeature.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerFeaturesApi.Controllers
{

    [Route("[controller]")]
    public class FeaturesController : ApplicationBaseController
    {
        private readonly ILogger<FeaturesController> _logger;
        private readonly ICustomerFeatureProvider _customerFeatureProvider;

        public FeaturesController(ILogger<FeaturesController> logger, ICustomerFeatureProvider customerFeatureProvider)
        {
            _logger = logger;
            _customerFeatureProvider = customerFeatureProvider;
        }

        #region Get Features
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<CustomerFeaturesApi.Models.Feature>), (int)HttpStatusCode.OK)]
        public ActionResult<List<CustomerFeaturesApi.Models.Feature>> GetFeatures()
        {
            var Message = $" Action GetFeatures called at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation("+++++++++++++ Message displayed +++++++++++++: {Message}", Message);

            
            var features = _customerFeatureProvider.GetAllFeatures();

            if (features == null)
                return NotFound();

            return Ok(MapFeaturesDto(features.ToList()));
        }

        #endregion

        #region Get Customer Feature list
        [HttpGet("customer/{customerId}", Name = "Customer_FeatureList")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<CustomerFeaturesDto>), (int)HttpStatusCode.OK)]
        public ActionResult<List<CustomerFeaturesApi.Models.CustomerFeature>> GetCustomerFeatureList(string customerId)
        {
            var Message = $" Action GetCustomerFeatureList called at {DateTime.UtcNow.ToLongTimeString()} with customerId as {customerId}";
            _logger.LogInformation("+++++++++++++ Message displayed +++++++++++++: {Message}", Message);

            var features = _customerFeatureProvider.GetCustomerFeatures(customerId);

            if (features != null)
                return Ok(MapCustomerFeaturesDto(features.ToList()));
            else
                return NotFound();

        }
        #endregion

        #region Get Customer Feature
        [HttpGet("customer/{customerId}/feature/{featureId}", Name = "Customer_Feature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CustomerFeaturesApi.Models.CustomerFeature> GetCustomerFeature(string customerId, int featureId)
        {
            var Message = $" Action GetCustomerFeature called at {DateTime.UtcNow.ToLongTimeString()} with customerId as {customerId} and featureId as {featureId.ToString()}";
            _logger.LogInformation("+++++++++++++ Message displayed +++++++++++++: {Message}", Message);

           
            if (featureId < 1)
            {
                return BadRequest();
            }

            var featureDto = _customerFeatureProvider.GetCustomerFeature(customerId, featureId);

            if (featureDto != null)
              return Ok(MapCustomerFeatureDto(featureDto));
             else
              return NotFound();
        }
        #endregion

        #region MapCustomerFeaturesDto
        /// <summary>
        /// MapCustomerFeaturesDto
        /// </summary>
        /// <param name="featuresDto"></param>
        /// <returns></returns>
        private List<CustomerFeaturesApi.Models.CustomerFeature> MapCustomerFeaturesDto(List<CustomerFeaturesDto> featuresDto)
        {

            var customerFeatures = new List<CustomerFeaturesApi.Models.CustomerFeature>();

            if(featuresDto != null || featuresDto.Count < 1)
             featuresDto.ForEach(CustomerFeaturesDto => customerFeatures
                .Add(MapCustomerFeatureDto(CustomerFeaturesDto)));

            return customerFeatures;
        }
        #endregion

        #region MapCustomerFeatureDto
        /// <summary>
        /// MapCustomerFeatureDto
        /// </summary>
        /// <param name="customerFeaturesDto"></param>
        /// <returns></returns>
        private CustomerFeaturesApi.Models.CustomerFeature MapCustomerFeatureDto(CustomerFeaturesDto customerFeaturesDto)
        {
            return new CustomerFeaturesApi.Models.CustomerFeature
            {
                FeatureId = customerFeaturesDto.FeatureId,
                FeatureName = customerFeaturesDto.FeatureName,
                EnrollmentDate = customerFeaturesDto.EnrollmentDate,
                FeatureSettings = customerFeaturesDto.FeaturesSetting
            };
        }
        #endregion

        #region MapFeaturesDto
        /// <summary>
        /// MapFeaturesDto
        /// </summary>
        /// <param name="featuresDto"></param>
        /// <returns>List of Feature</returns>
        private List<CustomerFeaturesApi.Models.Feature> MapFeaturesDto(List<CustomerFeaturesDto> featuresDto)
        {

            var features = new List<CustomerFeaturesApi.Models.Feature>();

            if (featuresDto != null || featuresDto.Count < 1)
                featuresDto.ForEach(CustomerFeaturesDto => features
                   .Add(MapFeatureDto(CustomerFeaturesDto)));

            return features;
        }
        #endregion

        #region MapFeatureDto
        /// <summary>
        /// MapFeatureDto
        /// </summary>
        /// <param name="customerFeaturesDto"></param>
        /// <returns>Feature</returns>
        private CustomerFeaturesApi.Models.Feature MapFeatureDto(CustomerFeaturesDto customerFeaturesDto)
        {
            return new CustomerFeaturesApi.Models.Feature
            {
                FeatureId = customerFeaturesDto.FeatureId,
                FeatureName = customerFeaturesDto.FeatureName,
                CreationDate = customerFeaturesDto.CreationDate,
                DefaultState = customerFeaturesDto.DefaultState,
                EffectiveDate = customerFeaturesDto.EffectiveDate,
                ExpirationDate = customerFeaturesDto.ExpirationDate
            };
        }
        #endregion

    }
}
