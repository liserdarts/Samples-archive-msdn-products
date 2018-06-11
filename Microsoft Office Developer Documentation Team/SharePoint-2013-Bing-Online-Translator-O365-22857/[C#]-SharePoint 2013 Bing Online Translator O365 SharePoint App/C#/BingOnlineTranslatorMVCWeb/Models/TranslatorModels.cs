using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BingOnlineTranslatorMVCWeb;

namespace BingOnlineTranslatorMVCWeb.Models
{
    public class TranslatorModels
    {
        public List<Language> GetLanguages()
        {
            List<Language> languages = new List<Language>();

            MicrosoftTranslatorHelper mh = new MicrosoftTranslatorHelper();
            List<string> languagesForSpeak = mh.GetLanguagesForSpeakMethod();
            string[] codes = mh.GetLanguagesForTranslate().ToArray();
            string[] names = mh.GetLanguageNamesMethod(codes);

            for (int i = 0; i < names.Length; i++)
            {
                languages.Add(new Language
                {
                    LanguageCode = codes[i],
                    LanguageName = names[i],
                    IsSupportedSpeak = languagesForSpeak.Contains(codes[i])
                });
            }

            return languages;
        }

        public string Translate(string from, string to, string text)
        {
            MicrosoftTranslatorHelper mh = new MicrosoftTranslatorHelper();
            return mh.TranslateMethod(from, to, text);
        }

        public byte[] Speak(string text, string languageCode)
        {
            MicrosoftTranslatorHelper mh = new MicrosoftTranslatorHelper();
            return mh.SpeakMethod(text, languageCode);
        }
    }

    public class Language
    {
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsSupportedSpeak { get; set; }
    }
}