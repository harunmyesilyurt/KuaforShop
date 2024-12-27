using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Application.Models
{
    public class AIResponse
    {
        public bool Success { get; set; }
        public Data Data { get; set; }
    }
    public class Shape
    {
        public string ClassName { get; set; }
        public string Probability { get; set; }
    }

    public class Data
    {
        public List<Shape> Shapes { get; set; }
        public List<string> Suggestions { get; set; }
    } 
}
