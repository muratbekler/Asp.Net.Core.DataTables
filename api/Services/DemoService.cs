using api.Contracts;
using api.Models;
using DataTables.Infrastructure;
using DataTables.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace api.Services
{
    public class DemoService : IDemo
    {
        private readonly IConfigurationProvider _mappingConfiguration;
        public async Task<DTResult<PatientResult>> GetResultsAsync(DTParameters param)
        {
            HttpResponseMessage patientResponse = await new HttpClient().GetAsync("url");
            string patientString = await patientResponse.Content.ReadAsStringAsync();
            IEnumerable<PatientResult> patientList = JsonConvert.DeserializeObject<List<PatientResult>>(patientString);

            IQueryable<PatientResult> query = patientList.AsQueryable();
            query = new SearchOptionsProcessor<PatientResult, PatientResult>().Apply(query, param.Columns);
            query = new SortOptionsProcessor<PatientResult, PatientResult>().Apply(query, param);

            var items = await Task.Run(() => { return query.Skip(param.Start - 1 * param.Length).Take(param.Length).ToArray(); });

            return new DTResult<PatientResult> { Data = items, Draw = param.Draw, RecordsFiltered = items.Length, RecordsTotal = query.Count() };


            // entity model 

            //HttpResponseMessage patientResponse = await new HttpClient().GetAsync("url");
            //string patientString = await patientResponse.Content.ReadAsStringAsync();
            //IEnumerable<PatientResultEntity> patientList = JsonConvert.DeserializeObject<List<PatientResultEntity>>(patientString);

            //IQueryable<PatientResultEntity> query = patientList.AsQueryable();
            //query = new SearchOptionsProcessor<PatientResult, PatientResultEntity>().Apply(query, param.Columns);
            //query = new SortOptionsProcessor<PatientResult, PatientResultEntity>().Apply(query, param);

            //var items = await query
            //      .AsNoTracking()
            //      .Skip(param.Start - 1 * param.Length)
            //      .Take(param.Length)
            //      .ProjectTo<PatientResult>(_mappingConfiguration)
            //      .ToArrayAsync();

            //return items;
        }
    }
}
