
using ASPNetCoreGraphQlServer.Models;
using System.Collections;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ASPNetCoreGraphQlServer.GraphQl
{
    public class GraphQLQuery
    {

        #region OrdersData Resolver
        public ReturnType<Order> OrdersData(DataManagerRequest dataManager)
        {
            IEnumerable<Order> result = Orders;
            int count = result.Count();
            return dataManager.RequiresCounts ? new ReturnType<Order>() { Result = result, Count = count } : new ReturnType<Order>() { Result = result };
        }

        public static List<Order> Orders { get; set; } = GetOrdersList();

        private static List<Order> GetOrdersList()
        {
            var data = new List<Order>();
                data.Add(new Order() { OrderID =  1, EmployeeID = "Production Manager",  CustomerID = "", OrderDate = new DateTime(2023, 08, 23), Freight = 5.7 * 2, Address = new CustomerAddress() { ShipCity = "Berlin", ShipCountry = "Denmark" }  });
                data.Add(new Order() { OrderID =  2, EmployeeID = "Control Room", CustomerID = "1", OrderDate = new DateTime(1994, 08, 24), Freight = 6.7 * 2, Address = new CustomerAddress() { ShipCity = "Madrid", ShipCountry = "Brazil" } });
                data.Add(new Order() { OrderID = 3, EmployeeID = "Plant Operator", CustomerID = "1", OrderDate = new DateTime(1993, 08, 25), Freight = 7.7 * 2, Address = new CustomerAddress() { ShipCity = "Cholchester", ShipCountry = "Germany" } });
                data.Add(new Order() { OrderID =  4, EmployeeID = "Foreman", CustomerID = "2", OrderDate = new DateTime(1992, 08, 26), Freight = 8.7 * 2, Address = new CustomerAddress() { ShipCity = "Marseille", ShipCountry = "Austria" } });
                data.Add(new Order() { OrderID =  5, EmployeeID = "Foreman", CustomerID = "3", OrderDate = new DateTime(1991, 08, 27), Freight = 9.7 * 2, Address = new CustomerAddress() { ShipCity = "Tsawassen", ShipCountry = "Switzerland" } });
                data.Add(new Order() { OrderID = 6, EmployeeID = "Craft Personnel", CustomerID = "4", OrderDate = new DateTime(1991, 08, 27), Freight = 9.7 * 2, Address = new CustomerAddress() { ShipCity = "Tsawassen", ShipCountry = "Switzerland" } });
                data.Add(new Order() { OrderID = 7, EmployeeID = "Craft Personnel", CustomerID = "4", OrderDate = new DateTime(1991, 08, 27), Freight = 9.7 * 2, Address = new CustomerAddress() { ShipCity = "Tsawassen", ShipCountry = "Switzerland" } });
                data.Add(new Order() { OrderID = 8, EmployeeID = "Craft Personnel", CustomerID = "5", OrderDate = new DateTime(1991, 08, 27), Freight = 9.7 * 2, Address = new CustomerAddress() { ShipCity = "Tsawassen", ShipCountry = "Switzerland" } });
                data.Add(new Order() { OrderID = 9, EmployeeID = "Craft Personnel", CustomerID = "5", OrderDate = new DateTime(1991, 08, 27), Freight = 9.7 * 2, Address = new CustomerAddress() { ShipCity = "Tsawassen", ShipCountry = "Switzerland" } });
            return data;
        }
        #endregion

    }


}
