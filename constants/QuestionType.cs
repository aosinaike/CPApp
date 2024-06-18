namespace CPApp.constants
{
    public enum QuestionType
    {
        PARAGRAPH,
        YESORNO,
        DROPDOWN,
        DATE,
        NUMBER,
    }

    public class QuestionTypeUtil
    {
        public QuestionType Type { get; }
        public static QuestionType ToQuestionType(string questionType)
        {
            switch (questionType)
            {
                case "PARAGRAPH":
                    return QuestionType.PARAGRAPH;
                case "YESORNO":
                    return QuestionType.YESORNO;
                case "DROPDOWN":
                    return QuestionType.DROPDOWN;
                case "DATE":
                    return QuestionType.DATE;
                case "NUMBER":
                    return QuestionType.NUMBER;
                default:
                    return QuestionType.PARAGRAPH;
            }
        }
    }
}
