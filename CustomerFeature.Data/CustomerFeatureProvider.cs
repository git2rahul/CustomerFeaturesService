using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CustomerFeature.Repositories
{
    public class CustomerFeatureProvider : ICustomerFeatureProvider
    {
        private readonly string _connectionString;

        public CustomerFeatureProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Operation to get list of all features available for any customer to configure.
        /// </summary>
        /// <returns>List of features of type IEnumerable</returns>
        public IEnumerable<CustomerFeaturesDto> GetAllFeatures()
        {
            IEnumerable<CustomerFeaturesDto> customerFeatures = null;
         
            try
            {
                using (var connnection = new SqlConnection(_connectionString))
                {
                    customerFeatures = connnection.Query<CustomerFeaturesDto>("EXEC [GetFeaturesList];");
                   
                }
            }
            catch (Exception ex)
            {
                var t = ex;
                // log as exception.
            }

            return customerFeatures;
        }

        /// <summary>
        /// Operation to get list of all configured features for a customer.
        /// </summary>
        /// <returns>List of features of type IEnumerable</returns>
        public IEnumerable<CustomerFeaturesDto> GetCustomerFeatures(string customerno)
        {
            IEnumerable<CustomerFeaturesDto> customerFeatures = null;

            try
            {
                using (var connnection = new SqlConnection(_connectionString))
                {
                    customerFeatures = connnection.Query<CustomerFeaturesDto>("[GetCustomerFeaturesList]", new {CustomerNumber = customerno},
                        commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                var t = ex;
                // log as exception.
            }

            return customerFeatures;
        }

        /// <summary>
        /// Operation to get details of a configure features for a customer.
        /// </summary>
        /// <returns>Features details</returns>
        public CustomerFeaturesDto GetCustomerFeature(string customerno, int featureId)
        {
            IEnumerable<CustomerFeaturesDto> customerFeatures = null;
            CustomerFeaturesDto customerFeature = null;

            try
            {
                using (var connnection = new SqlConnection(_connectionString))
                {
                    customerFeatures = connnection.Query<CustomerFeaturesDto>("[GetCustomerFeature]", new { CustomerNumber = customerno, FeatureId = featureId },
                        commandType: System.Data.CommandType.StoredProcedure);
                    customerFeature = customerFeatures.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                var t = ex;
                // log as exception.
            }

            return customerFeature;
        }

    }
}
