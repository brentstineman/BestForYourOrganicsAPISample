using Newtonsoft.Json;
using System;

namespace BestForYourOrganics.API
{
    public class RatingDocument
    {
        public Guid id {get; set;}

        public string userid {get; set;}

        public string productid {get; set;}

        public DateTime timestamp {get; set;}

        public string locationname  {get; set;}

        public int rating {get; set;}

        public string usernotes {get; set;}
              
    }
}