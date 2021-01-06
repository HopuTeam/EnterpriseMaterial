using EnterpriseMaterial.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
   public interface ICategoryBLL
    {
        Common.LayuiTreeModel LayuiTreeModels();
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Category Selectid(int id);
        /// <summary>
        /// 添加分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        bool AddSave(int id, string name);
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        bool EditSave(Category view);
       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        bool Delete(int id);
    }
}
