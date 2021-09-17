using Autofac;
using ParkyAPI.Data;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Autofac
{
    public class AutofacServiceModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //base.Load(builder);
            //builder.RegisterType(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>();
            builder.RegisterGeneric(typeof(NationalParkRepository<>)).As(typeof(INationalParkRepository<>));
            builder.RegisterGeneric(typeof(TrailRepository<>)).As(typeof(ITrailRepository<>));
        }
    }
}
