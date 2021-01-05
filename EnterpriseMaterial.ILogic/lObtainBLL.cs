using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
   public interface lObtainBLL
    {
        List<Model.Goods> GetGoods(out int dataConunt,int page,int limit);


    }
}
