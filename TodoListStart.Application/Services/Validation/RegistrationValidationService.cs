using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.Models.Auth;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService
    {
        public virtual List<string> ValidateRegistrationUser(RegistrationUser user)
        {
            var errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                errorMessages.Add("Name must be filled");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                errorMessages.Add("Email must be filled");
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                errorMessages.Add("Password must be filled");
            }

            if (string.IsNullOrWhiteSpace(user.ConfirmPassword))
            {
                errorMessages.Add("Confirm Password must be filled");
            }

            if (!user.Password.Equals(user.ConfirmPassword))
            {
                errorMessages.Add("Password and Confirm Password must be equals");
            }

            return errorMessages;
        }
    }
}
