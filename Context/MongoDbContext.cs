using DemoWebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;



namespace DemoWebAPI.Context;

public class MongoDbContext{

    private readonly IMongoDatabase _database;
    public IMongoCollection<Student> StudentCollection;
    public IMongoCollection<Faculty> FacultyCollection;
    public IMongoCollection<Admin> AdminCollection;

    public MongoDbContext(IOptions<DatabaseSettings> databaseSettings){
        
        Console.WriteLine(databaseSettings.Value.connectionURI);
        MongoClient client = new MongoClient(databaseSettings.Value.connectionURI);
        _database = client.GetDatabase(databaseSettings.Value.Databasename);
        StudentCollection = _database.GetCollection<Student>(databaseSettings.Value.StudentCollection);
        FacultyCollection = _database.GetCollection<Faculty>(databaseSettings.Value.FacultyCollection);
        AdminCollection = _database.GetCollection<Admin>(databaseSettings.Value.AdminCollection);
        
    }

}