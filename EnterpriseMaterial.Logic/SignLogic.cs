using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EnterpriseMaterial.Logic
{
    public class SignLogic : ILogic.ISignLogic
    {
        private Data.CoreEntities EF { get; }

        public SignLogic(Data.CoreEntities _ef)
        {
            EF = _ef;
        }

        public Model.User GetSign(Model.Sign sign)
        {
            var mod = (from s in EF.Signs
                       join u in EF.Users on s.ID equals u.SignID
                       where s.Account == sign.Account && s.Password == sign.Password
                       select u).FirstOrDefault();

            return mod;
        }

        public Model.Sign GetAccount(Model.Sign sign)
        {
            return EF.Signs.FirstOrDefault(x => x.Account == sign.Account);
        }

        public bool GetRegister(Model.Sign sign)
        {
            EF.Signs.Add(sign);
            if (EF.SaveChanges() <= 0)
                return false;

            Model.User user = new Model.User()
            {
                Name = sign.Account,
                Sex = 2,
                SignID = sign.ID,
                DepartmentID = 1,
                Status = false
            };
            EF.Users.Add(user);

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}