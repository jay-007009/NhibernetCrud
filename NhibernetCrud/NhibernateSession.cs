using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NhibernetCrud
{
    public class NhibernateSession
    {
        public static NHibernate.ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = MyServer.MapPath(@"Models\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var employeeConfigurationFile = MyServer.MapPath(@"Mappings\Employee.hbm.xml");
            configuration.AddFile(employeeConfigurationFile);
            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }




    }
}
