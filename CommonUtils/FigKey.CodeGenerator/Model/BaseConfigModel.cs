﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FigKey.CodeGenerator.Model
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c) 2019 丰柯电子科技（重庆）有限公司
    /// 创建人：唐小东
    /// 日 期：2020.01.03 14:54
    /// 描 述：基本信息配置
    /// </summary>
    public class BaseConfigModel
    {
        /// <summary>
        /// 数据库连接Id
        /// </summary>
        public string DataBaseLinkId { get; set; }
        /// <summary>
        /// 数据库表名称
        /// </summary>
        public string DataBaseTableName { get; set; }
        /// <summary>
        /// 数据库表主键
        /// </summary>
        public string DataBaseTablePK { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 中文描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 实体类名
        /// </summary>
        public string EntityClassName { get; set; }
        /// <summary>
        /// 映射类名
        /// </summary>
        public string MapClassName { get; set; }
        /// <summary>
        /// 服务类名
        /// </summary>
        public string ServiceClassName { get; set; }
        /// <summary>
        /// 接口类名
        /// </summary>
        public string IServiceClassName { get; set; }
        /// <summary>
        /// 业务类名
        /// </summary>
        public string BusinesClassName { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 列表页名
        /// </summary>
        public string IndexPageName { get; set; }
        /// <summary>
        /// 表单页名
        /// </summary>
        public string FormPageName { get; set; }
        /// <summary>
        /// 输出所在区域
        /// </summary>
        public string OutputAreas { get; set; }
        /// <summary>
        /// 实体层输出目录
        /// </summary>
        public string OutputEntity { get; set; }
        /// <summary>
        /// 映射层输出目录
        /// </summary>
        public string OutputMap { get; set; }
        /// <summary>
        /// 服务层输出目录
        /// </summary>
        public string OutputService { get; set; }
        /// <summary>
        /// 接口层输出目录
        /// </summary>
        public string OutputIService { get; set; }
        /// <summary>
        /// 业务层输出目录
        /// </summary>
        public string OutputBusines { get; set; }
        /// <summary>
        /// 应用层输出目录
        /// </summary>
        public string OutputController { get; set; }
        /// <summary>
        /// 表格信息模型
        /// </summary>
        public GridModel gridModel { get; set; }
        /// <summary>
        /// 表格字段模型
        /// </summary>
        public List<GridColumnModel> gridColumnModel { get; set; }
        /// <summary>
        /// 表单信息模型
        /// </summary>
        public FormModel formModel { get; set; }
        /// <summary>
        /// 表单字段模型
        /// </summary>
        public List<FormFieldModel> formFieldModel { get; set; }
    }
}
