using System.Collections.Generic;

namespace EnterpriseMaterial.Common
{
    public class LayuiJsonModel<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }
}
