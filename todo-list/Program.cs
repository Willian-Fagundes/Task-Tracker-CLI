// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using Task;
using System.Text.RegularExpressions;
using System.Data.Common;

TaskOperations taskOperations = new TaskOperations();

bool valid = true;

while(valid == true)
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.Write("task-cli ");
    Console.ResetColor();
    string? input = Console.ReadLine().ToLower();
    

    if (input != null)
    {
        string command = taskOperations.Validate(input);
        if(command.Equals("add"))
        {
            taskOperations.Create(input);
        }
        else if(command.Equals("update"))
        {
            taskOperations.Update(input);
        }  
         
        else if(command.Equals("delete"))
        {
            taskOperations.Delete(input);           
        }

        else if(command.Equals("mark-in-progress"))
        {
            taskOperations.MarkInProgress(input);
        }

        else if(command.Equals("mark-done"))
        {
            taskOperations.MarkDone(input);
        }
    
        else if (command.Equals("list"))
        {
            taskOperations.ListAll(input);
        }
        else if(command.Equals("commands"))
        {
            taskOperations.Commands(input);   
        }
       
        else if (command.Equals("quit"))
        {
            Console.WriteLine("Bye!");
            valid = false;
        }
        
        else
        {
            Console.WriteLine("Error, Command not found. Type 'commands' to see all commands");
        }
    }   
        
                
}




