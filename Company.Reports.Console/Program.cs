
using System.Diagnostics;
using Company.Reports.Logic.Logic;
using Company.Reports.Readers.Logic;


if (args.Length == 0)
{
  Console.WriteLine("В параметрах не передано имя файла");
  return;
}

var fileName = args[0];
if (String.IsNullOrWhiteSpace(fileName))
{
  Console.WriteLine("Ошибка в имени файла");
  return;
}


var reader = new CsvDataReader(fileName, countSkipRows: 1);
var reportCreator = new ReportCreator(reader);

Console.WriteLine("Отчет \"Максимальное число активных сессий\"");
var stopWatch = new Stopwatch();
stopWatch.Start();
var itemsMaxSessionByDays = reportCreator.GetMaxSessionsByDays();

foreach (var item in itemsMaxSessionByDays)
{
  Console.WriteLine($"{item.Date:dd.MM.yyyy} - {item.Count}");
}
stopWatch.Stop();

Console.WriteLine($"Время выполнения:{stopWatch.Elapsed}");

Console.WriteLine(new string('-', 50));
Console.WriteLine("Отчет \"Суммарное время состояний операторов\"");
stopWatch.Reset();
stopWatch.Start();
var items = reportCreator.GetOperatorsDutationInStates();


foreach (var item in items)
{
  Console.Write($"{item.OperatorName} ");
  foreach (var itemDurationState in item.DurationInStates)
  {
    Console.Write($"{itemDurationState.State}:{itemDurationState.Duration} ");
  }

  Console.WriteLine();
}

stopWatch.Stop();

Console.WriteLine($"Время выполнения:{stopWatch.Elapsed}");


