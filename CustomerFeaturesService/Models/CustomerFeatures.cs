using System;

namespace CustomerFeaturesService
{
    public class CustomerFeatures
    {
        public int FeatureId { get; set; }

        public string FeatureName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string AdditionalSettings { get; set; }
    }
}
