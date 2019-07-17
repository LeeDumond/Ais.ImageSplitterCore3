using System;
using System.Collections.Generic;
using System.Linq;

namespace Ais.ImageSplitter.Library
{
    public class SplitRequest
    {
        public string InputFilePath { get; set; }
        public string OutputFilePath { get; set; }
        public string Pages { get; set; }

        public int[] PageList => GetPageList();

        private int[] GetPageList()
        {
            var finalList = new List<int>();

            if (Pages == null)
            {
                return finalList.ToArray();
            }

            string[] initialList = Pages.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in initialList)
            {
                if (int.TryParse(s, out int result))
                {
                    finalList.Add(result);
                }
                else
                {
                    string trimmedString = s.Trim('[', ']');

                    if (trimmedString.IndexOf('-') != -1)
                    {
                        if (int.TryParse(trimmedString.Substring(0, trimmedString.IndexOf('-')), out int firstPage) &&
                            int.TryParse(trimmedString.Substring(trimmedString.IndexOf('-') + 1), out int lastPage))
                        {
                            for (int i = firstPage; i <= lastPage; i++)
                            {
                                finalList.Add(i);
                            }
                        }
                    }
                }
            }

            return finalList.Distinct().OrderBy(i => i).ToArray();
        }
    }
}