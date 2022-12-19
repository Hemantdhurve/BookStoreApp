using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public string AddFeedback(long userId, FeedbackModel feedbackModel);
    }
}
