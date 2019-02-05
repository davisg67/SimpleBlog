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
        private const int WorkFactor = 13;

        //Used to defeat timing check to see if user name is valid.
        //Hash method takes ~500ms. Normally if the user does not exist the time is very low.
        //This masks that difference to hide if user exists in database.
        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }

        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
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