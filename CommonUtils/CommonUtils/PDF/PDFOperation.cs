﻿using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace CommonUtils.PDF
{
    /// <summary>
    /// PDF文档操作类
    /// </summary>
    //------------------------------------调用--------------------------------------------
    //PDFOperation pdf = new PDFOperation();
    //pdf.Open(new FileStream(path, FileMode.Create));
    //pdf.SetBaseFont(@"C:\Windows\Fonts\SIMHEI.TTF");
    //pdf.AddParagraph("测试文档（生成时间：" + DateTime.Now + "）", 15, 1, 20, 0, 0);
    //pdf.Close();
    //-------------------------------------------------------------------------------------
    public class PDFOperation
    {
        #region 私有字段
        private Font font;
        private Rectangle rect;   //文档大小
        private Document document;//文档对象
        private BaseFont basefont;//字体
        private PdfWriter pdfWriter;
        private PdfContentByte contentByte;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public PDFOperation()
        {
            rect = PageSize.A4;
            document = new Document(rect);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">页面大小(如"A4")</param>
        public PDFOperation(string type)
        {
            SetPageSize(type);
            document = new Document(rect);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">页面大小(如"A4")</param>
        /// <param name="marginLeft">内容距左边框距离</param>
        /// <param name="marginRight">内容距右边框距离</param>
        /// <param name="marginTop">内容距上边框距离</param>
        /// <param name="marginBottom">内容距下边框距离</param>
        public PDFOperation(string type, float marginLeft, float marginRight, float marginTop, float marginBottom)
        {
            SetPageSize(type);
            document = new Document(rect, marginLeft, marginRight, marginTop, marginBottom);
        }
        #endregion

        #region 设置字体
        /// <summary>
        /// 设置字体
        /// </summary>
        public void SetBaseFont(string path)
        {
            basefont = BaseFont.CreateFont(path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="size">字体大小</param>
        public void SetFont(float size)
        {
            font = new Font(basefont, size);
        }
        #endregion

        #region 设置页面大小
        /// <summary>
        /// 设置页面大小
        /// </summary>
        /// <param name="type">页面大小(如"A4")</param>
        public void SetPageSize(string type)
        {
            switch (type.Trim())
            {
                case "A4":
                    rect = PageSize.A4;
                    break;
                case "A8":
                    rect = PageSize.A8;
                    break;
            }
        }
        #endregion

        #region 实例化文档
        /// <summary>
        /// 实例化文档
        /// </summary>
        /// <param name="os">文档相关信息（如路径，打开方式等）</param>
        public void GetInstance(Stream os)
        {
            this.pdfWriter = PdfWriter.GetInstance(document, os);
        }
        #endregion

        #region 打开文档对象
        /// <summary>
        /// 打开文档对象
        /// </summary>
        /// <param name="os">文档相关信息（如路径，打开方式等）</param>
        public void Open(Stream os)
        {
            GetInstance(os);
            document.Open();
            this.contentByte = pdfWriter.DirectContent;
        }
        #endregion

        #region 关闭打开的文档
        /// <summary>
        /// 关闭打开的文档
        /// </summary>
        public void Close()
        {
            document.Close();
        }
        #endregion

        #region 添加段落
        /// <summary>
        /// 添加段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="fontsize">字体大小</param>
        public void AddParagraph(string content, float fontsize)
        {
            SetFont(fontsize);
            Paragraph pra = new Paragraph(content, font);
            document.Add(pra);
        }

        public void AddLine(int sizeHight)
        {
            //this.contentByte.SetFontAndSize(this.basefont, fontsize);
            this.contentByte.SaveState();
            this.contentByte.SetLineWidth(1.0f);   // Make a bit thicker than 1.0 default
            this.contentByte.MoveTo(35, this.rect.Height - sizeHight);
            this.contentByte.LineTo(this.rect.Width - 35, this.rect.Height - sizeHight);
            this.contentByte.Stroke();
            this.contentByte.RestoreState();
        }

        public void AddParagraph(string title, System.Data.DataTable dt, float fontsize)
        {
            SetFont(fontsize);
            Font f = new Font(basefont);

            PdfPTable table = new PdfPTable(dt.Columns.Count);

            int[] widths = { 12, 12, 25, 21, 25, 25, 25, 15, 15 };
            table.WidthPercentage = 100;
            table.TotalWidth = this.rect.Width;
            table.HorizontalAlignment = Element.ALIGN_CENTER;

            //table.SetWidths(new int[] { 74, 38, 35, 45, 40, 40, 40, 42, 40, 44 });
            PdfPCell cell = new PdfPCell(new Phrase(title, f));
            cell.Colspan = dt.Columns.Count;
            cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right 
            table.AddCell(cell);
            foreach (DataColumn col in dt.Columns)
            {
                PdfPCell cel = new PdfPCell();
                cel.VerticalAlignment = Element.ALIGN_CENTER;
                cel.HorizontalAlignment = Element.ALIGN_RIGHT;
                Paragraph paragraph = new Paragraph(col.ColumnName, f);
                paragraph.Alignment = Element.ALIGN_CENTER;
                cel.AddElement(paragraph);
                table.AddCell(cel);
            }
            foreach (DataRow r in dt.Rows)
            {
                foreach (DataColumn c in dt.Columns)
                {
                    string v = r[c.ColumnName] + "";
                    PdfPCell cel = new PdfPCell();
                    cel.VerticalAlignment = Element.ALIGN_CENTER;
                    cel.HorizontalAlignment = Element.ALIGN_RIGHT;
                    Paragraph pra = new Paragraph(v, f);
                    pra.Alignment = Element.ALIGN_CENTER;
                    cel.AddElement(pra);
                    table.AddCell(cel);
                }
            }
            document.Add(table);
        }

        /// <summary>
        /// 添加段落
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="fontsize">字体大小</param>
        /// <param name="Alignment">对齐方式（1为居中，0为居左，2为居右）</param>
        /// <param name="SpacingAfter">段后空行数（0为默认值）</param>
        /// <param name="SpacingBefore">段前空行数（0为默认值）</param>
        /// <param name="MultipliedLeading">行间距（0为默认值）</param>
        public void AddParagraph(string content, float fontsize, int Alignment, float SpacingAfter, float SpacingBefore, float MultipliedLeading)
        {
            SetFont(fontsize);
            Paragraph pra = new Paragraph(content, font);
            pra.Alignment = Alignment;
            if (SpacingAfter != 0)
            {
                pra.SpacingAfter = SpacingAfter;
            }
            if (SpacingBefore != 0)
            {
                pra.SpacingBefore = SpacingBefore;
            }
            if (MultipliedLeading != 0)
            {
                pra.MultipliedLeading = MultipliedLeading;
            }
            document.Add(pra);
        }
        #endregion

        #region 添加图片
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <param name="Alignment">对齐方式（1为居中，0为居左，2为居右）</param>
        /// <param name="newWidth">图片宽（0为默认值，如果宽度大于页宽将按比率缩放）</param>
        /// <param name="newHeight">图片高</param>
        public void AddImage(string path, int Alignment, float newWidth, float newHeight)
        {
            Image img = Image.GetInstance(path);
            img.Alignment = Alignment;
            if (newWidth != 0)
            {
                img.ScaleAbsolute(newWidth, newHeight);
            }
            else
            {
                if (img.Width > PageSize.A4.Width)
                {
                    img.ScaleAbsolute(rect.Width, img.Width * img.Height / rect.Height);
                }
            }
            document.Add(img);
        }
        #endregion

        #region 添加链接、点
        /// <summary>
        /// 添加链接
        /// </summary>
        /// <param name="Content">链接文字</param>
        /// <param name="FontSize">字体大小</param>
        /// <param name="Reference">链接地址</param>
        public void AddAnchorReference(string Content, float FontSize, string Reference)
        {
            SetFont(FontSize);
            Anchor auc = new Anchor(Content, font);
            auc.Reference = Reference;
            document.Add(auc);
        }

        /// <summary>
        /// 添加链接点
        /// </summary>
        /// <param name="Content">链接文字</param>
        /// <param name="FontSize">字体大小</param>
        /// <param name="Name">链接点名</param>
        public void AddAnchorName(string Content, float FontSize, string Name)
        {
            SetFont(FontSize);
            Anchor auc = new Anchor(Content, font);
            auc.Name = Name;
            document.Add(auc);
        }
        #endregion
    }
}