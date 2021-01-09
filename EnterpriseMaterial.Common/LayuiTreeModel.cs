using System.Collections.Generic;

namespace EnterpriseMaterial.Common
{
    public class LayuiTreeModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        public string PowerId { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 节点标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 节点字段名
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 节点是否初始展开，默认 false
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 节点是否初始为选中状态（如果开启复选框的话），默认 false
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// 节点是否为禁用状态。默认 false
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// 点击节点弹出新窗口对应的 url。需开启 isJump 参数
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// 子节点。支持设定选项同父节点
        /// </summary> 
        public List<LayuiTreeModel> Children { get; set; }
    }
}
