using ASPNetCoreGraphQlServer.Models;

namespace ASPNetCoreGraphQlServer.GraphQl
{
    public class GraphQLMutation
    {
        public Order CreateOrder(Order order, int index, string action,
            [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters)
        {
            GraphQLQuery.Orders.Insert(index, order);
            return order;
        }
        public Order UpdateOrder(Order order, string action, string primaryColumnName, int primaryColumnValue,
            [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters)
        {
            Order updatedOrder = GraphQLQuery.Orders.Where(x => x.OrderID == primaryColumnValue).FirstOrDefault();
            updatedOrder.OrderID = order.OrderID;
            updatedOrder.EmployeeID = order.EmployeeID;
            updatedOrder.CustomerID = order.CustomerID;
            updatedOrder.Freight = order.Freight;
            updatedOrder.OrderDate = order.OrderDate;
            return updatedOrder;
        }
        public Order DeleteOrder(int primaryColumnValue, string action, string primaryColumnName,
            [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters)
        {
            Order deletedOrder = GraphQLQuery.Orders.Where(x => x.OrderID == primaryColumnValue).FirstOrDefault();
            GraphQLQuery.Orders.Remove(deletedOrder);
            return deletedOrder;
        }
        public List<Order> BatchUpdate(List<Order>? changed, List<Order>? added,
            List<Order>? deleted, string action, String primaryColumnName,
            [GraphQLType(typeof(AnyType))] IDictionary<string, object> additionalParameters, int? dropIndex)
        {
            if (changed != null && changed.Count > 0)
            {
                foreach (var changedOrder in (IEnumerable<Order>)changed)
                {
                    Order order = GraphQLQuery.Orders.Where(e => e.OrderID == changedOrder.OrderID).FirstOrDefault();
                    order.OrderID = changedOrder.OrderID;
                    order.CustomerID = changedOrder.CustomerID;
                    order.OrderDate = changedOrder.OrderDate;
                    order.Freight = changedOrder.Freight;
                }
            }
            if (added != null && added.Count > 0)
            {
                if (dropIndex != null)
                {
                    GraphQLQuery.Orders.InsertRange((int)dropIndex, added);
                }
                else {
                    foreach (var newOrder in (IEnumerable<Order>)added)
                    {
                        GraphQLQuery.Orders.Add(newOrder);
                    }
                }                
            }
            if (deleted != null && deleted.Count > 0)
            {
                foreach (var deletedOrder in (IEnumerable<Order>)deleted)
                {
                    GraphQLQuery.Orders.Remove(GraphQLQuery.Orders.Where(e => e.OrderID == deletedOrder.OrderID).FirstOrDefault());
                }
            }
            return GraphQLQuery.Orders;
        }

    }
}
