using System;

namespace acessoDadosC.NETDapperSQL.Models
{
    public class CareerItem
    {
        public Guid Id { get; set; }
        public String Title { get; set; }

        public Courses Courses { get; set; }
    }
}