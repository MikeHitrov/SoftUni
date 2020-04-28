using BattleCards.Services;
using BattleCards.ViewModels.Cards;
using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;
        private readonly IUsersService usersService;

        public CardsController(ICardsService cardsService, IUsersService usersService)
        {
            this.cardsService = cardsService;
            this.usersService = usersService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardAddInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (inputModel.Name.Length < 5 || inputModel.Name.Length > 15)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.Description) || inputModel.Description.Length > 200)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.Image) || string.IsNullOrEmpty(inputModel.Keyword))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(inputModel.Attack.ToString()) || inputModel.Attack < 0 || string.IsNullOrEmpty(inputModel.Health.ToString()) || inputModel.Health < 0)
            {
                return this.View();
            }

            var cardId = this.cardsService.Add(inputModel);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cards = this.cardsService.GetAll();

            return this.View(cards);
        }

        public HttpResponse AddToCollection(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = User;
            var user = this.usersService.GetUserById(userId);
            var card = this.cardsService.GetById(id);

            this.cardsService.AddToCollection(userId, user, id, card);

            return this.Redirect("/Cards/Collection");
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cards = this.cardsService.GetAllForUser(User);

            return this.View(cards);
        }

        public HttpResponse RemoveFromCollection(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = User;
            var user = this.usersService.GetUserById(userId);
            var card = this.cardsService.GetById(id);

            this.cardsService.RemoveFromCollection(userId, user, id, card);

            return this.Redirect("/Cards/Collection");
        }
    }
}
