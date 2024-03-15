using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.RegularExpressions;
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


    public LoginModel Authenticate(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        var user = _loginContext.LoginModel.SingleOrDefault(x => x.UserName == username);

        // check if username exists
        if (user == null)
            return null;

        // check if password is correct
        if (user.Password != password)
            return null;

        // authentication successful
        return user;
    }

    public LoginModel CreateAcc(String FirstName, String LastName, String UserName, String Password)
    {
        var user = new LoginModel();

        // validation
        if (string.IsNullOrWhiteSpace(Password))
            throw new("Password is required");

        if (_loginContext.LoginModel.Any(x => x.UserName == user.UserName))
            throw new("Username \"" + user.UserName + "\" is already taken");

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new("FirstName is mandetory");

        if (string.IsNullOrWhiteSpace(LastName))
            throw new("LastName is mandetory");

        user.FirstName = FirstName;
        user.LastName = LastName;
        user.UserName = UserName;
        user.Password = Password;


        _loginContext.LoginModel.Add(user);
        _loginContext.SaveChanges();

        return user;
    }
}
