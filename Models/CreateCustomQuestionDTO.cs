using CPApp.constants;

namespace CPApp.dtos
{
    public class CreateCustomQuestionDTO
    {
        public string Type { get; set; }
        public string Question { get; set; }
        public HashSet<string>? Options { get; set; }
        public bool? enableOthersOption { get; set; }
    }
}
