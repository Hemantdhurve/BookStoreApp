using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public Addressmodel AddAddress(long userId, Addressmodel addressmodel);
        public bool DeleteAddress(long addressId);
        public Addressmodel RetriveAddress(long userId);
        public Addressmodel UpdateAddress(long userId, long addressId,Addressmodel addressmodel);
    }
}
