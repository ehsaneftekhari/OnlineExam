﻿namespace OnlineExam.Application.Contract.DTOs.SectionDTOs
{
    public class ShowSectionDTO
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Title { get; set; } = null!;
        public int Order { get; set; }
        public bool RandomizeQuestions { get; set; }
    }
}
