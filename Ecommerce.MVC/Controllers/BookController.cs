using Ecommerce.Application.Services;
using Ecommerce.DTOs.Book;
using Ecommerce.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace Ecommerce.MVC.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookServices;

        private readonly IAuthorService _authorServices;
        public BookController(IBookService BookService, IAuthorService AuthorServices)
        {
            _bookServices = BookService;
            _authorServices = AuthorServices;
        }
        [Authorize]
        // GET: BookController
        public async Task<ActionResult> Index()
        {
            var prods = await _bookServices.GetAllPagination(5, 1);
            return View(prods);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookController/Create
        public async Task<ActionResult> Create()
        {
            var cards = await _authorServices.GetAllAuthors();
            ViewBag.authors = cards.Entities.ToList();

            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUpdateDeleteDto Book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Res = await _bookServices.Create(Book);
                    if (Res.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = Res.Message;
                        return View(Book);
                    }
                }
                else
                {
                    return View(Book);

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {

                var book = await _bookServices.GetOne(id);


                if (book != null)
                {
                    var authors = await _authorServices.GetAllAuthors();
                    ViewBag.Authors = authors.Entities.ToList();


                    return View(book);
                }
                else
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while processing your request.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: BookController/Edit/5
        [HttpPost]

        public async Task<ActionResult> Edit(int id, CreateUpdateDeleteDto Book)
        {

            var authors = await _authorServices.GetAllAuthors();
            ViewBag.Authors = authors.Entities.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _bookServices.UpdateAsync(Book);

                    if (result.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = result.Message;
                        return View(Book);
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid model state.";
                    return View(Book);
                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "An error occurred while processing your request.";
                return View(Book);
            }
        }


        // POST: BookController/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var prod = await _bookServices.GetOne(id);
                var Res = await _bookServices.SoftDelete(prod);
                if (Res.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = Res.Message;
                    return RedirectToAction(nameof(Index));
                }


            }
            catch
            {
                return View();
            }
        }
        public async Task<IActionResult> AddCard(int Id)
        {


            if (Id == 0)
            {
                return BadRequest("Invalid list of IDs");
            }

            // هات الكوكي او اعمل وحده جديده
            var cookie = Request.Cookies["BookListIDs"];
            List<int> existingIds = new List<int>();
            if (!string.IsNullOrEmpty(cookie))
            {
                // DeserializeObject بتلغي تسلسل الايديهات
                existingIds = JsonConvert.DeserializeObject<List<int>>(cookie);
            }
            existingIds.Add(Id);

            List<int> uniqueIds = existingIds.Distinct().ToList(); // بتمسح المتكرر

            // بتعيد تسلسل الايديهات مره تاني في الجيسون
            string serializedIds = JsonConvert.SerializeObject(uniqueIds);
            // هنحط الليست دي فالكوكيز
            Response.Cookies.Append("BookListIDs", serializedIds);


            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> AddCard(int id)
        //{
        //    try
        //    {
        //        var Book = await _bookServices.GetOne(id);

        //        if (Book == null)
        //        {
        //            return NotFound("Book not found.");
        //        }

        //        var session = HttpContext.Session;

        //        // Retrieve cart items from session (or create an empty list)
        //        List<CardDto> card;
        //        if (session.TryGetValue("card", out var cardBytes))
        //        {
        //            card = JsonSerializer.Deserialize<List<CardDto>>(cardBytes);
        //        }
        //        else
        //        {
        //            card = new List<CardDto>();
        //        }

        //        // Check for existing Book in cart
        //        var existingItem = card.FirstOrDefault(item => item.Id == id);

        //        if (existingItem != null)
        //        {
        //            //existingItem.Quantity++;
        //        }
        //        else
        //        {
        //            card.Add(new CardDto
        //            {
        //                Id = id,
        //                Name = Book.Name,
        //                Price = Book.Price,
        //                Quantity = 1
        //            });
        //        }

        //        // Store updated cart in session
        //        session.SetString("card", JsonSerializer.Serialize(card));

        //        // Redirect to cart view or provide success message
        //        return RedirectToAction(nameof(Index));// Or display a success message here
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions gracefully
        //        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
        //}
        //public async Task<ActionResult> AddCard()
        //{
        //    var session = HttpContext.Session;

        //    if (session.TryGetValue("card", out var cardBytes))
        //    {
        //        var cardItems = JsonSerializer.Deserialize<List<GetAllBooksDTO>>(cardBytes);
        //        // Use cartItems in your view model to display cart items
        //        return View("Card", cardItems); // Replace "Cart" with your actual cart view name
        //    }

        //    return View("Card", new List<CardDto>()); // Handle empty cart scenario
        //}
        //public async Task<IActionResult> AddCard(int id)
        //{
        //    var Book = await _bookServices.GetOne(id);

        //    if (Book == null)
        //    {
        //        return NotFound("Book not found.");
        //    }

        //    CookieOptions cookieOptions = new CookieOptions();
        //    cookieOptions.Expires = DateTimeOffset.Now.AddDays(1);

        //    Response.Cookies.Append("id", id.ToString(), cookieOptions);//persisste cookie
        //    Response.Cookies.Append("Name", Book.Name, cookieOptions);//persisste cookie

        //    Response.Cookies.Append("id", id.ToString());//session cookie
        //    Response.Cookies.Append("Name", Book.Name);//persisste cookie

        //    return Content("Cookie saves");
        //}
    }
}
