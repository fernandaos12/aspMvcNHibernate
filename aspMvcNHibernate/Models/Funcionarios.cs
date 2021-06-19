using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspMvcNHibernate.Models
{
    public class Funcionarios
    {
        //virtual para usar o lazyload
        public virtual long Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual int Idade { get; set; }
        public virtual double Salario { get; set; }
    }
}
