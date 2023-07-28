namespace OnlineExam.Application.Contract.DTOs.ExamDTOs
{
    public class PagingModel<T> where T : class
    {
        public PagingModel(T data, long count)
        {
            Data = data;
            Count = count;
        }

        public T Data { get; set; } = null!;
        public long Count { get; set; }
    }
}
