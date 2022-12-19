﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public Addressmodel AddAddress(long userId, Addressmodel addressmodel);
        public bool DeleteAddress(long addressId);
    }
}
