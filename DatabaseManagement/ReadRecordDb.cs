using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DatabaseManagement
{
    public class ReadRecordDb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string UserKey { get; set; }

        public int ComicId { get; set; }

        public string Name { get; set; }

        public string FirstLetter { get; set; }

        public string Cover { get; set; }

        public int ReadChapterId { get; set; }

        public string ReadChapterName { get; set; }

        public long ReadTime { get; set; }

        public int ReadPageId { get; set; }

        public int UpdateChapterId { get; set; }

        public string UpdateChapterName { get; set; }

        public long UpdateTime { get; set; }

        public int Accredit { get; set; }

        public int ComicType { get; set; }

        public int Sort { get; set; }

    }
}
