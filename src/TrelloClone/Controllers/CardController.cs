using Microsoft.AspNetCore.Mvc;
using TrelloClone.Services;
using TrelloClone.ViewModels;

namespace TrelloClone.Controllers
{
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }
        
        [HttpGet]
        public IActionResult Details(int id, int boardId)
        {
            var viewModel = _cardService.GetDetails(id);
            ViewBag.BoardId = boardId;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteCard(int id, int boardId)
        {
            if (!ModelState.IsValid) RedirectToAction("Index", "Board", new { id = boardId });

            _cardService.Delete(id);

            return RedirectToAction("Index", "Board", new { id = boardId });
        }

        [HttpPost]
        public IActionResult Update(CardDetails cardDetails)
        {
            _cardService.Update(cardDetails);

            TempData["Message"] = "Saved card Details";

            return RedirectToAction(nameof(Details), new { id = cardDetails.Id });
        }
    }
}