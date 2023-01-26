using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.DataTable.Search;
using DotNetFrameworkWebApp.Repo;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DotNetFrameworkWebApp.Service
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IRepository<JobApplication> repoJob;

        public JobApplicationService(IRepository<JobApplication> repoJob)
        {
            this.repoJob = repoJob;
        }

        public JobApplication SaveJob(JobApplication jobApplication)
        {
            jobApplication.Application_Last_Date = DateTime.UtcNow;
            repoJob.Insert(jobApplication);
            return jobApplication;
        }

        public JobApplication UpdateJob(JobApplication jobApplication)
        {
            repoJob.Update(jobApplication);
            return jobApplication;
        }

        public void DeleteJob(int id)
        {
            repoJob.Delete(id);            
        }
        public void DeleteJob(JobApplication jobApplication)
        {
            repoJob.Delete(jobApplication);
        }
        public JobApplication GetJob(int id)
        {
            return repoJob.FindById(id);
        }
        public IEnumerable<JobApplication> GetJobList()
        {
            return repoJob.Query().Get().ToList();
        }
        public PagedListResult<JobApplication> GetJob(SearchQuery<JobApplication> query, out int totalItems)
        {
            return repoJob.Search(query, out totalItems);
        }
        //public bool GetUser(int id, string email)
        //{
        //    return repoJob.Query().Filter(x => x.id == id && x.Title == email).Get().Any();

        //}

        //public JobApplication FindById(int job_Code)
        //{
        //    return repoJob.FindById()
        //}
    }
}
