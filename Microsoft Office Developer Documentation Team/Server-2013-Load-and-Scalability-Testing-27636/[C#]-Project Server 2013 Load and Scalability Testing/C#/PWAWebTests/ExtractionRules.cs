using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.ComponentModel;

namespace PWAWebTests
{

    [DisplayName("Extract substring from existing context parameter")]
    public class ExtractSubstringFromContextParameter : ExtractionRule
    {
        #region Private variables

        private string inputContextParameter;
        private string textBefore;
        private string textAfter;

        #endregion

        #region Public properties

        public string InputContextParameter
        {
            get
            {
                return this.inputContextParameter;
            }
            set
            {
                this.inputContextParameter = value;
            }
        }

        public string TextBefore
        {
            get
            {
                return this.textBefore;
            }
            set
            {
                this.textBefore = value;
            }
        }

        public string TextAfter
        {
            get
            {
                return this.textAfter;
            }
            set
            {
                this.textAfter = value;
            }
        }

        #endregion

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            string sourceValue = e.WebTest.Context[this.inputContextParameter].ToString();

            int startIndex = sourceValue.IndexOf(this.textBefore) + this.textBefore.Length;
            int endIndex = sourceValue.IndexOf(this.textAfter, startIndex);

            if (endIndex > startIndex)
            {
                string extractedSubstring = sourceValue.Substring(startIndex, (endIndex - startIndex));

                if (e.WebTest.Context.ContainsKey(this.ContextParameterName))
                {
                    e.WebTest.Context[this.ContextParameterName] = extractedSubstring;
                }
                else
                {
                    e.WebTest.Context.Add(this.ContextParameterName, extractedSubstring);
                }
            }
            else
            {
                throw new ArgumentException("Cannot find expected substrings in existing context parameter " + this.inputContextParameter);
            }
        }
    }

    [DisplayName("Extract regular expression from existing context parameter")]
    public class ExtractRegexFromContextParameter : ExtractionRule
    {
        #region Private variables

        private string inputContextParameter;
        private string regexText;
        private bool pickRandomMatch;

        private static Random randomNumber = new Random();

        #endregion

        #region Public properties

        public string InputContextParameter
        {
            get { return inputContextParameter; }
            set { inputContextParameter = value; }
        }

        public string RegexText
        {
            get { return regexText; }
            set { regexText = value; }
        }

        public bool PickRandomMatch
        {
            get { return pickRandomMatch; }
            set { pickRandomMatch = value; }
        }

        #endregion

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            string sourceValue = e.WebTest.Context[this.inputContextParameter].ToString();
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(this.regexText);

            //this option is potentially expensive from a computational standpoint.
            if (this.pickRandomMatch)
            {
                System.Text.RegularExpressions.MatchCollection matches = regex.Matches(sourceValue);
                int matchcount = matches.Count;
                if (matchcount > 0)
                {
                    AddToContext(e, matches[randomNumber.Next(0, matchcount)].Value);
                }
                else
                {
                    throw new ArgumentException("Could not find regex in existing context parameter " + this.inputContextParameter);
                }
            }

            //this option is cheaper.
            else
            {
                System.Text.RegularExpressions.Match match = regex.Match(sourceValue);
                if (match.Success)
                {
                    AddToContext(e, match.Value);
                }
                else
                {
                    throw new ArgumentException("Could not find regex in existing context parameter " + this.inputContextParameter);
                }
            }
          

        }

        private void AddToContext(ExtractionEventArgs e, string value)
        {
            if (e.WebTest.Context.ContainsKey(this.ContextParameterName))
            {
                e.WebTest.Context[this.ContextParameterName] = value;
            }
            else
            {
                e.WebTest.Context.Add(this.ContextParameterName, value);
            }
        }
    }

    [DisplayName("Find and replace string pattern in existing context parameter")]
    public class FindReplaceInContextParameter : ExtractionRule
    {
        #region Variables

        private string textToFind;

        public string TextToFind
        {
            get { return textToFind; }
            set { textToFind = value; }
        }


        private string replacementText;

        public string ReplacementText
        {
            get { return replacementText; }
            set { replacementText = value; }
        }

        #endregion

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            string oldValue = e.WebTest.Context[this.ContextParameterName].ToString();
            string newValue=oldValue.Replace(this.textToFind, this.replacementText);
            e.WebTest.Context[this.ContextParameterName] = newValue;
        }
    }
}
