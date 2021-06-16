using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlePessoas.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            CriadoEm = DateTime.Now;
            Ativo = true;
        }

        public int Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public bool Ativo { get; private set; }
        public void Desativar() => Ativo = false;
    }
}
