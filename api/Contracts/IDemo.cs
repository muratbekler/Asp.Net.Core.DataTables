using api.Models;
using DataTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Contracts
{
    public interface IDemo
    {
        Task<DTResult<PatientResult>> GetResultsAsync(DTParameters param);
    }
}
