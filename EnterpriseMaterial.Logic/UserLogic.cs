using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseMaterial.Logic
{
    public class UserLogic : ILogic.IUserLogic
    {
        private Data.CoreEntities EF { get; }

        public UserLogic(Data.CoreEntities _ef)
        {
            EF = _ef;
        }

        public List<Model.Power> GetPower(int ID)
        {
            return (from u in EF.Signs
                    join i in EF.Identities on u.IdentityID equals i.ID
                    join ip in EF.IdentityPowers on i.ID equals ip.IdentityID
                    join p in EF.Powers on ip.PowerID equals p.ID
                    where u.ID == ID
                    select p).ToList();
        }

        public Model.User GetAccount(string Email, int SignID = 0)
        {
            if (Email != null)
                return EF.Users.FirstOrDefault(x => x.Email == Email);
            else
                return EF.Users.FirstOrDefault(x => x.SignID == SignID);
        }

        public bool Auth(int SignID)
        {
            var mod = EF.Users.FirstOrDefault(x => x.SignID == SignID);
            mod.Status = true;

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public Dto.UserDto.UserOut GetInfo(int ID)
        {
            var mod = (from s in EF.Signs
                       join u in EF.Users on s.ID equals u.SignID
                       where s.ID == ID
                       select new
                       {
                           s.Account,
                           u.Name,
                           u.Email,
                           u.Phone,
                           u.Birthday,
                           u.Sex,
                           u.Status,
                           u.EntryTime
                       }).FirstOrDefault();
            return new Dto.UserDto.UserOut()
            {
                Account = mod.Account,
                Name = mod.Name,
                Email = mod.Email,
                Phone = mod.Phone,
                Birthday = mod.Birthday,
                Sex = mod.Sex,
                Status = mod.Status,
                EntryTime = mod.EntryTime
            };
        }

        public Model.User GetEmail(string Email)
        {
            return EF.Users.FirstOrDefault(x => x.Email == Email);
        }
    }
}