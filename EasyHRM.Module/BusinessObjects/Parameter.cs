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
    public class Parameter : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Parameter(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        private double _SoGioToiThieuTinhTangCa;
        [XafDisplayName("Số Giờ Tối Thiểu Tính Tăng Ca")]
        public double SoGioToiThieuTinhTangCa
        {
            get { return _SoGioToiThieuTinhTangCa; }
            set { SetPropertyValue("SoGioToiThieuTinhTangCa", ref _SoGioToiThieuTinhTangCa, value); }
        }

     
        private TimeSpan _ThoiGianTinhCaDem;
        [XafDisplayName("Thời Gian Bắt Đầu Tính Ca Đêm")]
        public TimeSpan ThoiGianTinhCaDem
        {
            get { return _ThoiGianTinhCaDem; }
            set { SetPropertyValue("ThoiGianTinhCaDem", ref _ThoiGianTinhCaDem, value); }
        }
      
    }
}
