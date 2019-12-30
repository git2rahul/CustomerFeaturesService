using System;
namespace CustomerFeaturesApi.Models
{
    public class Feature
    {
        public int FeatureId { get; set; }

        public string FeatureName { get; set; }

        public bool DefaultState { get; set; }

        public System.DateTime CreationDate { get; set; }

        public System.DateTime EffectiveDate { get; set; }

        public System.DateTime ExpirationDate { get; set; }
    }
}
