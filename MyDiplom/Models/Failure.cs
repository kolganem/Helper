using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyDiplom.Models
{
    public class Failure
    {
        public int FailureId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата отказа")]
        public DateTime DateFailure { get; set; }

        [DisplayName("Причина")]
        public string Reason { get; set; }

        [DisplayName("Последствия")]
        public string Consequence { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
