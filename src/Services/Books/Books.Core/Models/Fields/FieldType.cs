using Books.Core.SeedWork;

namespace Books.Core.Models.Fields
{
    public class FieldType : Enumeration
    {
        public static FieldType Picture = new PictureFieldType();
        public static FieldType Text = new TextFieldType();
        public static FieldType Input = new InputFieldType();

        public FieldType(int id, string name, string description)
            : base(id, name, description)
        {
        }

        private class PictureFieldType : FieldType
        {
            public PictureFieldType() : base(1, "Picture", "Store pictures and images")
            { }
        }

        private class TextFieldType : FieldType
        {
            public TextFieldType() : base(2, "Text", "Store multi-line texts")
            { }
        }

        private class InputFieldType : FieldType
        {
            public InputFieldType() : base(3, "Input", "Store single-line texts")
            { }
        }
    }
}
