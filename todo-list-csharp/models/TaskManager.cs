using System;
using System.Text.Json;
using Models;
public class TaskOperations
{
    public void Create(string input)
    {
        var db = new AppDbContext();
        string pattern = @"""(?<txt>[^""]*)""";
        var newTask = new ToDoTask
        {
            Description = Regex.Match(input, pattern).Groups["txt"].Value
        };
        db.items.Add(newTask);
        db.SaveChanges();
        Console.WriteLine("Task Added!");
    }

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
