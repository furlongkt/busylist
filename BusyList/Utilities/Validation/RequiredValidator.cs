namespace BusyList.Utilities.Validation
{
    /// <summary>
    /// This validation rule makes sure a string is required, meaning is not null or whitespace
    /// </summary>
    public class RequiredValidator: IValidator
    {
        public string Message { get; set; } = "This field is required";

        public bool Check(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
