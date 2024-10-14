using System.ComponentModel.DataAnnotations;

namespace DDDSample1.Auth;

public class Login {

    // This property represents the username input from a user during the login process.
    // Usernames are essential for identifying the user in the system.
    // The Required attribute makes sure that the username field is not empty.
    // The StringLength attribute can be used to specify the maximum length of the username.
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, ErrorMessage = "Username must be less than 100 characters.")]
    public string Username { get; set; }

    // This property is used to store the password input from a user during the login process.
    // Passwords need secure handling to prevent unauthorized access.
    // The Required attribute ensures the password field is not left empty.
    // The StringLength attribute can specify minimum and maximum password lengths, enhancing security.
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
    public string Password { get; set; }
    
}