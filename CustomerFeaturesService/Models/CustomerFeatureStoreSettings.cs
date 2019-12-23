using System;

namespace CustomerFeaturesApi.Models
{
    public class CustomerFeatureStoreSettings : ICustomerFeatureStoreSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICustomerFeatureStoreSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
