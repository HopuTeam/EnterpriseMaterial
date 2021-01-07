using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
    public interface ISignLogic
    {
        Model.User GetSign(Model.Sign sign);
        Model.Sign GetAccount(string Account, int ID = 0);
        bool GetRegister(Model.Sign sign);
        bool EditPassword(Model.Sign sign);
    }
}