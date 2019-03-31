using System.Linq;
using Microsoft.EntityFrameworkCore;
using Modelized.Models;
using Modelized.Server.Providers;
using Modelized.Server.Tests.Models;

namespace Modelized.Server.Tests
{
    public class SqlProvider : DbContext, IProvider
    {
        public DbSet<TestModel> TestModels { get; set; }

        public IQueryable<T> Load<T>() where T : IModel
        {
            if (typeof(T) == typeof(TestModel))
            {
                return (IQueryable<T>) TestModels;
            }

            return null;
        }

        public new void Add<T>(T model) where T : IModel
        {
            if (model is TestModel entity)
            {
                TestModels.Add(entity);
            }
        }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySQL(
                "server=localhost;userid=modelized;password=modelized;database=modelized;"
            );

            base.OnConfiguring(options);
        }
    }
}
