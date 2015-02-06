using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DatabaseManagement
{
    [Table("Users")]
    public class User
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }

        
        public string Name { get; set; }

        public string City { get; set; }

        public string Sex { get; set; }

        //public int Age { get; set; }
    }
}
