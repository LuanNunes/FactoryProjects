using DataAccessLayer.Model.Enum;

namespace DataAccessLayer.Model
{
    public class FileConfiguration
    {
        public int IdClient { get; set; }

        public int IdFile { get; set; }

        public string Project { get; set; }

        public string FileName { get; set; }

        public string Worksheet { get; set; }

        public string ExtractTable { get; set; }

        public string ColumnName { get; set; }

        public TypeEnum TypeColumn { get; set; }
    }
}
