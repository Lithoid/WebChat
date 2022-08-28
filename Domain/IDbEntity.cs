using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public interface IDbEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}