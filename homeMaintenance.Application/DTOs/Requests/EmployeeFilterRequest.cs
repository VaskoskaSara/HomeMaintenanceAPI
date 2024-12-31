public class EmployeeFilterRequest
{
    public string[]? Cities { get; set; }
    public int? Price { get; set; }
    public int? Experience { get; set; }
    public bool? ExcludeByContract { get; set; }
    public Guid[] CategoryIds { get; set; }
}
