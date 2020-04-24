using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Xml;

namespace WindowsFormTelerik.GridViewExportData
{
    class XMLHelper
    {
        public class DealXML
        {
            /// <summary>
            /// 将xml对象内容转换为dataset
            /// </summary>
            /// <param name="xmlData"></param>
            /// <returns></returns>
            private static DataSet ConvertXMLToDataSet(string xmlData)
            {
                using (StringReader stream = new StringReader(xmlData))
                {
                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        try
                        {
                            DataSet xmlDS = new DataSet();
                            xmlDS.ReadXml(reader);
                            return xmlDS;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (reader != null)
                                reader.Close();
                        }
                    }
                }
            }

            /// <summary>
            /// 将DataSet转换为xml对象字符串
            /// </summary>
            /// <param name="xmlDS"></param>
            /// <returns></returns>

            public static string ConvertDataSetToXML(DataSet xmlDS)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //从stream装载到XmlTextReader 
                    using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default))
                    {

                        try
                        {
                            //用WriteXml方法写入文件.  
                            xmlDS.WriteXml(writer);
                            int count = (int)stream.Length;
                            byte[] arr = new byte[count];
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.Read(arr, 0, count);

                            return Encoding.Default.GetString(arr).Trim();
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (writer != null) writer.Close();
                        }
                    }
                }
            }

            /// <summary>
            /// 将DataSet转换为xml文件
            /// </summary>
            /// <param name="xmlDS"></param>
            /// <param name="xmlFile"></param>

            public static void ConvertDataSetToXMLFile(DataSet xmlDS, string xmlFile)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    //从stream装载到XmlTextReader 
                    using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Default))
                    {

                        try
                        {
                            //用WriteXml方法写入文件.  
                            xmlDS.WriteXml(writer);
                            int count = (int)stream.Length;
                            byte[] arr = new byte[count];
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.Read(arr, 0, count);

                            //返回Encoding.Default编码的文本  
                            using (StreamWriter sw = new StreamWriter(xmlFile))
                            {
                                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                                string ss = System.Text.Encoding.Default.GetString(arr).Trim();
                                sw.WriteLine(ss);
                                sw.Flush();
                                sw.Close();
                            }
                            //重新排版生成的xml文档
                            XmlDocument doc = new XmlDocument();
                            doc.Load(xmlFile);
                            doc.Save(xmlFile);
                            doc = null;

                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (writer != null) writer.Close();
                        }
                    }
                }
            }


            // Xml结构的文件读到DataSet中
            public static DataSet XmlToDataTableByFile(string fileName)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);

                string xmlString = doc.InnerXml;
                DataSet XmlDS = ConvertXMLToDataSet(xmlString);

                return XmlDS;
            }
        }
    }
}
