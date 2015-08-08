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
    [NavigationItem ("Employee Manager")]
    public class Employee : Person
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Employee(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        [XafDisplayName("Chi Nhánh")]
        public Branch Branch
        {
            get { return GetPropertyValue<Branch>("Branch"); }
            set { SetPropertyValue<Branch>("Branch", value); }
        }

        [XafDisplayName("Phòng Ban")]
        public Department Department
        {
            get { return GetPropertyValue<Department>("Department"); }
            set { SetPropertyValue<Department>("Department", value); }
        }
        
        [XafDisplayName("Chức Vụ")]
        public Position Position
        {
            get { return GetPropertyValue<Position>("Position"); }
            set { SetPropertyValue<Position>("Position", value); }
        }

        private string _employeeCode;
        [XafDisplayName("Mã Nhân Viên")]
        public string EmployeeCode
        {
            get { return _employeeCode; }
            set { SetPropertyValue("EmployeeCode", ref _employeeCode, value); }
        }
    }
}
