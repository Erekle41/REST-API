﻿using RestAPI.Data.Interfaces;
using RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Data.Repository
{
    public class WlogRepository : RepositoryBase<Wlog>, IWlogRepository
    {
        public WlogRepository(WebContext context) : base(context)
        {

        }
    }
}
