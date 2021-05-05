using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using DataLayer;

namespace Tests
{
    public class MenuMap : ClassMap<Menu>
    {
        public MenuMap()
        {
            Map(m => m.PositionId).Name("PositionId");
            Map(m => m.Dish).Name("Dish");
            Map(m => m.Size).Name("Size");
            Map(m => m.Price).Name("Price");
        }
    }
    public class OrderMap : ClassMap<Orders>
    {
        public OrderMap()
        {
            Map(m => m.Orderid).Name("Orderid");
            Map(m => m.Clientname).Name("Clientname");
            Map(m => m.Price).Name("Price");
        }
    }
    
    public class CsvHandler<T1,T2> where T2 : ClassMap
    {
        public static void CsvEx(IList records,string path)
        {

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<T2>();
                csv.WriteRecords(records);
            }
        }

        public static List<T1> ReadCsv(string path)
        {
            var records = new List<T1>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<T2>();
                records = csv.GetRecords<T1>().ToList();
            }
            return records;
        }
    }
}