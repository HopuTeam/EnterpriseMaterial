using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
    public interface IUserLogic
    {
        Dto.UserDto.UserOut GetInfo(int ID);
        List<Model.Power> GetPower(int ID);
    }
}