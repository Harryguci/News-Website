namespace NewsWebsite.Domain.Dtos
{
    public enum PostStatusEnum
    {
        Normal, New, Removed
    }

    public enum CategoryEnum
    {
        Bussiness,
        Environment,
        Weather,
        Fashion,
        Family,
        Cooking
    }

    public class NewsPostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<string> Images { get; set; } = [];
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public PostStatusEnum Status { get; set; }
        public bool Accepted { get; set; } = false;
        public List<CategoryEnum> Categories { get; set; } = [];
    }
}
