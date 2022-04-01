namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///  Feedback Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        /// <summary>
        /// The feedback BL
        /// </summary>
        private readonly IFeedbackBL feedbackBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackController"/> class.
        /// </summary>
        /// <param name="feedbackBL">The feedback BL.</param>
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        /// <summary>
        /// Adds the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <returns>Added feedback Message</returns>
        [Authorize(Roles = Role.User)]
        [HttpPost("Add")]
        public IActionResult AddFeedback(FeedbackModel feedback)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.feedbackBL.AddFeedback(feedback, userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback Added For this Book Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Provide Different BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the feedback.
        /// </summary>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <returns>True Or False</returns>
        [Authorize(Roles = Role.User)]
        [HttpDelete("Delete")]
        public IActionResult DeleteFeedback(int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.feedbackBL.DeleteFeedback(feedbackId, userId))
                {
                    return this.Ok(new { Status = true, Message = "Deleted From Feedback" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Updates the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <returns> Updated feedback Message</returns>
        [Authorize(Roles = Role.User)]
        [HttpPut("Update")]
        public IActionResult UpdateFeedback(FeedbackModel feedback, int feedbackId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.feedbackBL.UpdateFeedback(feedback, userId, feedbackId);
                if (result.Equals("Feedback Updated For this Book Successfully"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gets the records by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// All Records From Feedback Table
        /// </returns>
        [HttpGet("Get")]
        public IActionResult GetRecords(int bookId)
        {
            try
            {
                var result = this.feedbackBL.GetRecordsByBookId(bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback Records Fetched Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = " Provide Different BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
