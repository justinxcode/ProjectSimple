using Telerik.DataSource;

namespace ProjectSimple.Application.Models;

public class DataEnvelope<T>
{
    /// Use this when there is data grouping
    public List<AggregateFunctionsGroup>? GroupedData { get; set; }

    /// Use this when there is no data grouping and the response is flat data, like in the common case.
    public List<T>? CurrentPageData { get; set; }

    /// Always set this to the total number of records.
    public int TotalItemCount { get; set; }
}