using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    public class Log
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
