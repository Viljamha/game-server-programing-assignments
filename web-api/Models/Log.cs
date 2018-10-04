using System;

namespace web_api.Models
{
    public class Log
    {
        public string text { get; set;}
        public Guid Id { get; set; }

        public Log(string str) {
            text = str;
            Id = Guid.NewGuid();
        }

    }
}