using System.Collections.Generic;

namespace EnterpriseMaterial.ILogic
{
    public interface ITypeLogic
    {
        List<Model.Type> GetList();
        bool Add(Model.Type type);
        bool Delete(Model.Type type);
        bool Edit(Model.Type type);
    }
}