using System;
using System.Collections.Generic;
using System.Text;
using Util;

/* Develop custom class TextTransformer which transforms input text into uppercase text and converts null strings into something valuable. (using iterator) */

namespace Strings
{
    class TextTransformer
    {

        private const string NULL_STRING = "$EMPTY STRING$";
        private StringBuilder _sbInitialText = new StringBuilder();
        private StringBuilder _sbTransformedText = new StringBuilder();

        public string InitialText {
            get { return _sbInitialText.ToString(); }
            private set
            {
                _sbInitialText.Clear();
                _sbInitialText.Append((value == null) ? NULL_STRING : value);                
            }
        }

        public string TransformedText
        {
            get { return _sbTransformedText.ToString(); }
            private set { }
        }

        public TextTransformer()
        {
        }

        public TextTransformer(string inputText)
        {
            this.InitialText = inputText;
            
        }

        public TextTransformer(StringBuilder sbInput)
        {
            this.InitialText = sbInput.ToString();
        }


        /* Text transformation using iterator */
        private TextTransformer Transform(Func<char, char> ct)
        {
            _sbTransformedText.Clear();

            foreach (var chr in this.InText())
            {
                _sbTransformedText.Append(ct(chr));
            }

            return this;
        }


        public TextTransformer Transform()
        {
            return Transform(Char.ToUpper);

        }


        public TextTransformer Transform(string newText)
        {
            this.InitialText = newText;
            return this.Transform(Char.ToUpper);
        }

        public TextTransformer TransformWith(Func<char, char> ct)
        {
            return this.Transform(ct);
        }

        public TextTransformer TransformWith(string newText, Func<char, char> ct)
        {
            this.InitialText = newText;
            return this.Transform(ct);
        }

        /* Iterator for input text */
        private IEnumerable<char> InText()
        {
            for(int i=0; i < _sbInitialText.Length; i++)
            {
                yield return _sbInitialText[i];
            }
        }
    }

}
