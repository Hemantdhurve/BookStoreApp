using CommonLayer.Model;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICustomerDetailsRL
    {
        public IEnumerable<CustomerDetailsModel> RetriveDetails(long userId);
    }
}
