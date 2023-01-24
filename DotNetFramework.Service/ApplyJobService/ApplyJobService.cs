using DotNetFramework.Data;
using DotNetFramework.DataTable.Search;
using DotNetFramework.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service.ApplyJobService
{
    class ApplyJobService: IApplyJobService
    {


        private readonly IRepository<JobDetail> _JobMasterRepo;

        public ApplyJobService(IRepository<JobDetail> CircularMasterRepo)
        {
            _JobMasterRepo = CircularMasterRepo;


        }
        public List<JobDetail> GetCircularAllocation()
        {
            return _JobMasterRepo.Query().Get().ToList();
        }

        public IEnumerable<JobDetail> GetCircularAllocationPresenters()
        {
            return _JobMasterRepo.Query().Get();
        }

        public bool DeleteCircularAllocation(int id)
        {
            try
            {
                _JobMasterRepo.Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public JobDetail SaveCircularAllocation(JobDetail circularmaster)
        {
            if (circularmaster.JobId > 0)
            {
                _JobMasterRepo.Update(circularmaster);
            }
            else
            {
                _JobMasterRepo.Insert(circularmaster);
            }
            return circularmaster;
        }

        public JobDetail FindById(int id)
        {
            return _JobMasterRepo.FindById(id);
        }
        public JobDetail GetCircularAllocationPresenter(int id)
        {
            return _JobMasterRepo.FindById(id);
        }
        public JobDetail SaveCircularAllocationPresenter(JobDetail circularmaster)
        {
            _JobMasterRepo.Insert(circularmaster);
            return circularmaster;
        }

        public JobDetail UpdateCircularAllocationPresenter(JobDetail circularmaster)
        {
            _JobMasterRepo.Update(circularmaster);
            return circularmaster;
        }

        public int DeleteCircularAllocationPresenter(int id)
        {
            _JobMasterRepo.Delete(id);
            return id;
        }


        public PagedListResult<JobDetail> GetCircularAllocationList(SearchQuery<JobDetail> query, out int totalItems)
        {
            return _JobMasterRepo.Search(query, out totalItems);
        }
        public List<JobDetail> GetCircularFacultyById(int Circularfor)
        {
            return _JobMasterRepo.Query().Filter(x => x.JobId == Circularfor).Get().ToList();
        }


    }

}

