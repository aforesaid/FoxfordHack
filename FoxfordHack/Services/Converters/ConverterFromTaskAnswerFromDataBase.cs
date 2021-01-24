using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FoxfordHack.Services.Converters
{
    public static class ConverterFromTaskAnswerFromDataBase
    {
        public static string DictionaryToString
      (List<KeyValuePair<string, string>> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException("Bad Dictionary");

            var items = from kvp in dictionary
                        select kvp.Key + "=" + kvp.Value;

            return "{" + string.Join(",", items) + "}";
        }
        public static List<KeyValuePair<string, string>> StringToDictionary
      (string content)
        {
            if (content == null)
                throw new ArgumentNullException("Bad string from DB");
            var dictionary = new List<KeyValuePair<string, string>>();
            var contents = content.Split(new char[] { '=', '}', '{', ',' });
            for (int i = 1; i+1 < contents.Length; i+=2)
                dictionary.Add(new KeyValuePair<string,string>(contents[i], contents[i + 1]));
            return dictionary;
        }
    }
}
