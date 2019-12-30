using System;
using System.Collections.Generic;

namespace CustomerFeature.Repositories
{
    public interface ICustomerFeatureProvider
    {
        IEnumerable<CustomerFeaturesDto> GetAllFeatures();

        IEnumerable<CustomerFeaturesDto> GetCustomerFeatures(string customerno);

        CustomerFeaturesDto GetCustomerFeature(string customerno, int featureId);
    }
}
