using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Foncia.PointOfSaleCodeKata.Dto
{
    [Serializable]
    [DataContract]
    public class BasketItemDto
    {
        [DataMember]
        [Required]
        [MinLength(1)]
        public string ProductName { get; set; }

        [DataMember]
        [Required]
        [Range(1, 1000000)]
        public int Count { get; set; }
    }
}
