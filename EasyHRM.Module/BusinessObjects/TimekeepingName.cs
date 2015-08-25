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
    [NavigationItem("Quản Lý Chấm Công")]
    public class TimekeepingName : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public TimekeepingName(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        private string _timekeepingName;
        [XafDisplayName("Tên Bảng Chấm Công")]
        public string timekeepingName
        {
            get { return _timekeepingName; }
            set { SetPropertyValue("timekeepingName", ref _timekeepingName, value); }
        }

        private DateTime _startDate;
        [XafDisplayName("Ngày Bắt Đầu")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetPropertyValue("StartDate", ref _startDate, value); }
        }


        private DateTime _endDate;
        [XafDisplayName("Ngày Kết Thúc")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetPropertyValue("EndDate", ref _endDate, value); }
        }      

    }
}
