using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace IngeniuxApiDemos.Target.IngeniuxCms.Model.Validation
{
    public class ValidationResults
    {
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>();

        public bool IsValid => _validationResults.All(x => x.IsValid);
        public IEnumerable<ValidationResult> Results => _validationResults;

        public void Add(ValidationResult result)
        {
            _validationResults.Add(result);
        }

        public void Add(ValidationResults results)
        {
            _validationResults.AddRange(results.Results);
        }

        public void ThrowIfErrors()
        {
            if (IsValid)
            {
                return;
            }

            throw new ValidationException(ToString(), this);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(new
            {
                ValidationErrors = _validationResults.Where(x => !x.IsValid)
            });
        }
    }
}