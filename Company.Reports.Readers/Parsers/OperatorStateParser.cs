
using Company.Reports.Readers.Dto;

namespace Company.Reports.Readers.Parsers
{
  public static class OperatorStateParser
  {
    private const int CountColums = 6;
    public static OperatorStateInfo Parse( string[] colums)
    {
      if (colums == null)
      {
        throw new ArgumentNullException( nameof(colums));
      }

      if (colums.Length < CountColums)
      {
        throw new ArgumentException("Неверное количество столбцов в файле");
      }

      var operatorStateInfo = new OperatorStateInfo
      {
        Begin = DateTime.Parse(colums[0]),
        End = DateTime.Parse(colums[1]),
        ProjectName = colums[2],
        OperatorName = colums[3],
        State = colums[4],
        Duration = Int32.Parse(colums[5])
      };


      return operatorStateInfo;

    }

    
  }
}
