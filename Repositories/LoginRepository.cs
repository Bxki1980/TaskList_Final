using TaskList_Final_.Data;
using TaskList_Final_.Models;
using TaskList_Final_.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly LoginContex _loginContext;

    public LoginRepository(LoginContex context)
    {
        _loginContext = context;
    }

    // It has problems !!!
    public bool Authenticate(string Username, string Password)
    {
        // Check if username or password is null or empty
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            return false;
        }

        // Check if user exists with the provided username
        var user = _loginContext.AuthenticateModel.SingleOrDefault(c => c.UserName == Username);
        if (user == null)
        {
            return false;
        }

        // Check if password is correct for the user
        if (user.Password != Password)
        {
            return false;
        }

        // Authentication successful
        return true;
    }


    public UserModel CreateAcc(String FirstName, String LastName, String UserName, String Password)
    {
        var user = new UserModel();

        // validation
        if (string.IsNullOrWhiteSpace(Password))
            throw new("Password is required");

        if (_loginContext.AuthenticateModel.Any(x => x.UserName == user.UserName))
            throw new("Username \"" + user.UserName + "\" is already taken");

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new("FirstName is mandetory");

        if (string.IsNullOrWhiteSpace(LastName))
            throw new("LastName is mandetory");

        user.FirstName = FirstName;
        user.LastName = LastName;
        user.UserName = UserName;
        user.Password = Password;


        _loginContext.UserModel.Add(user);
        _loginContext.SaveChanges();

        return user;
    }
}
