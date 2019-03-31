using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Modelized.Attributes;
using Modelized.Models;

namespace Modelized.Server.Tests.Models
{
    [Route("/test")]
    [Provide(typeof(SqlProvider))]
    [Table("test")]
    public class TestModel : IModel
    {
        [Key]
        public long? Id { get; set; }

        public string Value { get; set; }
    }
}
