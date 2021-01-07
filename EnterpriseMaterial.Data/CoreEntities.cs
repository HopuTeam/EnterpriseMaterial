using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Data
{
    public class CoreEntities : DbContext
    {
        public CoreEntities(DbContextOptions<CoreEntities> options) : base(options) { }
        public DbSet<Model.Borrow> Borrows { get; set; }
        public DbSet<Model.BorrowStatus> BorrowStatuses { get; set; }
        public DbSet<Model.Category> Categories { get; set; }
        public DbSet<Model.Departments> Departments { get; set; }
        public DbSet<Model.Goods> Goods { get; set; }
        public DbSet<Model.Identity> Identities { get; set; }
        public DbSet<Model.Log> Logs { get; set; }
        public DbSet<Model.Obtain> Obtains { get; set; }
        public DbSet<Model.IdentityPower> IdentityPowers { get; set; }
        public DbSet<Model.Power> Powers { get; set; }
        public DbSet<Model.Sign> Signs { get; set; }
        public DbSet<Model.Type> Types { get; set; }
        public DbSet<Model.User> Users { get; set; }
    }
}