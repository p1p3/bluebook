using System;

namespace Books.Core.Models.Exceptions
{
    public class EmptyPageException : Exception
    {
        public EmptyPageException() : base("Page is empty, please create the page with some fields in it.")
        {

        }
    }
}
