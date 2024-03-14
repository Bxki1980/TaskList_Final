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
        if ( user.Password != password)
            return null;

        // authentication successful
        return user;
    }

}
