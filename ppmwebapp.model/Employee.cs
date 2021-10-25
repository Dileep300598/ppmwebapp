using System;


namespace ppmwebapp.model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long Contact { get; set; }
        public int RoleId { get; set; }
    }
}
