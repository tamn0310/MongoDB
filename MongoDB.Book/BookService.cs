using MongoDB.Book.MongoSetting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.Book
{
    public class BookService
    {
        private readonly IMongoCollection<Models.Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Models.Book>(settings.BooksCollectionName);
        }

        public List<Models.Book> Gets() =>
            _books.Find(book => true).ToList();

        public Models.Book Get(string id) =>
            _books.Find<Models.Book>(book => book.Id == id).FirstOrDefault();

        public Models.Book Insert(Models.Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Models.Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Delete(Models.Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Delete(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}