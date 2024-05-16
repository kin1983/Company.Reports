using Company.Reports.Logic.Dto;
using Company.Reports.Readers.Dto;
using Company.Reports.Readers.Interfaces;

namespace Company.Reports.Logic.Logic
{
  public class ReportCreator
  {
    private readonly ICsvFileReader _fileReader;

    public ReportCreator(ICsvFileReader fileReader)
    {
      _fileReader = fileReader;
    }

    public IEnumerable<MaxSessionsByDaysInfo> GetMaxSessionsByDays()
    {
      var items = new List<MaxSessionsByDaysInfo>();

      var sourseItems = _fileReader.Read();

      var grouped = sourseItems.GroupBy(p => p.Begin.Date);
      var groupedList = grouped.ToList();

      var listSessionsNotEnded = new List<OperatorStateInfo>();
      foreach (var item in groupedList)
      {
        var itemsByDay = groupedList.Where(p => p.Key.Date == item.Key.Date).SelectMany(a => a);

        var max = 0;
        var itemsByDayList = itemsByDay.OrderBy(p => p.Begin).ThenBy(p => p.End);
        foreach (var itemByDay in itemsByDayList)
        {
          listSessionsNotEnded.Add(itemByDay);
          listSessionsNotEnded.RemoveAll(p => p.End < itemByDay.Begin);

          if (listSessionsNotEnded.Count > max)
          {
            max = listSessionsNotEnded.Count;
          }
        }
        listSessionsNotEnded.Clear();
        items.Add(new MaxSessionsByDaysInfo { Date = item.Key, Count = max });
      }

      return items;
    }
    
    public IEnumerable<DurationOperatorInStateInfo> GetOperatorsDutationInStates()
    {

      var sourceItems = _fileReader.Read();

      var grouped = sourceItems.GroupBy(p => p.OperatorName)
        .Select(p => new DurationOperatorInStateInfo
        {
          OperatorName = p.Key,
          DurationInStates = p.GroupBy(p => p.State)
            .Select(p => new DurationInStateInfo()
            {
              State = p.Key,
              Duration = p.Sum(a => a.Duration)
            })
        });


      return grouped;
    }

  }
}
