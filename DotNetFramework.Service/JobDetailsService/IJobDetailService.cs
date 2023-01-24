using DotNetFramewok.DataTable.Search;
using DotNetFrameWork.Data;

namespace DotNetFramework.Service.JobDetailsService
{
    public interface IJobDetailService
    {
        int DeleteStudentPresenter(int id);
        JobDetail GetJobDetails(int id);
        JobDetail UpdateJobDetails(JobDetail jobDetail);
        JobDetail SaveJobDetails(JobDetail jobDetail);
        JobDetail FindById(int id);
        PagedListResult<JobDetail> GetJobList(SearchQuery<JobDetail> query, out int totalItems);
    }
}
