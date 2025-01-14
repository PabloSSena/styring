using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Task
{
    public class UpdateTaskDto
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

    }
}