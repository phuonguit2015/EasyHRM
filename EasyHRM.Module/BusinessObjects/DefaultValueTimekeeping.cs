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
    public class DefaultValueTimekeeping : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public DefaultValueTimekeeping(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        [XafDisplayName("Ca Làm Việc")]
        public Shift Shift
        {
            get { return GetPropertyValue<Shift>("Shift"); }
            set { SetPropertyValue<Shift>("Shift", value); }
        }

        private TimeSpan _thoiGianVao;
        [XafDisplayName("Thời Gian Vào")]
        public TimeSpan ThoiGianVao
        {
            get { return _thoiGianVao; }
            set { SetPropertyValue("ThoiGianVao", ref _thoiGianVao, value); }
        }

        private TimeSpan _thoiGianRa;
        [XafDisplayName("Thời Gian Ra")]
        public TimeSpan ThoiGianRa
        {
            get { return _thoiGianRa; }
            set { SetPropertyValue("ThoiGianRa", ref _thoiGianRa, value); }
        }
        
        private double _tongSoGioLam;
        [XafDisplayName("Tổng Số Giờ Làm")]
        public double TongSoGioLam
        {
            get { return _tongSoGioLam; }
            set { SetPropertyValue("TongSoGioLam", ref _tongSoGioLam, value); }
        }        

    }   
}
