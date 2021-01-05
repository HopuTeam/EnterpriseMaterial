using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseMaterial.Data
{
    public class InitData
    {
        public static void Send(IServiceProvider service)
        {
            using var context = new CoreEntities(service.GetRequiredService<DbContextOptions<CoreEntities>>());
            if (context.Signs.ToList().Count() == 0)
            {
                // User identity
                context.Identities.Add(new Model.Identity()
                {
                    Name = "管理员",
                    Description = "This manager",
                    EntryTime = DateTime.Now,
                    Status = true
                });
                // User department
                context.Departments.Add(new Model.Department()
                {
                    Name = "管理部门",
                    EntryTime = DateTime.Now,
                    ParentID = 0,
                    UserID = 1,
                });
                // User engine
                context.Powers.Add(new Model.Power()
                {
                    Name = "Main",
                    ActionUrl = "#",
                    Description = "this main url",
                    ParentID = 0,
                });
                context.Powers.Add(new Model.Power()
                {
                    Name = "Test",
                    ActionUrl = "#",
                    Description = "this test",
                    ParentID = 0,
                });
                // Identity Power Middle Table
                context.IdentityPowers.Add(new Model.IdentityPower()
                {
                    IdentityID = 1,
                    PowerID = 1,
                });
                context.IdentityPowers.Add(new Model.IdentityPower()
                {
                    IdentityID = 1,
                    PowerID = 2,
                });
                // Goods Info
                context.Goods.Add(new Model.Goods()
                {
                    Name = "测试物品",
                    Description = "我真的是测试的",
                    CategoryID = 1,
                    Specification = "200ml",
                    Status = true,
                    EntryTime = DateTime.Now,
                    Money = 188,
                    Number = 50,
                    TypeID = 1,
                    Unit = "组",
                    WarningNum = 5,
                });
                context.Goods.Add(new Model.Goods()
                {
                    Name = "aaa",
                    Description = "ccc",
                    CategoryID = 1,
                    Specification = "200g",
                    Status = true,
                    EntryTime = DateTime.Now,
                    Money = 66,
                    Number = 22,
                    TypeID = 2,
                    Unit = "条",
                    WarningNum = 5,
                });
                // 用户信息
                context.Signs.Add(new Model.Sign()
                {
                    Account = "admin",
                    Password = "E1ADC3949BA59ABBE56E057F2F883E",// MD5(123456),
                    IdentityID = 1
                });
                context.Users.Add(new Model.User()
                {
                    Name = "admin",
                    Birthday = DateTime.Now,
                    DepartmentID = 1,
                    Email = "admin@test.com",
                    EntryTime = DateTime.Now,
                    Sex = 0,
                    SignID = 1,
                    Phone = "17600000000",
                    Status = true
                });
                // type
                context.Types.Add(new Model.Type()
                {
                    Name = "耗材领用"
                });
                context.Types.Add(new Model.Type()
                {
                    Name = "设备借取"
                });
                // borrow status
                context.BorrowStatuses.Add(new Model.BorrowStatus()
                {
                    Name = "待仓库管理审批"
                });
                context.BorrowStatuses.Add(new Model.BorrowStatus()
                {
                    Name = "待部门领导同意"
                });
                context.BorrowStatuses.Add(new Model.BorrowStatus()
                {
                    Name = "申请通过"
                });
                context.BorrowStatuses.Add(new Model.BorrowStatus()
                {
                    Name = "驳回申请"
                });
                context.BorrowStatuses.Add(new Model.BorrowStatus()
                {
                    Name = "取消申请"
                });             
                context.SaveChanges();
            }
        }
    }
}