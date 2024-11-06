namespace Backend.Domain.Auth;

public static class HospitalClaims {

    // As strings literais têm de começar por minúscula! Só assim funciona em
    // conformidade com os padrões JSON.

    public const string Id = "id";

    public const string Username = "username";

    public const string Email = "email";

    public const string Role = "role";

}