
using OnlineExam.Model.Models;

namespace OnlineExam.Application.Contract.DTOs.QuestionDTOs
{
    public class ShowQuestionDTO 
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Text { get; set; }
        public string? ImageAddress { get; set; }
        public int Score { get; set; }
        public TimeSpan Duration { get; set; }
        public int Order { get; set; }
    }
}
