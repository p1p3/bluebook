namespace Books.Core.Models.Fields
{
    public class PageField
    {
        protected PageField() { }

        public PageField(FieldType type, string description, string identifier, bool required)
        {
            this.Type = type;
            this.Description = description;
            this.Identifier = identifier;
            this.Required = required;
        }

        public string Identifier { get; private set; }
        public FieldType Type { get; private set; }
        public string Description { get; private set; }
        public bool Required { get; private set; }
    }
}
