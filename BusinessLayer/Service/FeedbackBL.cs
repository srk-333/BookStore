namespace BusinessLayer.Service
{ 
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;

    /// <summary>
    /// Service class for Interface  of Business Layer
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IFeedbackBL" />
    public class FeedbackBL : IFeedbackBL
    {
        /// <summary>
        /// The feedback RL
        /// </summary>
        private readonly IFeedbackRL feedbackRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackBL"/> class.
        /// </summary>
        /// <param name="feedbackRL">The feedback RL</param>
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        /// <summary>
        /// Adds the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Added feedback Message
        /// </returns>
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedback, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the feedback.
        /// </summary>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// True or False
        /// </returns>
        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                return this.feedbackRL.DeleteFeedback(feedbackId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <returns>
        /// Updated feedback Message
        /// </returns>
        public string UpdateFeedback(FeedbackModel feedback, int userId, int feedbackId)
        {
            try
            {
                return this.feedbackRL.UpdateFeedback(feedback, userId, feedbackId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the records by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns> All Records From Feedback Table </returns>
        public List<GetFeedModel> GetRecordsByBookId(int bookId) 
        {
            try
            {
                return this.feedbackRL.GetRecordsByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
