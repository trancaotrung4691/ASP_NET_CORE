﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Systems.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNuber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
