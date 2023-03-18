using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DevTest.Shared.DTO
{

    public class RFCAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string rfc = value.ToString();
                if (rfc.Length == 13 && IsValidRFCFormat(rfc))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage ?? "Invalid RFC format");
        }

        private bool IsValidRFCFormat(string rfc)
        {
            if (string.IsNullOrEmpty(rfc) || rfc.Length != 13)
            {
                return false;
            }

            Regex regex = new Regex(@"^[A-Z]{4}[0-9]{6}[A-Z0-9]{3}$");
            if (!regex.IsMatch(rfc))
            {
                return false;
            }

            string year = rfc.Substring(4, 2);
            int yearInt;
            if (!int.TryParse(year, out yearInt))
            {
                return false;
            }

            int month = int.Parse(rfc.Substring(6, 2));
            if (month < 1 || month > 12)
            {
                return false;
            }

            int day = int.Parse(rfc.Substring(8, 2));
            if (day < 1 || day > DateTime.DaysInMonth(1900 + yearInt, month))
            {
                return false;
            }

            return true;
        }
    }
}
