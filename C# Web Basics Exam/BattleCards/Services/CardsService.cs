namespace BattleCards.Services
{
    using BattleCards.Data;
    using BattleCards.Models;
    using BattleCards.ViewModels.Cards;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext dbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(CardAddInputModel inputModel)
        {
            Card card = new Card
            {
                Name = inputModel.Name,
                ImageUrl = inputModel.Image,
                Keyword = inputModel.Keyword,
                Description = inputModel.Description,
                Attack = inputModel.Attack,
                Health = inputModel.Health,
            };

            this.dbContext.Cards.Add(card);
            this.dbContext.SaveChanges();

            return card.Id;
        }

        public void DeleteById(int id)
        {
            var card = this.GetById(id);
            this.dbContext.Cards.Remove(card);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<Card> GetAll()
        {
            return this.dbContext.Cards.Select(x => new Card
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Description = x.Description,
                Keyword = x.Keyword,
                Health = x.Health,
                Attack = x.Attack
            })
                 .ToArray();
        }

        public Card GetById(int id)
            => this.dbContext.Cards.FirstOrDefault(x => x.Id == id);

        public void AddToCollection(string userId, User user, int cardId, Card card)
        {
            var userCards = this.dbContext.UsersCards.Select(x => new UserCard
            {
                UserId = x.UserId,
                User = x.User,
                CardId = x.CardId,
                Card = x.Card
            }).Where(uc => uc.UserId == userId).ToList();

            bool isAlreadyAdded = false;


            foreach (var userCard in userCards)
            {
                if (userCard.CardId == cardId)
                    isAlreadyAdded = true;
            }

            if(isAlreadyAdded == false)
            {
                this.dbContext.UsersCards.Add(new UserCard { 
                    UserId = userId,
                    CardId = cardId,
                    Card = this.GetById(cardId)
                });

                this.dbContext.SaveChanges();
            }
        }

        public IEnumerable<Card> GetAllForUser(string id)
        {
            var userCards = this.dbContext.UsersCards.Select(x => new UserCard
            {
                UserId = x.UserId,
                User = x.User,
                CardId = x.CardId,
                Card = x.Card
            }).Where(uc => uc.UserId == id).ToList();

            var result = new List<Card>();

            foreach (var userCard in userCards)
            {
                var card = this.GetById(userCard.CardId);

                result.Add(new Card
                {
                    Id = card.Id,
                    Name = card.Name,
                    ImageUrl = card.ImageUrl,
                    Description = card.Description,
                    Keyword = card.Keyword,
                    Health = card.Health,
                    Attack = card.Attack
                });
            }

            return result.ToArray();
        }

        public void RemoveFromCollection(string userId, User user, int cardId, Card card)
        {
            var userCards = this.dbContext.UsersCards.Select(x => new UserCard
            {
                UserId = x.UserId,
                User = x.User,
                CardId = x.CardId,
                Card = x.Card
            }).Where(uc => uc.UserId == userId).ToList();

            var cardToRemove = userCards.Find(uc => uc.CardId == cardId);

            this.dbContext.UsersCards.Remove(cardToRemove);
            this.dbContext.SaveChanges();
        }
    }
}
