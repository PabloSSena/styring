using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Task
{
    public class TaskResponseDto
    {
        public int Id {get; set;}

        public string Title {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;

        public DateOnly DueDate {get;set;}

        public string Status {get;set;} =  string.Empty;

    }
}