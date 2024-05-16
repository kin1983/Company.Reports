using Company.Reports.Readers.Dto;
using Company.Reports.Readers.Interfaces;
using Company.Reports.Readers.Parsers;

namespace Company.Reports.Readers.Logic
{
  public class CsvDataReader : ICsvFileReader
  {
    private readonly string _fileName;
    private readonly int _countSkipRows;

    public CsvDataReader(string fileName,int countSkipRows= 0)
    {

      if (String.IsNullOrWhiteSpace(fileName))
      {
        throw new ArgumentNullException(nameof(fileName));
      }

      _fileName = fileName;
      _countSkipRows = countSkipRows;
    }

    public IEnumerable<OperatorStateInfo> Read()
    {
      if (!File.Exists(_fileName))
      {
        throw new FileNotFoundException();
      }

      if (_countSkipRows < 0)
      {
        throw new ArgumentException($"Значение {nameof(_countSkipRows)} не должно быть отрицательным");
      }

      var lines = File.ReadLines(_fileName);
      var items = lines.Select(p => p.Split(';'))
        .Skip(_countSkipRows)
        .Select(OperatorStateParser.Parse);

      return items;
    }
  }
}

