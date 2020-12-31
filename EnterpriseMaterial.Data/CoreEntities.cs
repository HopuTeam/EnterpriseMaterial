using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Data
{
    public class CoreEntities : DbContext
    {
        public CoreEntities(DbContextOptions<CoreEntities> options) : base(options) { }
    }
}