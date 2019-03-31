using System; 

namespace Modelized.Models
{
    public class UniqueModel : IModel
    {
        public string Id { get; set; }

        public UniqueModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
