using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReactDemo.Models
{
    public class CommentModel
    {
        public string FechaHora { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public string Identificador { get; set; }
    }
}