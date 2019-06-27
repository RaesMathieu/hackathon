using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arena42.Models.DTO
{
    public class Selection
    {
        public int Id { get; set; }

        public string Odds { get; set; }

        public string Name { get; set; }

        public string ImgUrl { get; set; }

        public bool? Result { get; set; }

    }
}