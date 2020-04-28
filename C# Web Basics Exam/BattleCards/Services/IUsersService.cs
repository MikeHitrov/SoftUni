using BattleCards.Models;

namespace BattleCards.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        User GetUserById(string Id);

        void Register(string username, string email, string password);

        bool UsernameExists(string username);

        bool EmailExists(string email);

        string GetUsername(string id);
    }
}
