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
    /// 描 述：表单信息
    /// </summary>
    public class FormModel
    {
        /// <summary>
        /// 显示类型（一列、二列）
        /// </summary>
        public int? FormType { get; set; }
        /// <summary>
        /// 表单宽度
        /// </summary>
        public int? width { get; set; }
        /// <summary>
        /// 表单高度
        /// </summary>
        public int? height { get; set; }
    }
}
