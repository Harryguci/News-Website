using System.Numerics;

namespace Domain.Domain.Common;

public interface IUserFeature
{
    string Username { get; }
    string Roles { get; }
    string Phone { get; }
}
public class UserFeature : IUserFeature
{
    public UserFeature(string username,
        string roles,
        string phone)
    {
        Username = username;
        Roles = roles;
        Phone = phone;
    }

    public string Username { get; }
    public string Roles { get; }
    public string Phone { get; }
}