using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApi.Application.Authentication;
using MyApi.Domain.Entities;

namespace MyApi.Application.Authentication
{
    public interface ICurrentUser
    {
        User LoggedInUser();
        string LoggedInUserEmail();
        Guid LoggedInUserId();
    }
}