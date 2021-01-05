using EnterpriseMaterial.Data;
using EnterpriseMaterial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseMaterial.Logic
{
    public class ObtainBLL : ILogic.lObtainBLL
    {
        private readonly CoreEntities db;

        public ObtainBLL(CoreEntities _db)
        {
            db = _db;
        }
        /// <summary>
        /// 查询全部商品
        /// </summary>
        /// <returns></returns>
        public List<Goods> GetGoods(out int dataConunt, int page, int limit)
        {
            var list = db.Goods.OrderBy(a => a.ID).Skip((page - 1) * limit).Take(limit).ToList();
            dataConunt = list.Count();
            return list;
        }
    }
}
