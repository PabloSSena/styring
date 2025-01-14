using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Task;

namespace api.Dtos.Task
{

    public class ValidDueDateAttribute : ValidationAttribute
    {
        public ValidDueDateAttribute() : base("DueDate cannot be in the past."){}

        public override bool IsValid(object value)
        {
            if (value is DateOnly dueDate)
            {
                return dueDate >= DateOnly.FromDateTime(DateTime.Now);
            }
            return false;
        }
    }

    public class ValidStatusAttribute:ValidationAttribute
    {
        private static readonly string[] Allowed = {"Pendente", "Em andamento", "Concluído"};

        public ValidStatusAttribute() : base("Invalid status. Allowed values are 'Pendente', 'Em andamento', or 'Concluído'.") {}

        public override bool IsValid(Object value)
        {
            if(value is string status)
            {
                return Allowed.Contains(status);
            }
            return false;
        } 
    }


    public class TaskDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must have at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Title must have 100 characters at maximum")]
        public string Title {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;

        [Required]
        [ValidDueDate]
        public DateOnly DueDate {get;set;}

        [Required]
        [ValidStatus]
        public string Status {get;set;} =  string.Empty;

        [Required]
        public int UserId {get;set;}

    }
    
}