using System;

namespace acessoDadosC.NETDapperSQL.Models
{
    public class Career
    {
            public Career()
            {
                Items = new List<CareerItem>();
            }
        
        public Guid Id { get; set; }
        public String Title { get; set; }

        public IList<CareerItem> Items { get; set; }
    }
}