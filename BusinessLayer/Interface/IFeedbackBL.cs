using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public string AddFeedback(long userId, FeedbackModel feedbackModel);
        public IEnumerable<FeedbackModel> RetriveFeedback(long bookId);
    }
}
