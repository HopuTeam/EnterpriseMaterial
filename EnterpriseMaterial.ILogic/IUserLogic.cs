using System.Collections.Generic;

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
        bool SwichStatus(int SignID);
        List<Model.Power> GetPowerInfo();
        bool AddPower(Model.Power power);
        Model.Power GetOnePowerInfo(int ID);
        bool EditPower(Model.Power power);
        bool DelPower(int ID);
    }
}