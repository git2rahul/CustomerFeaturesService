using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CustomerFeature.Data
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
        public IEnumerable<CustomerFeaturesCollection> GetAllFeatures()
        {
            IEnumerable<CustomerFeaturesCollection> customerFeatures = null;
         
            try
            {
                using (var connnection = new SqlConnection(_connectionString))
                {
                    customerFeatures = connnection.Query<CustomerFeaturesCollection>("EXEC [CustomerFeatures].[GetFeaturesList];");
                   
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
        public IEnumerable<CustomerFeaturesCollection> GetCustomerFeatures(string customerno)
        {
            IEnumerable<CustomerFeaturesCollection> customerFeatures = null;

            try
            {
                using (var connnection = new SqlConnection(_connectionString))
                {
                    customerFeatures = connnection.Query<CustomerFeaturesCollection>("[CustomerFeatures].[GetCustomerFeaturesList]", new {CustomerNumber = customerno},
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
        public CustomerFeaturesCollection GetCustomerFeature(string customerno, int featureId)
        {
            IEnumerable<CustomerFeaturesCollection> customerFeatures = null;
            CustomerFeaturesCollection customerFeature = null;

            try
            {
                using (var connnection = new SqlConnection(_connectionString))
                {
                    customerFeatures = connnection.Query<CustomerFeaturesCollection>("[CustomerFeatures].[GetCustomerFeature]", new { CustomerNumber = customerno, FeatureId = featureId },
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
