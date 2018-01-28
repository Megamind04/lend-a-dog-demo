using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendADogDemo.Entities.Models
{
    public abstract class BaseModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }
    }
}
