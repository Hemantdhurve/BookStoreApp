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
    }
}
