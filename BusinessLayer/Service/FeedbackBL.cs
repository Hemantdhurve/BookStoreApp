using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class FeedbackBL:IFeedbackBL
    {
        public readonly IFeedbackRL ifeedbackRL;
        public FeedbackBL(IFeedbackRL ifeedbackRL)
        {
            this.ifeedbackRL = ifeedbackRL;
        }

        public string AddFeedback(long userId, FeedbackModel feedbackModel)
        {
            try
            {
                return ifeedbackRL.AddFeedback(userId, feedbackModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<FeedbackModel> RetriveFeedback(long bookId)
        {
            try
            {
                return ifeedbackRL.RetriveFeedback(bookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
