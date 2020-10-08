using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Padawan.Produtos.Domain.Result
{
    public class Result<T>
    {
        public Result()
        {
        }
        public Result(T entity)
        {
            Entity = entity;
        }

        public bool IsValid { get; set; } = true;
        public List<string> ErrorList { get; set; } = new List<string>();
        public T Entity { get; set; }



    }
}
