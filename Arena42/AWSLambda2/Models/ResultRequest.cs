using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSLambda2.Models
{
    public class ResultRequest
    {
        public int SelectionId { get; set; }

        public bool Result { get; set; }

    }
}