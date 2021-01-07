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

        public Dto.UserDto.UserOut GetInfo(int ID)
        {
            return (Dto.UserDto.UserOut)(from s in EF.Signs
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
                                         });
        }

        public Model.User GetEmail(string Email)
        {
            return EF.Users.FirstOrDefault(x => x.Email == Email);
        }
    }
}