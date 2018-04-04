using System;
using System.Collections.Generic;
using System.Text;

namespace TrasfusionaleApp.Service
{
    public class Header
    {
        public string header;
        public string value;

        public Header() { }

        public Header(string header, string value)
        {
            this.header = header;
            this.value = value;
        }
    }
}
