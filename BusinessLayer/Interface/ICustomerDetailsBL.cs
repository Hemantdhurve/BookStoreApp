using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICustomerDetailsBL
    {
        public IEnumerable<CustomerDetailsModel> RetriveDetails(long userId);
    }
}
