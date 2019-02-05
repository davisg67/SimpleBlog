using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog
{
    public static class Database
    {
        private const string SessionKey = "SimpleBlog.Database.SessionKey";
        private static ISessionFactory _sessionFactory;

        //Exposes the session so controllers can use it.
        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();

            //Configurations set in Web.config
            config.Configure();

            //Add our mappings - mapping requires entity, entity is a class representing
            //a row in the database. Class in User file in Models.
            var mapper = new ModelMapper(); //Create mapper obj
            mapper.AddMapping<UserMap>();  //Define mappings to mapper obj.

            //Adds mappings to configuration object.
            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            //Create session factory
            _sessionFactory = config.BuildSessionFactory();
        }


        //Global.asax - BeginRequest() opens session. EndRequest() close session.
        public static void OpenSession()
        {
            //Track the open session.
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }

    }
}