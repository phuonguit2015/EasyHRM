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
    public class HolidayTypes : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public HolidayTypes(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        
        private string _holidayTypesName;
        private string _description;


        [XafDisplayName("Tên Loại Ngày Nghĩ")]
        [Indexed(Unique = true)]
        public string HolidayTypesName
        {
            get { return _holidayTypesName; }
            set { SetPropertyValue("HolidayTypesName", ref _holidayTypesName, value); }
        }

        private bool _isPublicHoliday;
        [XafDisplayName("Ngày Lễ Pháp Định")]
        public bool IsPublicHoliday
        {
            get { return _isPublicHoliday; }
            set { SetPropertyValue("IsPublicHoliday", ref _isPublicHoliday, value); }
        }

        [XafDisplayName("Mô Tả")]
        public string Description
        {
            get { return _description; }
            set { SetPropertyValue("Description", ref _description, value); }
        }      
    }
}
