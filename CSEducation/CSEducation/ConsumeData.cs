using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ConsumeData
{

    /* Develop class Book with fields containing information about certain book and ability to load/safe contents of object from/to JSON and XML files. */

    class TAGS
    {
        public const string BOOK = "book";
        public const string ID = "id";
        public const string TITLE = "title";
        public const string AUTHOR = "authors";
        public const string YEAR = "publication-year";
        public const string DESCR = "description";
        public const string TEXT = "text";
    }
   
    [Serializable]
    [DataContract]
    public class Book
    {
        [XmlAttribute(TAGS.ID)]
        [DataMember]
        public string Id { get; set; }
        [XmlElement(TAGS.TITLE)]
        [DataMember]
        public string Title { get; set; }
        [XmlElement(TAGS.AUTHOR)]
        [DataMember]
        public string Author { get; set; }
        [XmlElement(TAGS.YEAR)]
        [DataMember]
        public int PublicationYear { get; set; }
        [XmlElement(TAGS.DESCR)]
        [DataMember]
        public string Description { get; set; }
        [XmlElement(TAGS.TEXT)]
        [DataMember]
        public string Text { get; set; }

        public Book()
        {

        }
        
        public static Book LoadFromXML(string id)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;

            Book book = new Book();

            using (XmlReader xr = XmlReader.Create(string.Format(@".\{0}.xml", id), settings))
            {
                xr.MoveToContent();
                book.Id = xr[TAGS.ID];
                xr.ReadStartElement(TAGS.BOOK);
                book.Title = xr.ReadElementContentAsString();                
                book.Author = xr.ReadElementContentAsString();
                book.PublicationYear = xr.ReadElementContentAsInt();
                book.Description = xr.ReadElementContentAsString();
                book.Text= xr.ReadElementContentAsString();
                xr.ReadEndElement();
            }

            return book;
        }

        public void SaveToXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(string.Format(@".\{0}.xml", this.Id), settings))
            {
                xw.WriteStartElement(TAGS.BOOK);
                xw.WriteAttributeString(TAGS.ID, this.Id);
                xw.WriteElementString(TAGS.TITLE, this.Title);
                xw.WriteElementString(TAGS.AUTHOR, this.Author);
                xw.WriteElementString(TAGS.YEAR, this.PublicationYear.ToString());
                xw.WriteElementString(TAGS.DESCR, this.Description);
                xw.WriteElementString(TAGS.TEXT, this.Text);
                xw.WriteEndElement();
            }                           
        }

        public static Book LoadFromJSON(string id)
        {
            using (TextReader tr = File.OpenText(string.Format(@".\{0}.json", id)))
            {
                return JsonConvert.DeserializeObject<Book>(tr.ReadToEnd());
            }
        }

        public void SaveToJSON()
        {
            string jsonStr = JsonConvert.SerializeObject(this);

            using (TextWriter tw = File.CreateText(string.Format(@".\{0}.json", this.Id)))
            {
                tw.Write(jsonStr);
            }
        }

        public override string ToString()
        {
            return string.Format("Book #{0}: '{1}' by {2} (pub. year: {3})\nDescription: {4}\n\n{5}", this.Id, this.Title, this.Author, this.PublicationYear, this.Description, this.Text);
        }
    }
}