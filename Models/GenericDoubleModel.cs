using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppProject.Models;

namespace WebAppProject.Models
{
    public class GenericDoubleModel<T,E>
    {
        public T? Model_1 { get; set; }
        public E? Model_2 { get; set; }
    }
}
