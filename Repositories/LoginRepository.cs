using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Tokens;
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
            throw new ArgumentException("Password is required");

        if (_loginContext.AuthenticateModel.Any(x => x.UserName == user.UserName))
            throw new ArgumentException("Username \"" + user.UserName + "\" is already taken");

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new ArgumentException("FirstName is mandetory");

        if (string.IsNullOrWhiteSpace(LastName))
            throw new ArgumentException("LastName is mandetory");

        user.FirstName = FirstName;
        user.LastName = LastName;
        user.UserName = UserName;
        user.Password = Password;


        _loginContext.UserModel.Add(user);
        _loginContext.SaveChanges();

        return user;
    }

    public async void DeleteAccount(int ID)
    {
        var user = await _loginContext.UserModel.FindAsync(ID);
        if (user != null) 
        {
            _loginContext.UserModel.Remove(user);
            _loginContext.SaveChanges();
        }
        
    }

    public async Task<IEnumerable<UserModel>> getAll()
    {
        try
        {
            return await _loginContext.UserModel.OrderBy(p => p.ID).ToListAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task UpdateUser(int ID, string FirstName, string LastName, string UserName, string Password)
    {
        var user = await _loginContext.UserModel.FindAsync(ID);
        if (user != null)
        {
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.UserName = UserName;
            user.Password = Password;

            _loginContext.UserModel.Update(user);
            await _loginContext.SaveChangesAsync();
        }
    }

}
