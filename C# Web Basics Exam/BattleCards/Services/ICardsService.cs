using BattleCards.Models;
using BattleCards.ViewModels.Cards;
using System.Collections.Generic;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        int Add(CardAddInputModel inputModel);

        IEnumerable<Card> GetAll();

        Card GetById(int id);

        void DeleteById(int id);

        void AddToCollection(string userId, User user, int cardId, Card card);

        void RemoveFromCollection(string userId, User user, int cardId, Card card);

        IEnumerable<Card> GetAllForUser(string id);


    }
}
