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

        public Model.Sign GetAccount(string Account, int ID = 0)
        {
            if (Account != null)
                return EF.Signs.FirstOrDefault(x => x.Account == Account);
            else
                return EF.Signs.FirstOrDefault(x => x.ID == ID);
        }

        public bool GetRegister(Dto.UserDto.UserOut info)
        {
            Model.Sign sign = new Model.Sign()
            {
                Account = info.Account,
                Password = info.Password,
                IdentityID = 1
            };
            EF.Signs.Add(sign);

            if (EF.SaveChanges() <= 0)
                return false;

            Model.User user = new Model.User()
            {
                Name = info.Account,
                Sex = 2,
                SignID = sign.ID,
                DepartmentID = 1,
                Status = false,
                Email = info.Email,
                EntryTime = DateTime.Now
            };
            EF.Users.Add(user);

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public bool EditPassword(Model.Sign sign)
        {
            var mod = EF.Signs.FirstOrDefault(x => x.Account == sign.Account);
            mod.Password = sign.Password;

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}