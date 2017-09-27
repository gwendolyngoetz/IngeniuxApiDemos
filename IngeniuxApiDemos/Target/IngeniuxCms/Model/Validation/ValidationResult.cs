namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation
{
    public class ValidationResult
    {
        public string Name { get; }
        public string Value { get; }
        public bool IsValid { get; }
        public string Message { get; }

        public ValidationResult(string name, object value, bool isValid, string message)
        {
            Name = name;
            Value = value?.ToString();
            IsValid = isValid;
            Message = message;
        }
    }
}