using NewsWebsite.Domain.Dtos;
using NewsWebsite.Domain.Entities;

namespace NewsWebsite.Domain.Interfaces
{
    public interface INewsPost
    {
        INewsPost CreateOne(NewsPostDto newsPostDto);
    }
}
