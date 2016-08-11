using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TechRadar.DataLayer.Interfaces
{
    public interface IDBContext : IDisposable
    {
        IDbConnection DB { get; }
    }
}
