using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL:IAddressBL
    {
		public readonly IAddressRL iaddressRL;

        public AddressBL(IAddressRL iaddressRL)
		{
			this.iaddressRL = iaddressRL;
		}
        public Addressmodel AddAddress(long userId, Addressmodel addressmodel)
        {
			try
			{
				return iaddressRL.AddAddress(userId, addressmodel);
			}
			catch (Exception)
			{

				throw;
			}
        }
        public bool DeleteAddress(long addressId)
		{
			try
			{
				return iaddressRL.DeleteAddress(addressId);
			}
			catch (Exception)
			{

				throw;
			}
		}


        public IEnumerable<Addressmodel> RetriveAddress(long userId)
        {
			try
			{
				return iaddressRL.RetriveAddress(userId);
			}
			catch (Exception)
			{

				throw;
			}
		}

        public Addressmodel UpdateAddress(long userId, long addressId,Addressmodel addressmodel)
		{
			try
			{
				return iaddressRL.UpdateAddress(userId, addressId, addressmodel);
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
