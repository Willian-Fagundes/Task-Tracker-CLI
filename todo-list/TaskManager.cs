using System;
using System.ComponentModel;
using System.Data.Common;
using System.Text.Json;
using System.Text.RegularExpressions;
using Task;
using System.IO;


public class TaskOperations
{
    
   
    public string Validate(string input)
    {
        var commands = new List<string> { "add", "delete", "list", "update" };

        var validCommands = commands
            .Where(command => input.Contains(command, StringComparison.OrdinalIgnoreCase))
            .ToList();


        if (validCommands.Count == 1)
        {
            return validCommands[0];
        }
        else if (validCommands.Count > 1)
        {
            return "Erro: A frase contém múltiplos comandos. Seja mais específico.";
        }
        else
        {
            return "Erro: Nenhum comando válido encontrado. Digite 'commands' para ver a lista de comandos";
        }
    }

    public void Create(string input)
    {
        List<ToDoTask> listaTarefas = new List<ToDoTask>();
        
        string pattern = @"""(?<txt>[^""]*)""";
        string description = "";
        Match match = Regex.Match(input, pattern);
        
        if(match.Success)
        {
            description = match.Groups["txt"].Value;
            Console.WriteLine(description);
            Console.WriteLine("Task Added!");
        }

        else
        {
            Console.WriteLine("Erro descrição não encontrada! Digite 'commands' para lista de comandos");
        }

        var todo = new ToDoTask()
        {
            Id = todoList.Count += 1,
            Description = description
            
        };
        todoList.Add(todo);
        SaveJson();
    }
}
    /*
    public void Update(string input)
    {
        string pattern = @"update\s+(?<id>\d+)\s+""(?<txt>[^""]*)""";
        int id = Convert.ToInt32(Regex.Match(input, pattern).Groups["id"].Value); 
        using (var db = new AppDbContext())
        {
            var UpdateTask = new ToDoTask
            {
                Id = id,
                Description = Regex.Match(input, pattern).Groups["txt"].Value,
                UpdatedAt = DateTime.UtcNow
            };

            db.items.Attach(UpdateTask);
            db.Entry(UpdateTask).Property(t => t.Description).IsModified = true;
            db.Entry(UpdateTask).Property(t => t.UpdatedAt).IsModified = true;

            db.SaveChanges();
            Console.WriteLine("Task Updated!");
        };
    }

    public void Delete(string input)
    {
        string Padrao = @"\bdelete\s+(?<id>\d+)";
        int id = Convert.ToInt32(Regex.Match(input, Padrao).Groups["id"].Value); 
        using (var db = new AppDbContext())
        {
            var DeleteTask = new ToDoTask
            {
                Id = id
            };
            db.Remove(DeleteTask);
            db.SaveChanges();
            Console.WriteLine("Task Deleted!");
        };
    }

    public void MarkInProgress(string input)
    {
        string Padrao = @"\bmark-in-progress\s+(?<id>\d+)";
        int id = Convert.ToInt32(Regex.Match(input, Padrao).Groups["id"].Value);
        using (var db = new AppDbContext())
        {
            var UpdateStatus = new ToDoTask
            {
                Id = id,
                Status = "in-progress",
                UpdatedAt = DateTime.UtcNow                  
            };
            db.items.Attach(UpdateStatus);
            db.Entry(UpdateStatus).Property(t => t.Status).IsModified = true;
            db.Entry(UpdateStatus).Property(t => t.UpdatedAt).IsModified = true;

            db.SaveChanges();
            Console.WriteLine("Status Updated!");
        }
    }

    public void MarkDone(string input)
    {
        string Padrao = @"\bmark-done\s+(?<id>\d+)";
        int id = Convert.ToInt32(Regex.Match(input, Padrao).Groups["id"].Value);
        using (var db = new AppDbContext())
        {
            var UpdateStatus = new ToDoTask
            {
                Id = id,
                Status = "done",
                UpdatedAt = DateTime.UtcNow                  
            };
            db.items.Attach(UpdateStatus);
            db.Entry(UpdateStatus).Property(t => t.Status).IsModified = true;
            db.Entry(UpdateStatus).Property(t => t.UpdatedAt).IsModified = true;

            db.SaveChanges();
            Console.WriteLine("Status Updated!");
        }
    }

    public void List(string input)
    {
        string padrao = @"(?<=list\s).*";
        Match match = Regex.Match(input, padrao);

        if(match.Success&&!string.IsNullOrWhiteSpace(match.Value))
        {
            string status = match.Value.Trim();
            
            using (var db = new AppDbContext())
            {
                var filteredItem = db.items.Where(i => i.Status == status).ToList();
                foreach (var i in filteredItem)
                {
                    Console.WriteLine($"id = {i.Id} | Description = {i.Description} | Status = {i.Status} | Created-At = {i.CreatedAt} | Upadated-At {i.UpdatedAt}");
                }
            }
            
        }   
        else
        {
            using (var db = new AppDbContext())
            {
                var allItems = db.items.ToList();
                foreach(var i in allItems)
                {
                    Console.WriteLine($"id = {i.Id} | Description = {i.Description} | Status = {i.Status} | Created-At = {i.CreatedAt} | Updated-At = {i.UpdatedAt}");
                };
            }
        }
    }
}
*/