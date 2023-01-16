using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CustomerDetailsBL:ICustomerDetailsBL
    {
        public readonly ICustomerDetailsRL icustomerDetailsRL;
        public CustomerDetailsBL(ICustomerDetailsRL icustomerDetailsRL)
        {
            this.icustomerDetailsRL = icustomerDetailsRL;
        }

        public IEnumerable<CustomerDetailsModel> RetriveDetails(long userId)
        {
            try
            {
                return icustomerDetailsRL.RetriveDetails(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
