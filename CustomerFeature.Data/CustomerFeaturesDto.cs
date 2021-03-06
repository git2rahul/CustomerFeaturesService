﻿namespace CustomerFeature.Repositories
{
    public class CustomerFeaturesDto
    {
        public int FeatureId { get; set; }

        public string FeatureName { get; set; }

        public System.DateTime EnrollmentDate { get; set; }

        public string FeaturesSetting { get; set; }

        public bool DefaultState { get; set; }

        public System.DateTime CreationDate { get; set; }

        public System.DateTime EffectiveDate { get; set; }

        public System.DateTime ExpirationDate { get; set; }
    }
}