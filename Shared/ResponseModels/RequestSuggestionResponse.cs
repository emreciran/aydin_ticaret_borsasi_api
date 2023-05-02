using EntitiesLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ResponseModels
{
    public class RequestSuggestionResponse
    {
        public List<RequestSuggestion> RequestSuggestions { get; set; } = new List<RequestSuggestion>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }

        public int Total { get; set; }
    }
}
