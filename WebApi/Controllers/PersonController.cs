using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class PersonController
{
        private PersonServices _personServices;

        public PersonController (PersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpGet("GetPersonsWithoutDapper")]
        public async Task<List<Person>> GetPersonsWithoutDapper()
        {
            return await _personServices.GetPersonsWithoutDapper();
            
            
        }
        [HttpGet("GetPersonWhithDapper")]
        public async Task<List<Person>> GetPersonWhithDapper()
        {
            return await _personServices.GetPersonWhithDapper();
        }
        
        [HttpGet("GetPersonWhithEntity")]
        public async Task<List<Person>>GetPersonWhithEntity()
        {
            return await  _personServices.GetPersonWhithEntity();
        }
        
        [HttpPost("AddPerson")]
        public async void AddPerson(Person person)
        {
            _personServices.AddPerson(person);
           
             
        }

      
}