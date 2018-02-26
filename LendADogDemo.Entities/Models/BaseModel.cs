using System;
using System.ComponentModel.DataAnnotations;

namespace LendADogDemo.Entities.Models
{
    public abstract class BaseModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }
    }
}
