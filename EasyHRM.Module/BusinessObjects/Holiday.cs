using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace EasyHRM.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("List Manager")]
    public class Holiday : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Holiday(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        private DateTime _startDate;
        private DateTime _endDate;
        private string _description;
       


        [XafDisplayName("Ngày Bắt Đầu")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetPropertyValue("StartDate", ref _startDate, value); }
        }

        [XafDisplayName("Ngày Kết Thúc")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetPropertyValue("EndDate", ref _endDate, value); }
        }      

        [XafDisplayName("Mô Tả")]
        public string Description
        {
            get { return _description; }
            set { SetPropertyValue("Description", ref _description, value); }
        }

        [XafDisplayName("Loại Ngày Nghĩ")]
        public HolidayTypes HolidayTypes
        {
            get { return GetPropertyValue<HolidayTypes>("HolidayTypes"); }
            set { SetPropertyValue<HolidayTypes>("HolidayTypes", value); }
        }

        [XafDisplayName("Nhân Viên")]
        public Employee Employee
        {
            get { return GetPropertyValue<Employee>("Employee"); }
            set { SetPropertyValue<Employee>("Employee", value); }
        }     
    }
}
