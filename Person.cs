using SQLite;

namespace Plamen
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}