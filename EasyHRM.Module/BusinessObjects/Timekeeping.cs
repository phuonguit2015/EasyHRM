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
    [NavigationItem ("Timekeeping Manager")]
    public class Timekeeping : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Timekeeping(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }


        private string _timekeepingName;
        [XafDisplayName ("Tên Bảng Chấm Công")]
        public string Name
        {
            get { return _timekeepingName; }
            set { SetPropertyValue("Name", ref _timekeepingName, value); }
        }

        [XafDisplayName("Bảng Chấm Công")]
        public TimekeepingName TimekeepingName
        {
            get { return GetPropertyValue<TimekeepingName>("TimekeepingName"); }
            set { SetPropertyValue<TimekeepingName>("TimekeepingName", value); }
        }

        [XafDisplayName ("Nhân Viên")]
        public Employee Employee
        {
            get { return GetPropertyValue<Employee>("Employee"); }
            set { SetPropertyValue<Employee>("Employee", value); }
        }

        [XafDisplayName("Ca Làm Việc")]
        public Shift Shift
        {
            get { return GetPropertyValue<Shift>("Shift"); }
            set { SetPropertyValue<Shift>("Shift", value); }
        }

      
        private DateTime _date;
        [XafDisplayName("Ngày")]
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime Date
        {
            get { return _date; }
            set { SetPropertyValue("Date", ref _date, value); }
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

        private double _soPhutDiTre;
        [XafDisplayName("Số Phút Đi Trể")]
        public double SoPhutDiTre
        {
            get { return _soPhutDiTre; }
            set { SetPropertyValue("SoPhutDiTre", ref _soPhutDiTre, value); }
        }
        
        private double _soPhutVeSom;
        [XafDisplayName("Số Phút Về Sớm")]
        public double SoPhutVeSom
        {
            get { return _soPhutVeSom; }
            set { SetPropertyValue("SoPhutVeSom", ref _soPhutVeSom, value); }
        }

        private double _tongSoGioLam;
        [XafDisplayName("Tổng Số Giờ Làm")]
        public double TongSoGioLam
        {
            get { return _tongSoGioLam; }
            set { SetPropertyValue("TongSoGioLam", ref _tongSoGioLam, value); }
        }

        private int _ngayCongThucTe;
        [XafDisplayName("Ngày Công Thực Tế")]
        public int NgayCongThucTe
        {
            get { return _ngayCongThucTe; }
            set { SetPropertyValue("NgayCongThucTe", ref _ngayCongThucTe, value); }
        }
        
        private int _ngayTinhCong;
        [XafDisplayName("Ngày Tính Công")]
        public int NgayTinhCong
        {
            get { return _ngayTinhCong; }
            set { SetPropertyValue("NgayTinhCong", ref _ngayTinhCong, value); }
        }

        private double _soGioTangCaNT;
        [XafDisplayName("Số Giờ Tăng Ca Ngày Thường")]
        public double SoGioTangCaNT
        {
            get { return _soGioTangCaNT; }
            set { SetPropertyValue("SoGioTangCaNT", ref _soGioTangCaNT, value); }
        }

        private double _soGioTangCaNTCaDem;
        [XafDisplayName("Số Giờ Tăng Ca Ngày Thường Ca Đêm")]
        public double SoGioTangCaNTCaDem
        {
            get { return _soGioTangCaNTCaDem; }
            set { SetPropertyValue("SoGioTangCaNTCaDem", ref _soGioTangCaNTCaDem, value); }
        }

        private double _soGioTangCaNN;
        [XafDisplayName("Số Giờ Tăng Ca Ngày Nghĩ")]
        public double SoGioTangCaNN
        {
            get { return _soGioTangCaNN; }
            set { SetPropertyValue("SoGioTangCaNN", ref _soGioTangCaNN, value); }
        }

        private double _soGioTangCaNNCaDem;
        [XafDisplayName("Số Giờ Tăng Ca Ngày Nghĩ Ca Đêm")]
        public double SoGioTangCaNNCaDem
        {
            get { return _soGioTangCaNNCaDem; }
            set { SetPropertyValue("SoGioTangCaNNCaDem", ref _soGioTangCaNNCaDem, value); }
        }

        private double _soGioTangCaNL;
        [XafDisplayName("Số Giờ Tăng Ca Ngày Lễ")]
        public double SoGioTangCaNL
        {
            get { return _soGioTangCaNL; }
            set { SetPropertyValue("SoGioTangCaNL", ref _soGioTangCaNL, value); }
        }
    }
}
