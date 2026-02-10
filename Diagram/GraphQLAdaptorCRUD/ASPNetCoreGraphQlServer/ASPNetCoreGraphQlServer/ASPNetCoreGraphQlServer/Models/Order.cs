namespace ASPNetCoreGraphQlServer.Models
{
    public class Order
    {
        [GraphQLName("OrderID")]
        public int? OrderID { get; set; }
        
        [GraphQLName("CustomerID")]
        public string? CustomerID { get; set; }

        [GraphQLName("EmployeeID")]
        public string EmployeeID { get; set; }        

        [GraphQLName("OrderDate")]
        public DateTime? OrderDate { get; set; }

        [GraphQLName("Freight")]
        public double? Freight { get; set; }

        [GraphQLName("Address")]
        public CustomerAddress? Address { get; set; }
    }

    public class CustomerAddress
    {
        [GraphQLName("ShipCity")]
        public string ShipCity { get; set; }

        [GraphQLName("ShipCountry")]
        public string ShipCountry { get; set; }
    }
}
