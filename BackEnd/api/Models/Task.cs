using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;

namespace api.Models
{
    public class Tasks
    {
        public int Id {get; set;}

        public string Title {get;set;} = string.Empty;

        public string Description {get;set;} = string.Empty;

        public DateOnly DueDate {get;set;}

        public string Status {get;set;} =  string.Empty;

        public int UserId {get;set;}
        public UsersDto? User {get;set;}
    }
}