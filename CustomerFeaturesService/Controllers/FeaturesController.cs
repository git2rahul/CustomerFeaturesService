using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerFeaturesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeaturesController : Controller
    {
        private readonly ILogger<FeaturesController> _logger;

        public FeaturesController(ILogger<FeaturesController> logger)
        {
            _logger = logger;
        }

        #region Get Features
        [HttpGet]
        [Route("")]
        public IEnumerable<CustomerFeatures> GetFeatures()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new CustomerFeatures
            {
                EnrollmentDate = DateTime.Now.AddDays(index),
                FeatureName = ""
            })
            .ToArray();
        }
        #endregion

        #region Get Customer Feature list
        [HttpGet]
        [Route("customers/{customerId}")]
        public IEnumerable<CustomerFeatures> GetCustomerFeatureList(string customerId)
        {
            var rng = new Random();
            return Enumerable.Range(1, 3).Select(index => new CustomerFeatures
            {
                FeatureId = index,
                EnrollmentDate = DateTime.Now.AddDays(index),
                FeatureName = "Feature"+ index
            })
            .ToArray();

        }
        #endregion

        #region Get Customer Feature
        [HttpGet]
        [Route("customers/{customerId}/feature/{featureId}")]
        public IActionResult GetCustomerFeature(string customerId, string featureId)
        {
            CustomerFeatures feature = new CustomerFeatures()
            {
                 FeatureId = 2,
                 FeatureName = "MemData",
                 EnrollmentDate = DateTime.Now
            };

            return Ok(feature);
        }
        #endregion
    }
}
