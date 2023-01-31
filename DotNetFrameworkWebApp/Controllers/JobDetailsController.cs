using DotNetFramewok.DataTable.Extension;
using DotNetFramewok.DataTable.Search;
using DotNetFramework.Data;
using DotNetFramework.DataTable.Sort;
using DotNetFramework.DataTable.Sort.DotNetFramewok.DataTable.Sort;
using DotNetFramework.Service.ApplyJob;
using DotNetFrameworkWebApp.Code.Serialization;
using DotNetFrameworkWebApp.Model;
using DotNetFrameworkWebApp.Model.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DotNetFrameworkWebApp.Controllers
{
    public class JobDetailsController : BaseController
    {
        private readonly IJobDetailService _jobDetailService;

        public JobDetailsController(IJobDetailService jobDetail)
        {
            this._jobDetailService = jobDetail;


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
                    job.JobId.ToString(), //0
                    count.ToString(),//1
                    job.Title,//2
                    job.MinimumQualification,//3
                    job.SortDescription,//4
                    job.ApplicationLastdatetoapply.ToString(),//5
                    
                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }

        [HttpGet]
        public PartialViewResult AddEditJobDetails(int ?id)
        {
   

            JobDetailsViewModel model = new JobDetailsViewModel();

            JobDetail jobDetail = new JobDetail();
            if (id != 0 && id != null)
            {

                jobDetail = _jobDetailService.GetJobDetails(id.Value);

                //model.JobId = id > 0 ? id : 0;
                model.JobId = jobDetail.JobId;
                model.Title = jobDetail.Title;
                model.MinimumQualification = jobDetail.MinimumQualification;
                model.JobDescription = jobDetail.SortDescription;
                model.ApplicationLastDate = DateTime.Now;

            }
            return PartialView("_AddEditJobDetails", model);
        }

        [HttpPost]
        public ActionResult AddEditJobDetails(JobDetailsViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JobDetail jobDetail = new JobDetail();

                    if (model.JobId > 0)
                    {
                        jobDetail = _jobDetailService.FindById(model.JobId);

                    }

                    jobDetail.Title = model.Title?.Trim() ?? "";
                    jobDetail.MinimumQualification = model.MinimumQualification;
                    jobDetail.SortDescription = model.JobDescription;
                    jobDetail.ApplicationLastdatetoapply = model.ApplicationLastDate;
                   // jobDetail.JobId = model.JobId;

                    _jobDetailService.SaveJobDetails(jobDetail);

                }
                ShowSuccessMessage("Success!", "Job Details are added successfully", false);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<dynamic> { RedirectUrl = @Url.Action("Index") });
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
                    ShowSuccessMessage("Success", "Job Details are successfully deleted", false);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
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