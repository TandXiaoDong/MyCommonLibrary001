﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Themes;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace WindowsFormTelerik.CommonUI
{
    public partial class LeftMenu : DockContent
    {
        private DockPanel dockPanel;

        public LeftMenu(DockPanel dockPanel)
        {
            InitializeComponent();
            this.dockPanel = dockPanel;
        }

        private void LeftMenu_Load(object sender, EventArgs e)
        {
            InitControl();
            this.radListView1.SelectedIndexChanged += RadListView1_SelectedIndexChanged;
        }

        List<MenuItem> m_Parent = null;

        //初始化按钮
        private void InitControl()
        {
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(this.radListView1);
            this.radListView1.Items.Clear();
            m_Parent = GetSysMenusParent(); //查询菜单的父节点，先生成父菜单按钮
            int i = m_Parent.Count;

            foreach (var item in m_Parent)
            {
                RadButton b = new RadButton();
                b.Text = item.Menu_Name;
                b.Dock = DockStyle.Top;//控件布局
                b.Tag = item.Menu_Id;
                b.ThemeName = this.office2010BlackTheme1.ThemeName;
                b.TabIndex = i;//控件顺序
                b.Click += new System.EventHandler(this.ButtonClick);
                this.panel1.Controls.Add(b);
                i--;
            }
        }

        /// <summary>
        /// 点击按钮后，改变布局，并初始化菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonClick(object sender, EventArgs e)
        {
            // 1.比较按钮，设置按钮布局顺序
            RadButton clickedButton = (RadButton)sender;
            int clickedButtonTabIndex = clickedButton.TabIndex;
            //button 位置
            foreach (Control ctl in this.panel1.Controls)
            {
                if (ctl is Button)
                {
                    RadButton btn = (RadButton)ctl;
                    if (btn.TabIndex > clickedButtonTabIndex)
                    {
                        //click button 之后的button
                        if (btn.Dock != DockStyle.Bottom)
                        {
                            btn.Dock = DockStyle.Bottom;
                            btn.BringToFront();
                        }
                    }
                    else
                    {
                        if (btn.Dock != DockStyle.Top)
                        {
                            btn.Dock = DockStyle.Top;
                            btn.BringToFront();
                        }
                    }
                }

            }
            //listview加载子菜单
            foreach (var item in m_Parent)
            {
                if (clickedButton.Tag.ToString() == item.Menu_Id)
                {
                    this.radListView1.Items.Clear();
                    List<Base_SysMenu> Childs = GetSysMenuChilds(clickedButton.Tag.ToString());//获取子菜单
                    this.radListView1.ImageList = imageList1;
                    this.radListView1.BeginUpdate(); //防止闪烁
                    foreach (var itemChilds in Childs)
                    {
                        this.radListView1.Items.Add(new ListViewItem
                        {
                            Text = itemChilds.Menu_Name,
                            Tag = itemChilds.Menu_Tags,
                            Name = itemChilds.Menu_Id,
                            ImageIndex = itemChilds.Menu_Img
                        });
                    }
                    this.radListView1.EndUpdate();
                    break;
                }
            }
            this.radListView1.BringToFront();
        }

        //list 菜单点击之后，打开窗口
        private void RadListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radListView1.SelectedItems.Count > 0)
                {
                    //反射动态实例化窗口
                    string FrmWindow = this.radListView1.SelectedItems[0].Tag.ToString(); ;
                    object MenuID = this.radListView1.SelectedItems[0].Text;

                    if (!CheckFormIsOpen(FrmWindow))
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集                   
                        object[] parameters = new object[1];
                        parameters[0] = MenuID;

                        DockContent obj = (DockContent)assembly.CreateInstance("BookMake." + FrmWindow, true, BindingFlags.Default, null, parameters, null, null);// 创建类的实例 
                        obj.ToolTipText = FrmWindow;
                        obj.Show(dockPanel, DockState.Document);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 判断窗口是否打开
        /// </summary>
        /// <param name="frmName"></param>
        /// <returns></returns>
        public bool CheckFormIsOpen(string frmName)
        {
            foreach (var item in dockPanel.Documents)
            {
                if (item.DockHandler.ToolTipText == frmName)
                {
                    item.DockHandler.Activate();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取父菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> GetSysMenusParent()
        {
            List<MenuItem> list = new List<MenuItem>();
            list.Add(new MenuItem() { Menu_Id = "1", Menu_Name = "库存管理" });
            list.Add(new MenuItem() { Menu_Id = "2", Menu_Name = "MES管理" });
            list.Add(new MenuItem() { Menu_Id = "3", Menu_Name = "系统管理" });
            return list;
        }
        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<Base_SysMenu> GetSysMenuChilds(string pid)
        {
            List<Base_SysMenu> list = new List<Base_SysMenu>();
            if (pid == "3")
            {
                list.Add(new Base_SysMenu() { Menu_Id = "1", Menu_Name = "菜单管理", Menu_Tags = "MenuManage", Menu_Img = 1 });
                list.Add(new Base_SysMenu() { Menu_Id = "2", Menu_Name = "用户管理", Menu_Tags = "UserMange", Menu_Img = 2 });
            }
            else
            {
                list.Add(new Base_SysMenu() { Menu_Id = "3", Menu_Name = "测试别点", Menu_Tags = "", Menu_Img = 3 });
            }
            return list;
        }

        public class MenuItem
        {
            public string Menu_Name { get; set; }
            public string Menu_Id { get; set; }
        }

        public class Base_SysMenu
        {
            public string Menu_Name { get; set; }
            public string Menu_Id { get; set; }
            public int Menu_Img { get; set; }
            public string Menu_Tags { get; set; }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private const int WM_NCPAINT = 0x949494;
        private const int WM_NCACTIVATE = 0x949494;
        private const int WM_NCLBUTTONDOWN = 0x949494;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            Rectangle vRectangle = new Rectangle(3, 3, Width - 6, 21);
            IntPtr vHandle = GetWindowDC(m.HWnd);
            Graphics vGraphics = Graphics.FromHdc(vHandle);
            vGraphics.FillRectangle(new LinearGradientBrush(vRectangle,
                Color.Gray, Color.Black, LinearGradientMode.BackwardDiagonal),
                vRectangle);

            StringFormat vStringFormat = new StringFormat();
            vStringFormat.Alignment = StringAlignment.Center;
            vStringFormat.LineAlignment = StringAlignment.Center;
            vGraphics.DrawString("LeftMenu", Font, Brushes.BlanchedAlmond,
                vRectangle, vStringFormat);

            vGraphics.Dispose();
            ReleaseDC(m.HWnd, vHandle);
        }
    }
}
