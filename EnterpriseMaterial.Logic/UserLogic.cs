using System;
using System.Collections.Generic;
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

        //public Model.Sign
        //EF.Signs.FirstOrDefault(x => x.ID == )
    }
}