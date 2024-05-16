namespace Company.Reports.Logic.Dto
{
    public class DurationOperatorInStateInfo
    {
        public string OperatorName { get; set; }
        public IEnumerable<DurationInStateInfo> DurationInStates { get; set; }
    }
}
