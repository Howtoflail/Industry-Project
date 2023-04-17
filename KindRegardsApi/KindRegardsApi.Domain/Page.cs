using System;
using System.Collections.Generic;

namespace KindRegardsApi.Domain
{
    public class Page
    {
        public int TotalRecords {get; set;} = 0;
        public int TotalPages {get; set;} = 0;
        public int CurrentPage {get; set;} = 0;
        public List<Object> Data {get; set;} = new List<object>();

        // Default constructor
        public Page(){}
    }
}
