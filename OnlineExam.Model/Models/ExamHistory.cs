using System.Data;

namespace OnlineExam.Model.Models
{
    public class ExamHistory : Exam
    {
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
