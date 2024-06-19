using CPApp.constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CPApp.Domains
{
    public class CustomQuestion : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public Guid EmployerId { get; set; }
        public QuestionType Type { get; set; }
        public string Question { get; set; }
        public HashSet<string>? Options { get; set; }
        public bool? enableOthersOption { get; set; }
    }
}
