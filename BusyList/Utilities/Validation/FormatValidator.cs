using System.Text.RegularExpressions;

namespace BusyList.Utilities.Validation
{
    /// <summary>
    /// Validate a string against a regex format
    /// </summary>
    public class FormatValidator: IValidator
    {
        public string Message { get; set; } = "Invalid format";
        public string Format { get; set; }

        public bool Check(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex format = new Regex(Format);

                return format.IsMatch(value);
            }
            else
            {
                return false;
            }
        }
    }
}
