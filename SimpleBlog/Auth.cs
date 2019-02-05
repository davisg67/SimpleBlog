﻿using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace SimpleBlog
{
    public static class Auth
    {
        private const string UserKey = "SimpleBlog.Auth.UserKey";

        //Represents currently logged in user.
        public static User User
        {
            get
            {
                //See if user is logged in.
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);
                    if (user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user;
                }

                return user;      
            }
        }
    }
}