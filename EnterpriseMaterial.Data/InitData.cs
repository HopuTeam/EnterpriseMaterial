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
                    ActionUrl = null,
                    Description = "this main url",
                    ParentID = 0,
                    MenuIcon = null,
                });
                // 用户信息
                context.Signs.Add(new Model.Sign()
                {
                    Account = "admin",
                    Password = "E1ADC3949BA59ABBE56E057F2F883E",// MD5(123456),
                    IdentityID = 1
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