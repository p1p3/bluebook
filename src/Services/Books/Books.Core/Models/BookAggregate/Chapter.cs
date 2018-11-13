using Books.Core.Models.Exceptions;
using Books.Core.Models.Fields;
using System;
using System.Collections.Generic;

namespace Books.Core.Models.BookAggregate
{
    public class Chapter
    {
        public string Title { get; private set; }
        public int ChapterNumber { get; private set; }
        public Guid Id { get; private set; }

        private readonly List<Page> _pages;
        public IReadOnlyCollection<Page> Pages => _pages.AsReadOnly();

        protected Chapter() { }

        public Chapter(string title, int chapterNumber)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.ChapterNumber = chapterNumber;
            this._pages = new List<Page>();
        }

        public Page AddPage(List<PageField> fields)
        {
            var newPage = new Page(fields);
            this._pages.Add(newPage);
            return newPage;
        }

        public void MoveChapter(int newChapterNumber)
        {
            this.ChapterNumber = newChapterNumber;
        }
    }
}
