using System.Collections.Generic;

namespace EnterpriseMaterial.ILogic
{
    public interface lObtainBLL
    {
        List<Model.Goods> GetGoods(out int dataConunt, int page, int limit);


    }
}
