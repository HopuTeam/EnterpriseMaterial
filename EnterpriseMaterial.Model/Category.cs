using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public string Description { get; set; }
    }
}