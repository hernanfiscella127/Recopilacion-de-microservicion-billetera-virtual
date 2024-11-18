﻿using Domain.Interfaces;

namespace Domain.Models
{
    public class StateAccount : ICommonObject
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<AccountModel> Accounts { get; set; }
    }
}