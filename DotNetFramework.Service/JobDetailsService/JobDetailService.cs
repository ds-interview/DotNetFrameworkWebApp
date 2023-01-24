using DotNetFramewok.DataTable.Search;
using DotNetFramework.Repo;
using DotNetFrameWork.Data;

namespace DotNetFramework.Service.JobDetailsService
{
    public class JobDetailService : IJobDetailService
    {
        private readonly IRepository<JobDetail> _jobdetailRepo;
     
        public JobDetailService(IRepository<JobDetail> jobdetailRepo)
        {
            _jobdetailRepo = jobdetailRepo;
            
        }
        public int DeleteStudentPresenter(int id)
        {

            _jobdetailRepo.Delete(id);
            return id;
        }
        public JobDetail GetJobDetails(int id)
        {
            return _jobdetailRepo.FindById(id);
        }
        public JobDetail UpdateJobDetails(JobDetail jobDetail)
        {

            _jobdetailRepo.Update(jobDetail);
            return jobDetail;
        }
        public JobDetail SaveJobDetails(JobDetail jobDetail)
        {
            _jobdetailRepo.Insert(jobDetail);
            return jobDetail;
        }
        public JobDetail FindById(int id)
        {
            return _jobdetailRepo.FindById(id);
        }
        public PagedListResult<JobDetail> GetJobList(SearchQuery<JobDetail> query, out int totalItems)
        {
            return _jobdetailRepo.Search(query, out totalItems);
        }
    }
}
