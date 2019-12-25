using System;
using System.Collections.Generic;
using System.Linq;
using CustomerFeature.Data;
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetFeatures()
        {
            var Message = $" Action GetFeatures called at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation("+++++++++++++ Message displayed +++++++++++++: {Message}", Message);
        
            IEnumerable<CustomerFeaturesCollection> features = _customerFeatureProvider.GetAllFeatures();

            if (features != null)
                return Ok(features);
            else
                return NotFound();
        }
        #endregion

        #region Get Customer Feature list
        [HttpGet("customer/{customerId}", Name = "Customer_FeatureList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerFeatureList(string customerId)
        {
            var Message = $" Action GetCustomerFeatureList called at {DateTime.UtcNow.ToLongTimeString()} with customerId as {customerId}";
            _logger.LogInformation("+++++++++++++ Message displayed +++++++++++++: {Message}", Message);

            IEnumerable<CustomerFeaturesCollection> features = _customerFeatureProvider.GetCustomerFeatures(customerId);

            if (features != null)
                return Ok(features);
            else
                return NotFound();

        }
        #endregion

        #region Get Customer Feature
        [HttpGet("customer/{customerId}/feature/{featureId}", Name = "Customer_Feature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerFeature(string customerId, int featureId)
        {
            var Message = $" Action GetCustomerFeature called at {DateTime.UtcNow.ToLongTimeString()} with customerId as {customerId} and featureId as {featureId.ToString()}";
            _logger.LogInformation("+++++++++++++ Message displayed +++++++++++++: {Message}", Message);

            CustomerFeaturesCollection feature = _customerFeatureProvider.GetCustomerFeature(customerId, featureId);

            if (feature != null)
                return Ok(feature);
            else
                return NotFound();
        }
        #endregion
    }
}
