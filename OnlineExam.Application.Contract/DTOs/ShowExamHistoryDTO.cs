using System.Data;

namespace OnlineExam.Application.Contract.DTOs
{
    public class ShowExamHistoryDTO
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public TimeSpan Period { get; set; }
    }
}
