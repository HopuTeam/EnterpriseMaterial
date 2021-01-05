using EnterpriseMaterial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseMaterial.Logic
{
    public class TypeLogic : ILogic.ITypeLogic
    {
        private CoreEntities EF { get; }

        public TypeLogic(CoreEntities _ef)
        {
            EF = _ef;
        }

        public List<Model.Type> GetList()
        {
            return EF.Types.ToList();
        }

        public bool Add(Model.Type type)
        {
            EF.Types.Add(type);

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public bool Delete(Model.Type type)
        {
            EF.Remove(type);

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        public bool Edit(Model.Type type)
        {
            var mod = EF.Types.FirstOrDefault(x => x.ID == type.ID);
            mod.Name = type.Name;

            if (EF.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}