using System;
using System.ComponentModel;
using System.Data.Common;
using System.Text.Json;
using System.Text.RegularExpressions;
using Task;
using System.IO;
using System.Data;


public class TaskOperations
{
    public void Commands(string input)
    {
        var commands = new List<string> { "add", "delete", "list", "update", "mark-in-progress", "mark-done", "list-done" };
        foreach(string command in commands)
        {
            Console.WriteLine(command);
        }
    }

    public string Validate(string input)
    {
        var commands = new List<string> { "add", "delete", "list", "update", "mark-in-progress", "mark-done", "commands" };

        var validCommands = commands
            .Where(command => input.Contains(command, StringComparison.OrdinalIgnoreCase))
            .ToList();


        if (validCommands.Count == 1)
        {
            return validCommands[0];
        }
        else if (validCommands.Count > 1)
        {
            return "invalid commmand";
        }
        else
        {
            return "invalid, type 'commands' to see all commands";
        }
    }

    public void Create(string input)
    {
        string pattern = @"""(?<txt>[^""]*)""";
        
        Match match = Regex.Match(input, pattern);
        
        if(match.Success)
        {
            
            List<ToDoTask> todoList = new List<ToDoTask>();
            FileHandler.LoadJson(ref todoList);
            string description = match.Groups["txt"].Value;
            var todo = new ToDoTask()
            {
                Id = todoList.Count + 1,
                Description = description
            
            };
            todoList.Add(todo);
            FileHandler.Savejson(todoList); 
                
        }

        else
        {
            Console.WriteLine("no description found, please type a description eg. add [id] 'description'");
        }  
    }

    
    public void Update(string input)
    {
        string pattern = @"update\s+(?<id>\d+)\s+""(?<txt>[^""]*)""";
        Match match = Regex.Match(input, pattern);

        if(match.Success)
        {
            List<ToDoTask> todoList = new List<ToDoTask>();

            FileHandler.LoadJson(ref todoList);
            string description = match.Groups["txt"].Value;
            int id = Convert.ToInt32(match.Groups["id"].Value);
            var taskToUpdate = todoList.FirstOrDefault(t => t.Id == id);

            if(taskToUpdate != null)
            {
                taskToUpdate.Description = description;
                taskToUpdate.UpdatedAt = DateTime.UtcNow;
                
                FileHandler.Savejson(todoList);
            }
            else
            {
                Console.WriteLine("no id found, type list to see all tasks");
            }
        }
        else
        {
            Console.WriteLine("no description found, please type a description eg. update [id] 'description' ");
        }  
    }


    public void Delete(string input)
    {
        string pattern = @"\bdelete\s+(?<id>\d+)";
        Match match = Regex.Match(input, pattern);

        if(match.Success)
        {
            List<ToDoTask> todoList = new List<ToDoTask>();
            FileHandler.LoadJson(ref todoList);
            int id = Convert.ToInt32(match.Groups["id"].Value);
            var taskToDelete = todoList.FirstOrDefault(t => t.Id == id);

            if(taskToDelete != null)
            {
                todoList.Remove(taskToDelete);
                FileHandler.Savejson(todoList);
            }
            
        }
        else
        {
            Console.WriteLine("Error, no id found");
        }

    }

    public void MarkInProgress(string input)
    {
        string pattern = @"\bmark-in-progress\s+(?<id>\d+)";
        Match match = Regex.Match(input, pattern);

        if(match.Success)
        {
            List<ToDoTask> todoList = new List<ToDoTask>();
            FileHandler.LoadJson(ref todoList);

            string status = "in-progress";
            int id = Convert.ToInt32(match.Groups["id"].Value);
            var statusToUpdate = todoList.FirstOrDefault(t => t.Id == id);

            if(statusToUpdate != null)
            {
                statusToUpdate.Status = status;
                statusToUpdate.UpdatedAt = DateTime.UtcNow;
                FileHandler.Savejson(todoList);
            }
        }
    }

    public void MarkDone(string input)
    {
        string pattern = @"\bmark-done\s+(?<id>\d+)";
        Match match = Regex.Match(input, pattern);

        if(match.Success)
        {
            List<ToDoTask> todoList = new List<ToDoTask>();
            FileHandler.LoadJson(ref todoList);

            string status = "done";
            int id = Convert.ToInt32(match.Groups["id"].Value);
            var statusToUpdate = todoList.FirstOrDefault(t => t.Id == id);

            if(statusToUpdate != null)
            {
                statusToUpdate.Status = status;
                statusToUpdate.UpdatedAt = DateTime.UtcNow;
                FileHandler.Savejson(todoList);
            }
        }
    }
   public void ListAll(string input)
    {
        string pattern = @"\blist\s+(\w+)";

        Match match = Regex.Match(input, pattern);
       
        if(!match.Success)
        {
            Console.WriteLine("All");
            Console.WriteLine(string.Format("{0,-5} | {1,-20} | {2,-10} | {3}", "ID", "Description", "Status", "Created At", "Last Update"));
            Console.WriteLine(new string('-', 65));
            List<ToDoTask> todoList = new List<ToDoTask>();
            FileHandler.LoadJson(ref todoList);
            if(todoList == null || todoList.Count == 0)
            {
                Console.WriteLine("no tasks");
                return;
            }
            
            foreach(var task in todoList)
            {
                string createdAt = task.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
                string updatedAt = task.UpdatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
                Console.WriteLine(string.Format("{0,-5} | {1,-20} | {2,-10} | {3}", 
                task.Id, task.Description.Length > 20 ? task.Description.Substring(0, 17) + "..." : task.Description, 
                task.Status, createdAt, updatedAt));
            }
        }
        else if(match.Success)
        {          
            string status = "";
            List<ToDoTask> todoList = new List<ToDoTask>();
            FileHandler.LoadJson(ref todoList);
            var statusList = new List<string> {"done", "todo", "in-progress"};
            var validStatus = statusList.Where(status => input.Contains(status, StringComparison.OrdinalIgnoreCase)).ToList();
            if(validStatus.Count == 1)
            {
                status = validStatus[0];
                var resultados = todoList.Where(i => i.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

                if(resultados == null || resultados.Count == 0)
                {
                    Console.WriteLine("no tasks");
                    return;
                }
                
                foreach(var task in resultados)
                {
                    Console.WriteLine(string.Format("{0,-5} | {1,-20} | {2,-10} | {3}", "ID", "Description", "Status", "Created At", "Last Update"));
                    Console.WriteLine(new string('-', 65));
                    string createdAt = task.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
                    string updatedAt = task.UpdatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm");
                    Console.WriteLine(string.Format("{0,-5} | {1,-20} | {2,-10} | {3}", 
                    task.Id, task.Description.Length > 20 ? task.Description.Substring(0, 17) + "..." : task.Description, 
                    task.Status, createdAt, updatedAt));
                }
            }
            else if(validStatus.Count>1)
            {
                Console.WriteLine("error multiple status");
            }
            else
            {
                Console.WriteLine("status not found");
            }

            
        }
    }
}