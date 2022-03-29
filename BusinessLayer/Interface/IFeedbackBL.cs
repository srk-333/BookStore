namespace BusinessLayer.Interface
{  
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    /// Interface Class for Business Layer
    /// </summary>
    public interface IFeedbackBL
    {
        /// <summary>
        /// Adds the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Added feedback Message</returns>
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId);

        /// <summary>
        /// Updates the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <returns>Updated feedback Message</returns>
        public string UpdateFeedback(FeedbackModel feedback, int userId, int feedbackId);

        /// <summary>
        /// Deletes the feedback.
        /// </summary>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True or False</returns>
        public bool DeleteFeedback(int feedbackId, int userId);

        /// <summary>
        /// Gets the records by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns> All Records From Feedback Table </returns>
        public List<GetFeedModel> GetRecordsByBookId(int bookId);
    }
}
