using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
    public interface IUserLogic
    {
        Dto.UserDto.UserOut GetInfo(int ID);
        List<Model.Power> GetPower(int ID);
        Model.User GetEmail(string Email);
        Model.User GetAccount(string Email, int SignID = 0);
        bool Auth(int SignID);
        bool GetEdit(Dto.UserDto.UserOut user);
        object GetUsers(int page, int limit);
    }
}