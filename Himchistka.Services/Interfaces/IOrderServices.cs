﻿using Himchistka.Data.Entities;
using Himchistka.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Himchistka.Services.Interfaces
{
    public interface IOrderServices
    {
        public Task<DTOUpsertOrder> UpsertOrder(DTOUpsertOrder model);
    }
}