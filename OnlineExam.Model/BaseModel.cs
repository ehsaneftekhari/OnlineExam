namespace OnlineExam.Model
{
    public class BaseModel
    {
        public string Guid { get; set; } = new Guid().ToString();
        public int Id { get; set; }
    }
}
