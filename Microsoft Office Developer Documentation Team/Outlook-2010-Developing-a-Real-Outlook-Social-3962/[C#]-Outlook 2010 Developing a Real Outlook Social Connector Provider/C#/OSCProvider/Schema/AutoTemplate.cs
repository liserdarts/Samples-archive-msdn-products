using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OSCProvider.Schema
{
    public class AutoTemplate
    {
        private static long? m_templateIndex = null;
        private static int m_varIndex = 0;
        /// <summary>
        /// Creates a Template based on the format strings for title, data, and icon and using the template variables provided.
        /// </summary>
        /// <param name="activity">The activity to which to add the variables and relate to the template</param>
        /// <param name="titleFormat">Format string for the title.</param>
        /// <param name="dataFormat">Format string for the data.</param>
        /// <param name="iconFormat">Format string for the icon</param>
        /// <param name="titleTokens">TemplateVariable collection to be used in the Title</param>
        /// <param name="dataTokens">Template variable collection to be used in the Data</param>
        /// <param name="iconTokens">Template variable collection to be used in the Icon</param>
        /// <returns></returns>
        public static Template CreateTemplate(Activity activity,
            string titleFormat,
            string dataFormat,
            string iconFormat,
            List<TemplateVariable> titleTokens,
            List<TemplateVariable> dataTokens,
            List<TemplateVariable> iconTokens,
            ProviderSchemaVersion schemaVersion)
        {
            List<string> strTokens = new List<string>();
            Random rnd = new Random();
            Template templ = new Template();
            Activity act = activity;
            if (act == null)
            {
                throw new ArgumentNullException("activity", "Activity cannot be null.");
            }

            templ.ApplicationId = act.ApplicationId;
            templ.TemplateType = ActivityTypes.StatusUpdate;
            if (!m_templateIndex.HasValue )
            {
                m_templateIndex = (long)rnd.Next(int.MaxValue);
            }
            else
            {
                m_templateIndex++;
            }
            templ.TemplateId = m_templateIndex.Value;
            act.TemplateId = templ.TemplateId;


            //Convert Title tokens
            if (!string.IsNullOrEmpty(titleFormat))
            {
                if (titleTokens != null && titleTokens.Count > 0)
                {
                    foreach (TemplateVariable tv in titleTokens)
                    {
                        string tokenS = "{" +
                            tv.VariableType.Substring(0, tv.VariableType.IndexOf("Variable"))
                            + ":"
                            + tv.Name
                            + "}";
                        strTokens.Add(tokenS);

                        if (!act.TemplateVariables.Contains(tv))
                        {
                            act.TemplateVariables.Add(tv);
                        }
                    }
                }
                templ.Title = string.Format(titleFormat, strTokens.ToArray());
            }

            //Convert Data tokens
            strTokens.Clear();
            if (!string.IsNullOrEmpty(dataFormat))
            {
                if (dataTokens != null && dataTokens.Count > 0)
                {
                    foreach (TemplateVariable tv in dataTokens)
                    {
                        string tokenS = "{" +
                            tv.VariableType.Substring(0, tv.VariableType.IndexOf("Variable"))
                            + ":"
                            + tv.Name
                            + "}";
                        strTokens.Add(tokenS);

                        if (!act.TemplateVariables.Contains(tv))
                        {
                            act.TemplateVariables.Add(tv);
                        }
                    }
                }

                templ.Data = string.Format(dataFormat, strTokens.ToArray());
            }


            //Convert iconTokens
            strTokens.Clear();
            if (!string.IsNullOrEmpty(iconFormat))
            {
                if (iconTokens != null && iconTokens.Count > 0)
                {
                    foreach (TemplateVariable tv in iconTokens)
                    {
                        string tokenS = "{" +
                            tv.VariableType.Substring(0, tv.VariableType.IndexOf("Variable"))
                            + ":"
                            + tv.Name
                            + "}";
                        strTokens.Add(tokenS);

                        if (!act.TemplateVariables.Contains(tv))
                        {
                            act.TemplateVariables.Add(tv);
                        }
                    }
                }

                templ.Icon = string.Format(iconFormat, strTokens.ToArray());
            }



            return templ;

        }

        /// <summary>
        /// Creates a collection of template variables based on detecting certain types
        /// of data in the input string. Http urls, @tags, and #tags are recognized
        /// </summary>
        /// <param name="atFormat">The format string to be used as the url for @tags</param>
        /// <param name="hashFormat">The format string to be used as the url for #tags</param>
        /// <param name="input">The starting input. This value will be modified by this method and tokens will be replaced with string format tokens used by the CreateTemplate method.</param>
        /// <returns></returns>
        public static  List<TemplateVariable> AutoReplace(string atFormat,
            string hashFormat,
            ref string input,
            ProviderSchemaVersion schemaVersion)
        {
            
            List<TemplateVariable> tempVars = new List<TemplateVariable>();

            //all procolos would look like this:
            //((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)
            //but OSC v1.0 and v1.1 only support http(s) so we'll only worry about those.
            Regex httpRegex = new Regex(@"(https?:((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)");
            int i = 0;
            for ( ; true ; i++)
            {
                Match match = httpRegex.Match(input);
                if (match.Success)
                {
                    LinkVariable lv = new LinkVariable();
                    lv.Text = match.Value;
                    lv.Name = string.Format("Link{0}v{1}",m_varIndex,i);
                    lv.Value = new Uri(match.Value);
                    tempVars.Add(lv);
                    input = httpRegex.Replace(input, "{" + i + "}", 1);
                }
                else
                    break;

            }

            //the mailto: protocol doesn't work, so we'll just use text variables
            Regex mailtoRegex = new Regex(@"(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})");
            for (; true; i++)
            {
                Match match = mailtoRegex.Match(input);
                if (match.Success)
                {
                    TextVariable lv = new TextVariable();
                    lv.Text = match.Value;
                    lv.Name = string.Format("Text{0}v{1}", m_varIndex, i);
                    tempVars.Add(lv);
                    input = mailtoRegex.Replace(input, "{" + i + "}", 1);
                }
                else
                    break;

            }
          

            Regex atRegex = new Regex(@"\@\w*");
            for (; true; i++)
            {
                Match match = atRegex.Match(input);
                if (match.Success)
                {
                    LinkVariable lv = new LinkVariable();
                    lv.Text = match.Value;
                    lv.Name = string.Format("Link{0}v{1}", m_varIndex, i);
                    lv.Value = new Uri(string.Format(atFormat,match.Value.TrimStart('@')));
                    tempVars.Add(lv);
                    input = atRegex.Replace(input, "{" + i + "}", 1);
                }
                else
                    break;

            }

            Regex hashRegex = new Regex(@"\#\w*");
            for (; true; i++)
            {
                Match match = hashRegex.Match(input);
                if (match.Success)
                {
                    LinkVariable lv = new LinkVariable();
                    lv.Text = match.Value;
                    lv.Name = string.Format("Link{0}v{1}", m_varIndex, i);
                    lv.Value = new Uri(string.Format(hashFormat, match.Value.TrimStart('#')));
                    tempVars.Add(lv);
                    input = hashRegex.Replace(input, "{" + i + "}", 1);
                }
                else
                    break;

            }
            m_varIndex++;

            return tempVars;

        }
    }
}
