using System;
using System.ComponentModel.DataAnnotations;

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
