using DataAccessLayer.Model.Enum;

namespace DataAccessLayer.Model
{
    public class Critic
    {
        public Critic(int lineNumber, object content, string column, TypeEnum expectedType)
        {
            this.LineNumber = lineNumber;
            this.Content = content;
            this.Column = column;
            this.ExpectedType = expectedType;
        }

        public int LineNumber { get; private set; }

        public object Content { get; private set; }

        public string Column { get; private set; }

        public TypeEnum ExpectedType { get; private set; }

        public string Description =>
            $"Error: At line number {this.LineNumber} in {this.Column}. Expected type {ExpectedType.ToString()} but has {this.Content}";
    }
}
