using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Foncia.PointOfSaleCodeKata.Dto.Requests
{
    [Serializable]
    [DataContract]
    public class CalculateTotalRequest
    {
        [DataMember]
        [Required]
        public List<BasketItemDto> BasketItems { get; set; }
    }
}
