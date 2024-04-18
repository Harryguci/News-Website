using NewsWebsite.Domain.Dtos;
using NewsWebsite.Domain.Interfaces;

namespace NewsWebsite.Domain.Implement;

public class NewsPostEntity : INewsPost
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<string> Images { get; set; } = [];
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public PostStatusEnum Status { get; set; }
    public bool Accepted { get; set; } = false;
    public List<CategoryEnum> Categories { get; set; } = [];

    public INewsPost CreateOne(NewsPostDto newsPostDto)
    {
        return new NewsPostEntity()
        {
            Title = newsPostDto.Title,
            Content = newsPostDto.Content,
            CreateAt = newsPostDto.CreateAt,
            Status = newsPostDto.Status,
            Accepted = newsPostDto.Accepted,
            Categories = newsPostDto.Categories,
        };
    }
}
