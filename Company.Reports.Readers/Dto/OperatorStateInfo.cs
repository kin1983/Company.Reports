
namespace Company.Reports.Readers.Dto
{
  public class OperatorStateInfo
  {
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public string ProjectName { get; set; }
    public string OperatorName { get; set; }
    public string State { get; set; }
    public int Duration { get; set; }

   
  }
}
