﻿namespace OnlineExam.Model.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string CreatorUserId { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Published { get; set; }
        public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
