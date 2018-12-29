using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace Core.Models
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
