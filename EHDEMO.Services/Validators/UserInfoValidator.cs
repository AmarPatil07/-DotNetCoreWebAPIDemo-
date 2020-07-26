using EHDEMO.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EHDEMO.Service.Validators
{
    public  class UserInfoValidator : AbstractValidator<UserInfo>
    {
        public UserInfoValidator()
        {
            RuleFor(c => c)
               .NotNull()
               .OnAnyFailure(x =>
               {
                   throw new ArgumentNullException("Can't found the object.");
               });

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name should not longer than 50 characters.")
                .NotNull().WithMessage("First Name is required.");

            RuleFor(c => c.LastName)
               .NotEmpty().WithMessage("Last Name is required.")
               .MaximumLength(100).WithMessage("Last Name should not longer than 100 characters.")
               .NotNull().WithMessage("Last Name is required.");

            RuleFor(c => c.PhoneNumber)
               .NotEmpty().WithMessage("Phone number is required")
               .Must(Validatelength).WithMessage("Phone number length should be 10")
               .Must(ValidateOnlyNumbers).WithMessage("Phone number must have only numbers");

            RuleFor(c => c.Email)
             .NotEmpty().WithMessage("Email address is required")
             .MaximumLength(50).WithMessage("Email address length should be 50")
             .Must(ValidateEmailAddress).WithMessage("Please enter valid email address");

        }

        //Phone number length
        private bool Validatelength(string number)
        {

            if (number.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateOnlyNumbers(string number)
        {
            return Regex.IsMatch(number, @"^[0-9]+$");

        }

        private bool ValidateEmailAddress(string email)
        {
            return Regex.IsMatch(email, @"^\S+@\S+$");
        }
    }
}
