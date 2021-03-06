using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerFeaturesApi.Models
{
    public class CustomerFeature
    {
        public int FeatureId { get; set; }

        [Required]
        public string FeatureName { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        public string FeatureSettings { get; set; }
    }
}
