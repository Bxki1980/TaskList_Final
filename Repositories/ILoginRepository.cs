        
using TaskList_Final_.Models;

namespace TaskList_Final_.Repositories
{
    public interface ILoginRepository
    {
        //to check the username and password
        bool Authenticate(String UserName, string password);

        //to create new account
        UserModel CreateAcc(String FirstName, String LastName, String UserName, String Password);

        //Read
        Task<IEnumerable<UserModel>> getAll();

        //Delete
        void DeleteAccount(int ID);

        //Update
        Task UpdateUser(int ID , String FirstName, String LastName, String UserName, String Password);

    }

}
