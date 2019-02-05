using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        public virtual void SetPassword(string password)
        {
            PasswordHash = "IgnoreMe";
        }
    }

    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            //Which table to use.
            Table("users");

            //Expression identifies the primary key and that the key is incremented automatically by the database.
            Id(x => x.Id, x => x.Generator(Generators.Identity));

            //Set properties to map.
            Property(x => x.Username, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));

            //multiple lambda example - needs curly braces.
            Property(x => x.PasswordHash, x =>
            {
                x.Column("password_hash"); //Overrides the property name for column name.
                x.NotNullable(true);
            });
        }
    }
}