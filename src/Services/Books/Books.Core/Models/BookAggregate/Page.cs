using Books.Core.Models.Fields;
using System;
using System.Linq;
using System.Collections.Generic;
using Books.Core.Models.Exceptions;

namespace Books.Core.Models.BookAggregate
{
    public class Page
    {
        public Guid Id { get; private set;}

        private readonly List<PageField> _fields;
        public IReadOnlyCollection<PageField> Fields => _fields.AsReadOnly();

        protected Page()
        {

        }

        public Page(List<PageField> fields)
        {
            if (fields == null || !fields.Any()) throw new EmptyPageException();
            this.Id = Guid.NewGuid();
            this._fields = fields;
        }

        public void AddField(FieldType fieldType, string description, bool required)
        {
            var id = Guid.NewGuid().ToString();
            var pageField = new PageField(fieldType, description, id, required);
        }
    }
}
