using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Foncia.PointOfSaleCodeKata.Dto.Requests
{
    [Serializable]
    [DataContract]
    public class SetPricingRequest
    {
        [DataMember]
        [Required]
        [MinLength(1)]
        public string ProductName { get; set; }

        [DataMember]
        [Required]
        [Range(1, 1000000)]
        public int Count { get; set; }

        [DataMember]
        [Required]
        [Range(0.01, 1000000000)]
        public decimal Price { get; set; }
    }
}
