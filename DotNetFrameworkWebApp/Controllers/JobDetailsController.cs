using DotNetFramewok.DataTable.Extension;
using DotNetFramewok.DataTable.Search;
using DotNetFramewok.DataTable.Sort;
using DotNetFramework.Service.JobDetailsService;
using DotNetFrameWork.Data;
using DotNetFrameworkWebApp.Code.Serialization;
using DotNetFrameworkWebApp.Models;
using DotNetFrameworkWebApp.Models.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DotNetFrameworkWebApp.Controllers
{
    public class JobDetailsController : BaseController
    {
        private readonly IJobDetailService _jobDetailService;
 


        public JobDetailsController(IJobDetailService jobDetailService)
        {
            this._jobDetailService = jobDetailService;
            
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DotNetFramewok.DataTable.DataTables.DataTable dataTable)
        {
            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //This Methods for seraching in dataTable
            var query = new SearchQuery<JobDetail>();

            if (!string.IsNullOrEmpty(dataTable.sSearch))
            {
                string sSearch = dataTable.sSearch.ToLower();
                query.AddFilter(q => q.Title.Contains(sSearch));
            }
            // This Methods for sorting in dataTable
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var sortDirection = Request["sSortDir_0"];
            switch (sortColumnIndex)
            {
                case 2:
                    query.AddSortCriteria(new ExpressionSortCriteria<JobDetail, string>(q => q.Title, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                default:
                    query.AddSortCriteria(new ExpressionSortCriteria<JobDetail, string>(q => q.MinimumQualification, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<JobDetail> jobDetails = _jobDetailService.GetJobList(query, out total).Entities;

            foreach (JobDetail job in jobDetails)
            {
                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    job.JobCode.ToString(), //0
                    count.ToString(),//1
                    job.Title,//2
                    job.MinimumQualification,//3
                    job.JobDescription,//4
                    job.ApplicationLastDate.ToString(),//5
                    
                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }

        [HttpGet]
        public PartialViewResult AddEditJobDetails(int id=0)
        {
            JobDetailsViewModel model = new JobDetailsViewModel();
            if (id > 0)
            {

                JobDetail jobDetail = _jobDetailService.GetJobDetails(id);
                model.JobCode = jobDetail.JobCode;
                model.Title = jobDetail.Title;
                model.MinimumQualification = jobDetail.MinimumQualification;
                model.JobDescription = jobDetail.JobDescription;            
                model.ApplicationLastDate = DateTime.Now;
             
            }
            return PartialView("_AddEditJobDetails",model);
        }

        [HttpPost]
        public ActionResult AddEditJobDetails(JobDetailsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JobDetail jobDetail = new JobDetail();

                    if (model.JobCode > 0)
                    {
                        jobDetail = _jobDetailService.FindById(model.JobCode);
                        
                    }

                    jobDetail.Title = model.Title?.Trim() ?? "";
                    jobDetail.MinimumQualification = model.MinimumQualification;
                    jobDetail.JobDescription = model.JobDescription;
                    jobDetail.ApplicationLastDate = model.ApplicationLastDate;
                   
                    _jobDetailService.SaveJobDetails(jobDetail);

                }

                return RedirectToAction("Index", "JobDetails");
                //return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("CircularIndex", "Circular") });
            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("CircularIndex", "Circular") });
            }

        }


        #region [Delete Job details]
        [HttpGet]
        public PartialViewResult DeleteJobDetails()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Size = ModalSize.Medium,
                IsHeader = true,
                Message = "Are you sure want to delete this Job Detail?",
                Header = new ModalHeader { Heading = "Delete Job Detail" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        public ActionResult DeleteJobDetails(int id)
        {
            try
            {
                int get = _jobDetailService.DeleteStudentPresenter(id);
                if (get == id)
                {
                    ShowSuccessMessage("Success", "Student is successfully deleted", false);
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Data Deleted" });
                }
                else
                {
                    return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Error Occourd" });
                }

            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }
        }
        #endregion

    }
}