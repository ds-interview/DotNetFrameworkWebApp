using DotNetFramework.Data;
using DotNetFramework.DataTable.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFramework.Service.ApplyJobService
{
    interface IApplyJobService
    {
        List<JobDetail> GetCircularAllocation();
        JobDetail SaveCircularAllocation(JobDetail circularmaster);
        bool DeleteCircularAllocation(int id);
        JobDetail FindById(int id);
        JobDetail GetCircularAllocationPresenter(int id);
        PagedListResult<JobDetail> GetCircularAllocationList(SearchQuery<JobDetail> query, out int totalItems);
        List<JobDetail> GetCircularFacultyById(int Circularfor);
        //List<CircularMaster> GetCircularAllocationById(int Id);
        JobDetail SaveCircularAllocationPresenter(JobDetail circularmaster);
        JobDetail UpdateCircularAllocationPresenter(JobDetail circularmaster);
        int DeleteCircularAllocationPresenter(int id);
        //List<CircularMaster> GetCircularAllocationByClass(int classId);
    }


}

