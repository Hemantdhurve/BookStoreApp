using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public Addressmodel AddAddress(long userId, Addressmodel addressmodel);
        public bool DeleteAddress(long addressId);
        public IEnumerable<Addressmodel> RetriveAddress(long userId);
        public Addressmodel UpdateAddress(long userId, long addressId, Addressmodel addressmodel);
    }
}
