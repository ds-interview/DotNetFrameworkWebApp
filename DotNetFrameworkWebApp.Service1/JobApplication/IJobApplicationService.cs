using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.DataTable.Search;
using System.Collections.Generic;

namespace DotNetFrameworkWebApp.Service

{
    public interface IJobApplicationService
    {
        JobApplication GetJob(int id);
        JobApplication SaveJob(JobApplication jobApplication);
        JobApplication UpdateJob(JobApplication jobApplication);
        void DeleteJob(int id);
        void DeleteJob(JobApplication jobApplication);
        PagedListResult<JobApplication> GetJob(SearchQuery<JobApplication> query, out int totalItems);
        bool GetUser(int id,string email);
        IEnumerable<JobApplication> GetJobList();
    }
}
