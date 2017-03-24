using System;
using System.Text.RegularExpressions;

namespace Validation
{

    /*
     Develop a console application for validating inputted email adresses, URLs, file paths using regular expressions.
     */

    interface IValidate
    {
        bool validate(string value);
    }

    class RangeValidator : IValidate
    {
        private long min, max;
        public RangeValidator(long min, long max)
        {

            if (min > max) throw new ArgumentException(string.Format("Min value {0} should be less or equal to max value {1}", new string[] { min.ToString(), max.ToString()}));
            this.min = min;
            this.max = max;

        }

        bool IValidate.validate(string value)
        {
            long longValue;
            if (long.TryParse(value, out longValue))
            {
                return (min <= longValue) && (longValue <= max);
            }
            else
            {
                return false;
            }            
        }
    }

    class RegexValidator : IValidate
    {

        public string _pattern { get; private set; }

        public RegexValidator(string pattern)
        {
            _pattern = pattern;
        }

        bool IValidate.validate(string value)
        {
            Match match = Regex.Match(value, _pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }

    }


    class EmailValidator : RegexValidator
    {
        public EmailValidator() : base(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$") { }

        public EmailValidator(string pattern) : base(pattern) { }
    }

    class URLValidator : RegexValidator
    {
        public URLValidator() : base(@"(?<protocol>http(s)?|ftp)://(?<server>([A-Za-z0-9-]+\.)*(?<basedomain>[A-Za-z0-9-]+\.[A-Za-z0-9]+))+((/?)(?<path>(?<dir>[A-Za-z0-9\._\-]+)(/){0,1}[A-Za-z0-9.-/]*)){0,1}") { }

        public URLValidator(string pattern) : base(pattern) { }
    }

    class FilePathValidator : RegexValidator
    {
        public FilePathValidator() : base(@"^([a-zA-Z]\:|\\\\[^\/\\:*?"" <>|]+\\[^\/\\:*?""<>|]+)(\\[^\/\\:*?""<>|]+)+(\.[^\/\\:*?""<>|]+)$") { }

        public FilePathValidator(string pattern) : base(pattern) { }
    }

    class NameValidator: RegexValidator
    {
        public NameValidator() : base(@"^((?:[A-Z](?:('|(?:[a-z]{1,3}))[A-Z])?[a-z]+)|(?:[A-Z]\.))(?:([ -])((?:[A-Z](?:('|(?:[a-z]{1,3}))[A-Z])?[a-z]+)|(?:[A-Z]\.)))?$") { }
        public NameValidator(string pattern): base(pattern) { }
    }



}