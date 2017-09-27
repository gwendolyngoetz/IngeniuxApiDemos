using IngeniuxApiDemos.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IngeniuxApiDemos.Common
{
    public interface IArgumentParser
    {
        string GetArgumentValue(string argumentName);
    }

    public class ArgumentParser : IArgumentParser
    {
        private readonly IDictionary<string, string> _parsedArguments;

        public ArgumentParser(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            _parsedArguments = ParseArguments(args);
        }

        private static IDictionary<string, string> ParseArguments(IEnumerable<string> args)
        {
            var dictionary = new Dictionary<string, string>();
            var errors = new List<string>();

            foreach (var value in args)
            {
                var argument = value.Replace("--", "").Split(new[] { '=' });

                if (argument.Length != 2)
                {
                    errors.Add($"Argument {value} is invalid.");
                    continue;
                }

                dictionary.Add(argument[0].ToLower(), argument[1]);
            }

            CheckForErrors(errors);

            return dictionary;
        }

        public string GetArgumentValue(string argumentName)
        {
            if (argumentName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argumentName));
            }

            var value = _parsedArguments[argumentName.ToLower()];

            if (value.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException($"Argument name {argumentName} does not have a value.");
            }

            return value;
        }

        public string GetArgumentValueOrDefault(string argumentName)
        {
            if (argumentName.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argumentName));
            }

            argumentName = argumentName.ToLower();

            if (!_parsedArguments.ContainsKey(argumentName))
            {
                return string.Empty;
            }

            var value = _parsedArguments[argumentName];

            if (value.IsNullOrWhiteSpace())
            {
                throw new InvalidOperationException($"Argument name {argumentName} does not have a value.");
            }

            return value;
        }

        private static void CheckForErrors(List<string> errors)
        {
            if (errors.Any())
            {
                var sb = new StringBuilder();

                foreach (var error in errors)
                {
                    sb.AppendLine(error);
                }

                throw new InvalidOperationException(sb.ToString());
            }
        }
    }
}
