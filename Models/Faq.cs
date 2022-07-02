namespace BanooClub.Models
{
    public class Faq : BaseEntity
    {
        public long FaqId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public long QuestionUserId { get; set; }
        public long AnswerUserId { get; set; }
    }
}
