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
   
    public class DataTypeTimekeeping : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public DataTypeTimekeeping(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        private string _dataType;
        [XafDisplayName("Loại Chấm Công")]
        public string DataType
        {
            get { return _dataType; }
            set { SetPropertyValue("DataType", ref _dataType, value); }
        }

        private KieuDuLieu _type;
        [XafDisplayName("Kiểu Dữ Liệu")]
        public KieuDuLieu TypeData
        {
            get { return _type; }
            set { SetPropertyValue("TypeData", ref _type, value); }
        }
    }

    public enum KieuDuLieu
    {
        Int, String, DateTime, Double, Decimal
    }
}
