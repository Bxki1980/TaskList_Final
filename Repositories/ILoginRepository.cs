using TaskList_Final_.Models;

namespace TaskList_Final_.Repositories
{
    public interface ILoginRepository 
    {
        //to check the username and password
        LoginModel Authenticate(String Username, string password);

        //to create new account
    }
    
}
