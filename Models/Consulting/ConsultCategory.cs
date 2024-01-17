namespace BanooClub.Models.Consulting
{
    public class ConsultCategory : BaseEntity
    {
        private ConsultCategory()
        {

        }
        public ConsultCategory(string title, long? parentId = null)
        {
            Title = title;
            ParentId = parentId;
        }
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }

    }
}
