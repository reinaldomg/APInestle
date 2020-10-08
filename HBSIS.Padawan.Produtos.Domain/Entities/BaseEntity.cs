using HBSIS.Padawan.Produtos.Domain.Interfaces;
using System;

namespace HBSIS.Padawan.Produtos.Domain.Entities
{
    public abstract class BaseEntity 
    {
        public Guid Id { get; set; }
    }
}