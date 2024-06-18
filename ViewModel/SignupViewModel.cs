using System;
using System.ComponentModel.DataAnnotations;

public class SignupViewModel
{
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}