using System;
using Domain.Interfaces;

namespace Domain
{
    [Serializable]
    public class Aluno : IEntity
    {
        public int Id { get; set ; }
    }
}