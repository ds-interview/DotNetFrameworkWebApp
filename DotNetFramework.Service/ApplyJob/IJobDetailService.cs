using DotNetFramewok.DataTable.Search;
using DotNetFramework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service.ApplyJob
{
  public  interface IJobDetailService
    {
        int DeleteStudentPresenter(int id);
        JobDetail GetJobDetails(int id);
        JobDetail UpdateJobDetails(JobDetail jobDetail);
        JobDetail SaveJobDetails(JobDetail jobDetail);
        JobDetail FindById(int id);
        PagedListResult<JobDetail> GetJobList(SearchQuery<JobDetail> query, out int totalItems);
    }
}
