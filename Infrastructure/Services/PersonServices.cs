using System.Diagnostics;
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Services;

public class PersonServices
{
    private string _connectionString = "Server=127.0.0.1;Port=5432;Database=Person;User Id=postgres;Password=13577;";

    public PersonServices()
    {
        
    }
    private readonly DataContext _context;

    public PersonServices(DataContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<Person>> GetPersonsWithoutDapper()
    {
        string sql = "SELECT * FROM \"Persons\"";
        var people = new List<Person>();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var sw = new Stopwatch();
            sw.Start();
            await connection.OpenAsync();
            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader =await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var persont = new Person()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                          
                        };
                        people.Add(persont);
                    }
                }
            }   
            sw.Stop();
            System.Console.WriteLine($"Elapsed Times without dapper /  {sw.ElapsedMilliseconds}");
            connection.Close();
        }

        return people;
    }  
    
    public async Task<List<Person>> GetPersonWhithDapper()
    {
        var sw = new Stopwatch();
        sw.Start();
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM \"Persons\" ";
            var result = await connection.QueryAsync<Person>(sql);

            sw.Stop();
            System.Console.WriteLine($"Elapsed Times with dapper /  {sw.ElapsedMilliseconds}");
            return result.ToList();
        }
       
    }
    public async Task<List<Person>> GetPersonWhithEntity()
    {
        var sw = new Stopwatch();
        sw.Start();
        var get =  _context.Persons.Select(x=> new  Person(x.Id,x.FirstName,x.LastName));
        sw.Stop();
        System.Console.WriteLine($"Elapsed Times whith Entity frem work /  {sw.ElapsedMilliseconds}");
        return await get.ToListAsync();
    }
    
    public async Task AddPerson(Person person)
    {
        
        var result  = new Person(person.Id,person.FirstName,person.LastName);
        _context.Persons.Add(result);
        await  _context.SaveChangesAsync();
    }
    
}