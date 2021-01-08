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

        public object GetUsers(int page, int limit)
        {
            var mod = (from s in EF.Signs
                       join u in EF.Users on s.ID equals u.SignID
                       select new
                       {
                           s.ID,
                           s.Account,
                           s.IdentityID,
                           u.Name,
                           u.Email,
                           u.Phone,
                           u.Birthday,
                           u.Sex,
                           u.Status,
                           u.EntryTime,
                           u.LockTime
                       }).ToList();
            var info = new
            {
                code = 0,
                msg = "",
                count = mod.Count(),
                data = mod.Skip((page - 1) * limit).Take(limit)
            };
            return info;
        }

        public bool SwichStatus(int SignID)
        {
            var mod = EF.Users.FirstOrDefault(x => x.SignID == SignID);
            if (mod.Status)
            {
                mod.Status = false;
                mod.LockTime = DateTime.Now;
            }
            else
            {
                mod.Status = true;
                mod.LockTime = null;
            }

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
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

        public bool GetEdit(Dto.UserDto.UserOut user)
        {
            var Smod = EF.Signs.FirstOrDefault(x => x.ID == user.ID);
            var Umod = GetAccount(null, user.ID);
            if (user.Password != null)
            {
                Smod.Password = user.Password;
            }
            else if (Umod.Email != user.Email)
            {
                Umod.Email = user.Email;
                Umod.Status = false;
            }else if (user.IdentityID != 0)
            {
                Smod.IdentityID = user.IdentityID;
            }

            Umod.Sex = user.Sex;
            Umod.Name = user.Name;
            Umod.Email = user.Email;
            Umod.Phone = user.Phone;
            Umod.Birthday = user.Birthday;

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
                           s.ID,
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
                ID = mod.ID,
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

        //public bool DelUser(int ID)
        //{
        //    var sign = EF.Signs.FirstOrDefault(x => x.ID == ID);
        //    var user = EF.Users.FirstOrDefault(x => x.SignID == ID);
        //}

        public Model.User GetEmail(string Email)
        {
            return EF.Users.FirstOrDefault(x => x.Email == Email);
        }
    }
}