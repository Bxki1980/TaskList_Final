using TaskList_Final_.Models;

namespace TaskList_Final_.Repositories
{
    public interface ILoginRepository
    {
        // Authenticate user and return JWT token
        Task<string> AuthenticateAsync(string userName, string password);

        // Create new account
        Task CreateAccountAsync(UserModel user);

        // Get all users
        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        // Delete user account
        Task<bool> DeleteAccountAsync(int id);

        // Update user
        Task<bool> UpdateUserAsync(int id, UserModel user);
    }
}
