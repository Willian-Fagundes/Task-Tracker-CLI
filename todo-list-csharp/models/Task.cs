using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using models;

namespace Models;

public class ToDoTask
{
    public int Id{get;set;}
    public string Description{get;set;}
    public string Status{get;set;} = "Pendente";
    [DataType(DataType.Date)]
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get;set;} = DateTime.UtcNow;
}




