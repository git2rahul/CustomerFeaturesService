using System;
using System.Collections.Generic;

namespace CustomerFeature.Data
{
    public interface ICustomerFeatureProvider
    {
        IEnumerable<CustomerFeaturesCollection> GetAllFeatures();

        IEnumerable<CustomerFeaturesCollection> GetCustomerFeatures(string customerno);

        CustomerFeaturesCollection GetCustomerFeature(string customerno, int featureId);
    }
}
