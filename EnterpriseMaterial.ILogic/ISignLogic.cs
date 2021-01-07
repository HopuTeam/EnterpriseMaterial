using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
    public interface ISignLogic
    {
        Model.User GetSign(Model.Sign sign);
        Model.Sign GetAccount(Model.Sign sign);
        bool GetRegister(Model.Sign sign);
    }
}