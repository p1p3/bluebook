using System;
using System.Linq;
using System.Collections.Generic;
using Books.Core.SeedWork;

namespace Books.Core.Models.BookAggregate
{
    public class Book : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }

        private readonly List<Chapter> _chapters;
        public IReadOnlyCollection<Chapter> Chapters => _chapters.AsReadOnly();

        protected Book() { }

        public Book(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            _chapters = new List<Chapter>();
        }

        public Chapter AppendChapter(string chapterTitle)
        {
            var chapterNumber = GetNextChapterNumber();
            return this.AddChapter(chapterTitle, chapterNumber);
        }

        public Chapter AddChapter(string chapterTitle, int chapterNumber)
        {
            var newChapter = new Chapter(chapterTitle, chapterNumber);
            var chapterNumberAlreadyExisted = this.Chapters.Any(chapter => chapter.ChapterNumber == newChapter.ChapterNumber);

            if (chapterNumberAlreadyExisted)
            {
                var chaptersNeededToMoveOneChapterNumber = _chapters.Where(chapter => chapter.ChapterNumber >= newChapter.ChapterNumber);
                foreach (var chapter in chaptersNeededToMoveOneChapterNumber)
                {
                    var newChapterNumber = chapter.ChapterNumber + 1;
                    chapter.MoveChapter(newChapterNumber);
                }
            }

            this._chapters.Add(newChapter);

            return newChapter;
        }

        private int GetNextChapterNumber()
        {
            var bookHasChapters = _chapters.Any();

            if (!bookHasChapters) return 1;

            var lastChapterNumber = _chapters.OrderByDescending(chapter => chapter.ChapterNumber).First().ChapterNumber;

            return lastChapterNumber++;
        }
    }
}
