using DotNetFramewok.DataTable.Search;
using DotNetFramework.Data;
using DotNetFramework.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service.ApplyJob
{
  public  class JobDetailService: IJobDetailService
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
            if (jobDetail.JobId != 0)
            {
                _jobdetailRepo.Update(jobDetail);
            }
            else
            {
                _jobdetailRepo.Insert(jobDetail);

            }

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

