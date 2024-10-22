using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaApp.Application.Common
{
    public static class UserErrors
    {
        public static class UsernameErrors
        {
            public static readonly ErrorInfo UsernameRequired = new ErrorInfo
            {
                Code = "USERNAME_REQUIRED",
                Message = "The Username field is required.",
                Details = "Please enter a username to proceed."
            };

            public static readonly ErrorInfo UsernameLengthInvalid = new ErrorInfo
            {
                Code = "USERNAME_LENGTH_INVALID",
                Message = "Username must be 2-30 characters long.",
                Details = "Your username is either too short or too long. Please adjust its length."
            };

            public static readonly ErrorInfo UsernameInvalidCharacters = new ErrorInfo
            {
                Code = "USERNAME_INVALID_CHARACTERS",
                Message = "Username contains invalid characters.",
                Details = "Only Latin characters, numbers, spaces, hyphens (-), and underscores (_) are allowed."
            };

            public static readonly ErrorInfo UsernameNotUnique = new ErrorInfo
            {
                Code = "USERNAME_NOT_UNIQUE",
                Message = "This username is already taken.",
                Details = "Please choose a different username."
            };

            public static readonly ErrorInfo UsernameInvalidFormat = new ErrorInfo
            {
                Code = "USERNAME_INVALID_FORMAT",
                Message = "Username cannot start or end with a special character.",
                Details = "Please remove any hyphens or underscores from the start and end of your username."
            };

            public static readonly ErrorInfo UsernameRequiredOneLetter = new ErrorInfo
            {
                Code = "USERNAME_REQUIRED_ONE_LETTER",
                Message = "The Username must contain at least one letter.",
                Details = "Please add at least one letter of your username."
            };
        }

        public static class EmailErrors
        {
            public static readonly ErrorInfo EmailRequired = new ErrorInfo
            {
                Code = "EMAIL_REQUIRED",
                Message = "The Email field is required.",
                Details = "Please enter an email address to proceed."
            };

            public static readonly ErrorInfo EmailLengthInvalid = new ErrorInfo
            {
                Code = "EMAIL_LENGTH_INVALID",
                Message = "Email must be 6-254 characters long.",
                Details = "Your email is either too short or too long. Please check and adjust its length."
            };

            public static readonly ErrorInfo EmailInvalidFormat = new ErrorInfo
            {
                Code = "EMAIL_INVALID_FORMAT",
                Message = "Invalid email format.",
                Details = "Please enter a valid email address (e.g., user@example.com)."
            };

            public static readonly ErrorInfo EmailNotUnique = new ErrorInfo
            {
                Code = "EMAIL_NOT_UNIQUE",
                Message = "This email address is already registered.",
                Details = "Please use a different email address or try to log in."
            };

            public static readonly ErrorInfo EmailInvalidCharacters = new ErrorInfo
            {
                Code = "EMAIL_INVALID_CHARACTERS",
                Message = "Email contains invalid characters.",
                Details = "Please check your email for any disallowed special characters."
            };
        }

        public static class PasswordErrors
        {
            public static readonly ErrorInfo PasswordRequired = new ErrorInfo
            {
                Code = "PASSWORD_REQUIRED",
                Message = "The Password field is required.",
                Details = "Please enter a password to proceed."
            };

            public static readonly ErrorInfo PasswordLengthInvalid = new ErrorInfo
            {
                Code = "PASSWORD_LENGTH_INVALID",
                Message = "Password must be 8-20 characters long.",
                Details = "Your password is either too short or too long. Please adjust its length."
            };

            public static readonly ErrorInfo PasswordComplexityInsufficient = new ErrorInfo
            {
                Code = "PASSWORD_COMPLEXITY_INSUFFICIENT",
                Message = "Password does not contain required characters.",
                Details = "Password must contain letters, numbers, and at least one special character."
            };

            public static readonly ErrorInfo PasswordRequiredOneLetter = new ErrorInfo
            {
                Code = "PASSWORD_ONLY_LETTERS",
                Message = "Password cannot consist of only letters.",
                Details = "Password must contain at least one digit and one special character."
            };

            public static readonly ErrorInfo PasswordRequiredOneDigit = new ErrorInfo
            {
                Code = "PASSWORD_ONLY_DIGITS",
                Message = "Password cannot consist of only digits.",
                Details = "Password must contain at least one letter and one special character."
            };

            public static readonly ErrorInfo PasswordRequiredOneSpecialCharacter = new ErrorInfo
            {
                Code = "PASSWORD_ONLY_SPECIAL_CHARACTERS",
                Message = "Password cannot consist of only special characters.",
                Details = "Password must contain at least one letter and one digit."
            };
        }

        public static class AgeErrors
        {
            public static readonly ErrorInfo AgeInvalidLength = new ErrorInfo
            {
                Code = "AGE_INVALID_LENGTH",
                Message = "Age must be 1-3 digits long.",
                Details = "Please enter a valid age between 0 and 999."
            };

            public static readonly ErrorInfo AgeInvalidCharacters = new ErrorInfo
            {
                Code = "AGE_INVALID_CHARACTERS",
                Message = "Age must contain only numbers.",
                Details = "Please remove any non-numeric characters from the Age field."
            };
        }

        public static class OccupationErrors
        {
            public static readonly ErrorInfo OccupationLengthInvalid = new ErrorInfo
            {
                Code = "OCCUPATION_LENGTH_INVALID",
                Message = "Occupation must be 1-120 characters long.",
                Details = "Your occupation description is too long. Please shorten it."
            };

            public static readonly ErrorInfo OccupationInvalidCharacters = new ErrorInfo
            {
                Code = "OCCUPATION_INVALID_CHARACTERS",
                Message = "Occupation contains invalid characters (-,._:;).",
                Details = "Only Latin characters, numbers, spaces, and allowed special characters (-,._:;) are permitted."
            };
        }
    }
}
