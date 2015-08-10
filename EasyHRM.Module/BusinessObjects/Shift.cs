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

    public class Shift : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Shift(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        //private string _shiftCode;
        private string _shiftName;
        private string _description;

        //[XafDisplayName("Mã Ca Làm Việc")]
        //[DbType("NOT")]
        //public string ShiftCode
        //{
        //    get { return _shiftCode; }
        //    set { SetPropertyValue("ShiftCode", ref _shiftCode, value); }
        //}

        [XafDisplayName("Tên Ca Làm Việc")]
        public string ShiftName
        {
            get { return _shiftName; }
            set { SetPropertyValue("ShiftName", ref _shiftName, value); }
        }     


        [XafDisplayName("Mô Tả")]
        public string Description
        {
            get { return _description; }
            set { SetPropertyValue("Description", ref _description, value); }
        }     
       
    }
}
