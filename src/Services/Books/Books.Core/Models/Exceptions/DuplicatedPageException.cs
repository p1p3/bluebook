using System;

namespace Books.Core.Models.Exceptions
{
    public class DuplicatedPageException : Exception
    {
        public DuplicatedPageException(int pageNumber, int chapterNumber) : base("This page is already added to the chapter")
        {

        }
    }
}
