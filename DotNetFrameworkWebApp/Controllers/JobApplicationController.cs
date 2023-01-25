using DotNetFrameworkWebApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNetFrameworkWebApp.Data;
using DotNetFrameworkWebApp.DataTable.Extension;
using DotNetFrameworkWebApp.DataTable.Search;
using DotNetFrameworkWebApp.DataTable.Sort;
using DotNetFrameworkWebApp.Code;
using DotNetFrameworkWebApp.Model;
using Microsoft.Graph;
using DotNetFrameworkWebApp.Models;

namespace DotNetFrameworkWebApp.Controllers
{
    public class JobApplicationController : BaseController
    {
        private readonly IJobApplicationService _jobService;

        public JobApplicationController(IJobApplicationService jobService)
        {
            this._jobService = jobService;

        }

        [HttpGet]
        public ActionResult Index()
        {
            if(Session["LoginCheck"] == "Login")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Index(DotNetFrameworkWebApp.DataTable.DataTable dataTable)
        {
            List<DataTableRow> table = new List<DataTableRow>();

            List<int> column1 = new List<int>();
            for (int i = dataTable.iDisplayStart; i < dataTable.iDisplayStart + dataTable.iDisplayLength; i++)
            {
                column1.Add(i);
            }

            //This Methods for seraching in dataTable
            var query = new SearchQuery<JobApplication>();

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
                    query.AddSortCriteria(new ExpressionSortCriteria<JobApplication, string>(q => q.Title, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;
                default:
                    query.AddSortCriteria(new ExpressionSortCriteria<JobApplication, string>(q => q.Minimum_Qualification, sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending));
                    break;

            }
            //for pagination
            query.Take = dataTable.iDisplayLength;
            query.Skip = dataTable.iDisplayStart;

            int count = dataTable.iDisplayStart + 1, total = 0;

            //To get data from table  using searchquery
            IEnumerable<JobApplication> jobApplication = _jobService.GetJob(query, out total).Entities;

            foreach (JobApplication job in jobApplication)
            {
                table.Add(new DataTableRow("rowId" + count.ToString(), "dtrowclass")
                {
                    job.Job_Code .ToString(), //0
                    count.ToString(),//1
                    job.Title,//2
                    job.Minimum_Qualification,//3
                    job.Sort_Description,//4
                    job.Application_Last_Date.ToString(),//5
                    
                });
                count++;
            }
            return new DataTableResultExt(dataTable, table.Count(), total, table);
        }
        [HttpGet]
        public PartialViewResult AddEditJobApplication(int? id)
        {
            JobApplicationViewModel model = new JobApplicationViewModel();
            if (id != 0 && id != null)
            {

                JobApplication jobApplication = _jobService.GetJob(id.Value);
                model.Job_Code = jobApplication.Job_Code;
                model.Title = jobApplication.Title;
                model.Minimum_Qualification = jobApplication.Minimum_Qualification;
                model.Sort_Description = jobApplication.Sort_Description;
                model.Application_Last_Date = DateTime.Now;
            }
            return PartialView("_AddEditJobApplication", model);
        }

        [HttpPost]
        public ActionResult AddEditJobApplication(JobApplicationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isExist = model.Job_Code == 0 ? false : true;
                    JobApplication jobApplication = new JobApplication();
                    jobApplication = isExist ? _jobService.GetJob(model.Job_Code) : new JobApplication();
                    jobApplication.Job_Code = isExist ? model.Job_Code : 0;
                    jobApplication.Title = model.Title;
                    jobApplication.Minimum_Qualification = model.Minimum_Qualification;
                    jobApplication.Sort_Description = model.Sort_Description;
                    jobApplication.Application_Last_Date = model.Application_Last_Date;
                    var data = isExist ? _jobService.UpdateJob(jobApplication) : _jobService.SaveJob(jobApplication);
                }
                return RedirectToAction("Index");
                // return NewtonSoftJsonResult(new RequestOutcome<dynamic> {IsSuccess=true, RedirectUrl = @Url.Action("Index", "JobApplication") });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public PartialViewResult DeleteJobApplication()
        {
            return PartialView("_ModalDelete", new Modal
            {
                Size = ModalSize.Medium,
                IsHeader = true,
                Message = "Are you sure want to delete this Job?",
                Header = new ModalHeader { Heading = "Delete Job" },
                Footer = new ModalFooter { SubmitButtonText = "Yes", CancelButtonText = "No" }
            });
        }

        [HttpPost]
        public ActionResult DeleteJobApplication(int id)
        {
            try
            {
                var data = _jobService.GetJob(id);
                if(data != null)
                {
                    _jobService.DeleteJob(data);
                }
                ShowSuccessMessage("Success", "Student is successfully deleted", false);
                return RedirectToAction("Index");
                //return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "Data Deleted" });

            }
            catch (Exception ex)
            {
                return NewtonSoftJsonResult(new RequestOutcome<string> { Data = ex.GetBaseException().Message, IsSuccess = false });
            }
        }

    }

}