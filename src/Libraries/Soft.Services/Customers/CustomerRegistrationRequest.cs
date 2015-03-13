using Soft.Core.Domain.Customers;

namespace Soft.Services.Customers
{
    public class CustomerRegistrationRequest
    {
        public Customer Customer { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public PasswordFormat PasswordFormat { get; set; }
        public bool IsApproved { get; set; }

        public CustomerRegistrationRequest(Customer customer, string email, string username,
            string password, 
            PasswordFormat passwordFormat,
            bool isApproved = true)
        {
            Customer = customer;
            Email = email;
            Username = username;
            Password = password;
            PasswordFormat = passwordFormat;
            IsApproved = isApproved;
        }

        //public bool IsValid  
        //{
        //    get 
        //    {
        //        return (!CommonHelper.AreNullOrEmpty(
        //                    Email,
        //                    Password
        //                    ));
        //    }
        //}
    }
}
