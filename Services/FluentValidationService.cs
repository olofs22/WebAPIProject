using FluentValidation;
using WebAPIProject.TodoDTOs;
using WebAPIProject.Models;

namespace WebAPIProject.Services
{
    public class CreateTodoRequestValidator : AbstractValidator<TodoCreateDTO> //fluentvalidation class that sets rules for different attributes
    {
        public CreateTodoRequestValidator() //validator for creating a tournament, Title has to be minimum 3 characters and startdate has to be in the future, cant be past time
        {
           
        }
    }

}