using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
// var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
var connectionString = "mongodb://root:example@localhost:27017";

if (connectionString == null)
{
    Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
    Environment.Exit(0);
}

var client = new MongoClient(connectionString);

var collection = client.GetDatabase("sample_mflix").GetCollection<BsonDocument>("movies");

var filter = Builders<BsonDocument>.Filter.Eq("title", "Back to the Future");

var document = collection.Find(filter).First();

Console.WriteLine(document);
