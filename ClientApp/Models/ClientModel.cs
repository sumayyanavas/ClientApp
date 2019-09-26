using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApp.Models
{
    public class ClientModel
    {
      
            public int ClientId { get; set; }
            public string ClientName { get; set; }
            public string Address { get; set; }
            public string PhoneNumber { get; set; }
            public int UserId { get; set; }
            public string Image { get; set; }
        
        
    }
}