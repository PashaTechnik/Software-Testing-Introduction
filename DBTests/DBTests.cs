using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using CsvHelper.Configuration;
using DataLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization.Internal;
using Npgsql;
using Xunit;
namespace Tests
{
    public class Menu
    {
        public int PositionId;
        public string Dish;
        public int? Size;
        public int? Price;
    }

    public class DBTests
    {
        public String ConnectionString;

        public DBTests()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            ConnectionString = configuration.GetConnectionString("myDb");
        }

        [Fact]
        public void JoinDataBaseTest()
        {
            using (var db = new RestaurantContext())
            {
                var menu = db.Menu.Join(db.Dish,
                    p => p.Dishid,
                    c => c.Dishid, (p, c) => new Menu()
                    {
                        PositionId = p.Positionid,
                        Dish = c.Name,
                        Size = p.Size,
                        Price = p.Price
                    }).OrderBy(x => x.Price).ToList();
                
                var logs = new List<Menu>();
                CsvHandler<Menu,MenuMap>.CsvEx(menu,"/Users/admin/Desktop/test.csv");
                var records = CsvHandler<Menu,MenuMap>.ReadCsv("/Users/admin/Desktop/expected result.csv");
                for (int i = 0; i < menu.Count; i++)
                {
                    if (menu[i].Dish != records[i].Dish 
                        || menu[i].Price != records[i].Price
                        || menu[i].Size != records[i].Size 
                        || menu[i].PositionId != records[i].PositionId )
                    {
                        logs.Add(records[i]);
                    }
                }
                CsvHandler<Menu,MenuMap>.CsvEx(logs,"/Users/admin/Desktop/log.csv");
                Assert.Empty(logs);
            }
            
        }
        
        [Fact]
        public void WhereDataBaseOrdersTest()
        {
            using (var db = new RestaurantContext())
            {
                var order = db.Orders.Where(x => x.Price<2500).ToList();
                
                var logs = new List<Orders>();
                CsvHandler<Orders,OrderMap>.CsvEx(order,"/Users/admin/Desktop/test2.csv");
                var records = CsvHandler<Orders,OrderMap>.ReadCsv("/Users/admin/Desktop/expected result 2.csv");
                for (int i = 0; i < order.Count; i++)
                {
                    if (order[i].Orderid != records[i].Orderid 
                        || order[i].Price != records[i].Price 
                        || order[i].Clientname != records[i].Clientname )
                    {
                        logs.Add(records[i]);
                    }
                }
                CsvHandler<Orders,OrderMap>.CsvEx(logs,"/Users/admin/Desktop/log 2.csv");
                Assert.Empty(logs);
            }
        }
    }
}