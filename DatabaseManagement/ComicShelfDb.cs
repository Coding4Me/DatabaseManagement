using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DatabaseManagement
{

    [Table("ComicShelfDb")]
    public class ComicShelfDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string UserKey { get; set; }

        public int ComicId { get; set; }

        public string Name { get; set; }

        [MaxLength(20)]
        public string FirstLetter { get; set; }

        public string Cover { get; set; }

        public int UpdateChapterId { get; set; }

        public string UpdateChapterName { get; set; }

        public long UpdateTime { get; set; }

        public int ReadChapterId { get; set; }

        public string ReadChapterName { get; set; }

        public long ReadChapterTime { get; set; }

        public int ReadPageId { get; set; }

        public int Accredit { get; set; }

        public int ComicType { get; set; }

        public int Sort { get; set; }

        public bool IsLine { get; set; }
    }
}
