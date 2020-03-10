using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace WebPrsTest.Models {
    public class RequestLine {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Request Request { get; set; }
        public virtual Product Product { get; set; }

        public RequestLine(){}

    }
}
