namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///  Book Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// The user interface from business layer
        /// </summary>
        private readonly IBookBL bookBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="bookBL">The user interface.</param>
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Added Book Details
        /// </returns>
        [HttpPost("Add")]
        public IActionResult AddBook(AddBookModel book)
        {
            try
            {
                var bookDetail = this.bookBL.AddBook(book);
                if (bookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = bookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>Updated Book Detail</returns>
        [HttpPut("Update")]
        public IActionResult UpdateBook(BookModel book)
        {
            try
            {
                var updatedBookDetail = this.bookBL.UpdateBook(book);
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Updated Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns> Book Deleted Message </returns>
        [HttpDelete("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(bookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Valid Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the book by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns> Book Detail Related to specific Book Id </returns>
        [HttpGet("{bookId}/Get")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetBookByBookId(bookId);
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the book.
        /// </summary>
        /// <returns> All Books Detail </returns>
        [HttpGet("Get")]
        public IActionResult GetBook()
        {
            try
            {
                var updatedBookDetail = this.bookBL.GetAllBooks();
                if (updatedBookDetail != null)
                {
                    return this.Ok(new { Success = true, message = "Book Detail Fetched Sucessfully", Response = updatedBookDetail });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
