using SpaApp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaApp.Domain.Repositories;
using System.Text.RegularExpressions;
using SpaApp.Application.Common;

namespace SpaApp.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private const int MinEmailLength = 6;
        private const int MaxEmailLength = 254;
        private const int MaxLocalPartLength = 65;
        private const int MinDomainPartLength = 1;
        private const int MaxDomainPartLength = 253;

        private readonly IUserRepository _userRepository;
        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(UserErrors.UsernameErrors.UsernameRequired.Message)
                .Length(2, 30).WithMessage(UserErrors.UsernameErrors.UsernameLengthInvalid.Message)
                .Matches(@"^[a-zA-Z0-9\s-_]+$").WithMessage(UserErrors.UsernameErrors.UsernameInvalidCharacters.Message)
                .Must(BeValidUsername).WithMessage(UserErrors.UsernameErrors.UsernameInvalidFormat.Message)
                .Must(ContainLetters).WithMessage(UserErrors.UsernameErrors.UsernameRequiredOneLetter.Message)
                .MustAsync(BeUniqueUsername).WithMessage(UserErrors.UsernameErrors.UsernameNotUnique.Message);
            RuleFor(user => user.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(UserErrors.EmailErrors.EmailRequired.Message)
                .Length(MinEmailLength, MaxEmailLength).WithMessage(UserErrors.EmailErrors.EmailLengthInvalid.Message)
                .Must(BeValidEmail).WithMessage(UserErrors.EmailErrors.EmailInvalidFormat.Message)
                .MustAsync(BeUniqueEmail).WithMessage(UserErrors.EmailErrors.EmailNotUnique.Message);
            RuleFor(user => user.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(UserErrors.PasswordErrors.PasswordRequired.Message)
                .Length(8, 20).WithMessage(UserErrors.PasswordErrors.PasswordLengthInvalid.Message)
                .Must(password => Regex.IsMatch(password, @"[A-Za-z]")).WithMessage(UserErrors.PasswordErrors.PasswordRequiredOneDigit.Message)
                .Must(password => Regex.IsMatch(password, @"\d")).WithMessage(UserErrors.PasswordErrors.PasswordRequiredOneLetter.Message)
                .Must(password => Regex.IsMatch(password, @"[\W_]")).WithMessage(UserErrors.PasswordErrors.PasswordRequiredOneSpecialCharacter.Message);
        }

        private bool BeValidUsername(string username)
        {
            return !username.StartsWith("-") && !username.StartsWith("_") && !username.StartsWith(" ") &&
                   !username.EndsWith("-") && !username.EndsWith("_") && !username.EndsWith(" ");
        }

        private bool ContainLetters(string username)
        {
            return Regex.IsMatch(username, @"[a-zA-Z]");
        }

        /// <summary>
        /// Validates an email address by checking if it has a valid '@' symbol, valid length, 
        /// and valid local and domain parts.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>True if the email address is valid according to the following rules:
        /// - It contains exactly one '@' symbol.
        /// - Its length is between <see cref="MinEmailLength"/> and <see cref="MaxEmailLength"/>.
        /// - The local part and domain part are valid according to their respective rules.
        /// - The format of the email address is valid.
        /// Otherwise, false.</returns>
        private bool BeValidEmail(string email)
        {
            if (!HasValidAtSymbol(email) || !HasValidLength(email))
                return false;

            var atIndex = email.IndexOf('@');
            var localPart = email.Substring(0, atIndex);
            var domainPart = email.Substring(atIndex + 1);

            return IsValidLocalPart(localPart) && IsValidDomainPart(domainPart) && HasValidFormat(email);
        }

        /// <summary>
        /// Checks if the email contains a valid '@' symbol.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>True if there is exactly one '@' symbol and it's in a valid position.</returns>
        private bool HasValidAtSymbol(string email)
        {
            var atIndex = email.IndexOf('@');
            return atIndex > 0 && atIndex == email.LastIndexOf('@');
        }

        /// <summary>
        /// Checks if the email address length is within the valid range.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>True if the length is valid, otherwise false.</returns>
        private bool HasValidLength(string email)
        {
            return email.Length >= MinEmailLength && email.Length <= MaxEmailLength;
        }

        /// <summary>
        /// Validates the local part of an email address according to the specified rules.
        /// </summary>
        /// <param name="localPart">The local part of the email address to validate.</param>
        /// <returns>
        /// True if the local part is valid according to the following rules:
        /// - It matches the allowed character set defined by the regular expression.
        /// - It does not start or end with a period ('.').
        /// - Its length does not exceed the maximum allowed length.
        /// Otherwise, false.
        /// </returns>
        private bool IsValidLocalPart(string localPart)
        {
            var localPartRegex = new Regex($@"^[a-zA-Z0-9!#$%&'*+/=?^_`{{|}}~.-]{{1,{MaxLocalPartLength}}}$");
            return localPartRegex.IsMatch(localPart) && !localPart.StartsWith(".") && !localPart.EndsWith(".");
        }

        /// <summary>
        /// Validates the domain part of an email address according to the specified rules.
        /// </summary>
        /// <param name="domainPart">The domain part of the email address to validate.</param>
        /// <returns>
        /// True if the domain part is valid according to the following rules:
        /// - It matches the allowed character set defined by the regular expression.
        /// - It does not start or end with a hyphen ('-').
        /// - It does not contain consecutive periods ('..').
        /// Otherwise, false.
        /// </returns>
        private bool IsValidDomainPart(string domainPart)
        {
            var domainPartRegex = new Regex($@"^[a-zA-Z0-9.-]{{{MinDomainPartLength},{MaxDomainPartLength}}}$");
            return domainPartRegex.IsMatch(domainPart) &&
                !domainPart.StartsWith("-") &&
                !domainPart.EndsWith("-") &&
                !domainPart.Contains("..") &&
                !domainPart.StartsWith(".") &&
                !domainPart.EndsWith(".");
        }

        /// <summary>
        /// Ensures the email address format is valid by checking the starting and ending characters.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>True if the format is valid, otherwise false.</returns>
        private bool HasValidFormat(string email)
        {
            return !email.StartsWith(".") && !email.EndsWith("-") && !email.EndsWith(".") && !email.Contains("..");
        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return !await _userRepository.UsernameExistsAsync(username);
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return !await _userRepository.EmailExistsAsync(email);
        }
    }
}
