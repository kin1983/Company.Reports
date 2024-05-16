
using Company.Reports.Readers.Dto;

namespace Company.Reports.Readers.Interfaces
{
  public interface ICsvFileReader
  {
    IEnumerable<OperatorStateInfo> Read();

  }
}
