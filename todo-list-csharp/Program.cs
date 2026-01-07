// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using models;
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
        if(input.Contains("add"))
        {
            taskOperations.Create(input);
        }
        else if(input.Contains("update"))
        {
            taskOperations.Update(input);
        }    
        else if(input.Contains("delete"))
        {
            taskOperations.Delete(input);           
        }

        else if(input.Contains("mark-in-progress"))
        {
            taskOperations.MarkInProgress(input);
        }

        else if(input.Contains("mark-done"))
        {
            taskOperations.MarkDone(input);
        }
    
        else if (input.Contains("list"))
        {
            taskOperations.List(input);
        }

        else if (input.Contains("quit"))
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



