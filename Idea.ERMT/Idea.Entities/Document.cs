namespace Idea.Entities
{
    public class Document
    {
        public string Filename { get; set; }

        public string Content { get; set; }

        public ERMTDocumentType DocumentType { get; set; }
    }
}
