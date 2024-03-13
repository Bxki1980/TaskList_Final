namespace TaskList_Final_.Repositories
{
    public interface ILoginRepository
    {
        public bool LoginValidation(String Username, String Password);
    }
}
