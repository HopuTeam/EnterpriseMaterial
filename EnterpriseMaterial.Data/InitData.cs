using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Data
{
    public class InitData
    {
        public static void Send(IServiceProvider service)
        {
            using var context = new CoreEntities(service.GetRequiredService<DbContextOptions<CoreEntities>>());
            //if (context.Members.ToList().Count() == 0)
            //{
            //    // 用户信息
            //    context.Members.Add(new Model.Member()
            //    {
            //        Name = "管理员",
            //        Account = "admin",
            //        //Password = "E1ADC3949BA59ABBE56E057F2F883E",// MD5(123456),
            //        Password = "123456",
            //        Age = 16,
            //        Sex = 0
            //    });
            //    context.SaveChanges();
            //}
        }
    }
}