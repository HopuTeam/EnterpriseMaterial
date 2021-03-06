﻿namespace EnterpriseMaterial.Dto.PowerDTO
{
    public class PowerOutput
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 说明
        public int PowerId { get; set; }
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string ActionUrl { get; set; }
    }
}
