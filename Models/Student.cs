using System;
using System.Collections.Generic;

namespace APIUsingToken.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public bool? Gender { get; set; }

    public DateTime? Dob { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    
}
