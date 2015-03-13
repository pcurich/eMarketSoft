﻿using System.Collections.Generic;

namespace Soft.Services.Customers 
{
    public class PasswordChangeResult
    {
        public IList<string> Errors { get; set; }

        public PasswordChangeResult() 
        {
            Errors = new List<string>();
        }

        public bool Success
        {
            get { return (Errors.Count == 0); }
        }

        public void AddError(string error) 
        {
            Errors.Add(error);
        }
    }
}
