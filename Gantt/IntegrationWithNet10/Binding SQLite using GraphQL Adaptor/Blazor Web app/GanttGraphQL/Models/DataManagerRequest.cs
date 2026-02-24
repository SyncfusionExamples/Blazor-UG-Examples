using HotChocolate;
using Syncfusion.Blazor.Data;
using System.Collections.Generic;

namespace GanttGraphQL.Models
{
    /// <summary>
    /// Represents the input structure for data manager requests from the Gantt Chart.
    /// Contains all parameters needed for data operations like filtering, sorting, and searching.
    /// </summary>
    public class DataManagerRequestInput
    {
        /// <summary>
        /// Number of records to skip from the beginning.
        /// Example: Skip=10 means start from the 11th record.
        /// </summary>
        [GraphQLName("Skip")]
        public int Skip { get; set; }

        /// <summary>
        /// Number of records to retrieve.
        /// Example: Take=10 means retrieve 10 records per page.
        /// </summary>
        [GraphQLName("Take")]
        public int Take { get; set; }

        /// <summary>
        /// Indicates whether the total record count should be calculated.
        /// Set to true when pagination requires knowing the total number of records.
        /// </summary>
        [GraphQLName("RequiresCounts")]
        public bool RequiresCounts { get; set; } = false;

        /// <summary>
        /// Additional custom parameters sent from the client.
        /// Used for advanced scenarios requiring extra metadata.
        /// </summary>
        [GraphQLName("Params")]
        [GraphQLType(typeof(AnyType))]
        public IDictionary<string, object> Params { get; set; }

        /// <summary>
        /// Collection of search expressions used for keyword matching.
        /// Example: searching across task name, duration, or other fields.
        /// </summary>
        [GraphQLName("Search")]
        public List<SearchFilter>? Search { get; set; }

        /// <summary>
        /// Collection of sorting instructions.
        /// Supports multi-column sorting (e.g., sort by StartDate, then by TaskID).
        /// </summary>
        [GraphQLName("Sorted")]
        public List<Sort>? Sorted { get; set; }

        /// <summary>
        /// Collection of filtering conditions (where clauses).
        /// Supports operators like equals, contains, greater-than, etc.
        /// </summary>
        [GraphQLName("Where")]
        [GraphQLType(typeof(AnyType))]
        public List<WhereFilter>? Where { get; set; }

        /// <summary>
        /// Table name associated with the request.
        /// Commonly unused but included for DataManager compatibility.
        /// </summar
        [GraphQLName("Table")]
        public string? Table { get; set; }

        /// <summary>
        /// List of field names to select from the dataset.
        /// Useful when projecting only selected columns.
        /// </summary>
        [GraphQLName("Select")]
        public List<string>? Select { get; set; }

        /// <summary>
        /// Indicates whether server‑side grouping is enabled.
        /// Required for hierarchical data scenarios.
        /// </summary>
        [GraphQLName("ServerSideGroup")]
        public bool? ServerSideGroup { get; set; }

        /// <summary>
        /// Indicates whether lazy loading is enabled for hierarchical data.
        /// Used mainly by Gantt to load child records on demand.
        /// </summary>
        [GraphQLName("LazyLoad")]
        public bool? LazyLoad { get; set; }

        /// <summary>
        /// Indicates whether all groups should be expanded during lazy loading.
        /// Used when the UI expands grouped records automatically.
        /// </summary>
        [GraphQLName("LazyExpandAllGroup")]
        public bool? LazyExpandAllGroup { get; set; }
    }
}