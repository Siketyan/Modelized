using System.Linq;
using Modelized.Models;

namespace Modelized.Server.Providers
{
    public interface IProvider
    {
        IQueryable<T> Load<T>() where T : IModel;
        void Add<T>(T model) where T : IModel;
        void Save();
    }
}
