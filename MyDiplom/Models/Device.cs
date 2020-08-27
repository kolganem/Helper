using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyDiplom.Models
{
    public class Device
    {
        public int Id { get; set; }

        [DisplayName("Статив")]
        public int Stand { get; set; }

        [DisplayName("Место")]
        public int PlaceOnStand { get; set; }

        [DisplayName("Тип")]
        public string typeDevice { get; set; }

        [DisplayName("Номер прибора")]
        public long NumberDevice { get; set; }

        [DisplayName("Год")]
        public int YearDevice { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата проверки")]
        public DateTime DateCheck { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Дата замены")]
        public DateTime DateFutureCheck { get; set; }

        public ICollection<Failure> Failures { get; set; }

        public Device()
        {
            Failures = new List<Failure>();
        }
    }
}
