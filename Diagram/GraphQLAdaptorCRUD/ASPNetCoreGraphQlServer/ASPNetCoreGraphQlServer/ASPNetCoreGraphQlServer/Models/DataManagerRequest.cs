namespace ASPNetCoreGraphQlServer.Models
{
    public class DataManagerRequest
    {
        [GraphQLName("Skip")]
        public int Skip { get; set; }

        [GraphQLName("Take")]
        public int Take { get; set; }

        [GraphQLName("RequiresCounts")]
        public bool RequiresCounts { get; set; } = false;

        [GraphQLName("Params")]
        [GraphQLType(typeof(AnyType))]
        public IDictionary<string, object> Params { get; set; }


        [GraphQLName("Aggregates")]
        [GraphQLType(typeof(AnyType))]
        public List<Aggregate>? Aggregates { get; set; }

        [GraphQLName("Search")]
        public List<SearchFilter>? Search { get; set; }

        [GraphQLName("Sorted")]
        public List<Sort>? Sorted { get; set; }

        [GraphQLName("Where")]
        [GraphQLType(typeof(AnyType))]
        public List<WhereFilter>? Where { get; set; }

        [GraphQLName("Group")]
        public List<string>? Group { get; set; }

        [GraphQLName("antiForgery")]

        public string? antiForgery { get; set; }

        [GraphQLName("Table")]
        public string? Table { get; set; }

        [GraphQLName("IdMapping")]
        public string? IdMapping { get; set; }

        [GraphQLName("Select")]
        public List<string>? Select { get; set; }

        [GraphQLName("Expand")]
        public List<string>? Expand { get; set; }

        [GraphQLName("Distinct")]
        public List<string>? Distinct { get; set; }

        [GraphQLName("ServerSideGroup")]
        public bool? ServerSideGroup { get; set; }

        [GraphQLName("LazyLoad")]
        public bool? LazyLoad { get; set; }

        [GraphQLName("LazyExpandAllGroup")]
        public bool? LazyExpandAllGroup { get; set; }
    }

    public class Aggregate
    {
        [GraphQLName("Field")]
        public string Field { get; set; }

        [GraphQLName("Type")]
        public string Type { get; set; }
    }

    public class SearchFilter
    {
        [GraphQLName("Fields")]
        public List<string> Fields { get; set; }

        [GraphQLName("Key")]
        public string Key { get; set; }

        [GraphQLName("Operator")]
        public string Operator { get; set; }

        [GraphQLName("IgnoreCase")]
        public bool IgnoreCase { get; set; }
    }

    public class Sort
    {
        [GraphQLName("Name")]
        public string Name { get; set; }

        [GraphQLName("Direction")]
        public string Direction { get; set; }

        [GraphQLName("Comparer")]
        [GraphQLType(typeof(AnyType))]
        public object Comparer { get; set; }
    }

    public class WhereFilter
    {
        [GraphQLName("Field")]
        public string? Field { get; set; }

        [GraphQLName("IgnoreCase")]
        public bool? IgnoreCase { get; set; }

        [GraphQLName("IgnoreAccent")]
        public bool? IgnoreAccent { get; set; }

        [GraphQLName("IsComplex")]
        public bool? IsComplex { get; set; }

        [GraphQLName("Operator")]
        public string? Operator { get; set; }

        [GraphQLName("Condition")]
        public string? Condition { get; set; }

        [GraphQLName("value")]
        [GraphQLType(typeof(AnyType))]
        public object? value { get; set; }

        [GraphQLName("predicates")]
        public List<WhereFilter>? predicates { get; set; }
    }

    public class ReturnType<T>
    {
        public int Count { get; set; }

        public IEnumerable<T> Result { get; set; }

        [GraphQLType(typeof(AnyType))]
        public IDictionary<string, object> Aggregates { get; set; }
    }
}
