using Newtonsoft.Json;
using NPOI.HPSF;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Inspire.Erp.Web.Common
{
    public class ReadingFiles<T> where T : class
    {
        public  List<T> ReadFile(string filename)
        {
            List<T> items = new List<T>();
            try
            {
                string dataFolder = AppDomain.CurrentDomain.BaseDirectory + "SettingsFiles\\";
                string filePath = Path.Combine(dataFolder, filename);

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                     items = JsonConvert.DeserializeObject<List<T>>(json);
                    return items;
                }
                else
                {
                    return items;
                }
            }
            catch (Exception ex)
            {
                return items;
            }
        }
    }
}
