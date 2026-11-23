using System;
using System.Collections.Generic;
using System.Text;
using Biblioteka.Models;

namespace Biblioteka.Services
{
    internal interface IKategoriaRepository
    {
        Task<IReadOnlyList<Kategoria>> GetAllAsync();
    }
}
