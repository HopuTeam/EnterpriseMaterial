using EnterpriseMaterial.Common;
using EnterpriseMaterial.Data;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseMaterial.Logic
{
    public class CategoryBLL : ILogic.ICategoryBLL
    {
        private readonly CoreEntities db;

        public CategoryBLL(CoreEntities _db) 
        {
            db = _db;
        }

        public LayuiTreeModel LayuiTreeModels()
        {
            List<Category> categories = db.Categories.Where(a => a.ParentID == 0).ToList();
            LayuiTreeModel layuiTree = new LayuiTreeModel()
            {
                Id = 0,
                Spread = true,
                Title = "分类目录",
                Field = "",
                Disabled = false,
                Checked = false,
            };
            DiGui(ref layuiTree, categories);
            return layuiTree;
        }

        private void DiGui(ref LayuiTreeModel model, List<Category> categories)
        {
            List<LayuiTreeModel> treeModels = new List<LayuiTreeModel>();
            foreach (var item in categories)
            {
                LayuiTreeModel entit = new LayuiTreeModel()
                {
                    Id = item.ParentID,
                    Spread = false,
                    Title = item.Name,
                    Disabled = false,
                    Checked = true
                };
                List<Category> list = db.Categories.Where(a => a.ParentID == item.ID).ToList();
                if (list!=null)
                {
                    entit.Checked = false;
                    DiGui(ref entit, list);
                }
                treeModels.Add(entit);
                model.Children = treeModels;
            };
          
        }

    }
}
