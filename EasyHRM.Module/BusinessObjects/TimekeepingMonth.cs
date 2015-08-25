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
    public class TimekeepingMonth : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public TimekeepingMonth(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        [XafDisplayName("Bảng Chấm Công")]
        public TimekeepingName TimekeepingName
        {
            get { return GetPropertyValue<TimekeepingName>("TimekeepingName"); }
            set { SetPropertyValue<TimekeepingName>("TimekeepingName", value); }
        }

        [XafDisplayName("Nhân Viên")]
        public Employee Employee
        {
            get { return GetPropertyValue<Employee>("Employee"); }
            set { SetPropertyValue<Employee>("Employee", value); }
        }

        [XafDisplayName("Phòng Ban")]
        public Department Department
        {
            get { return GetPropertyValue<Department>("Department"); }
            set { SetPropertyValue<Department>("Department", value); }
        }


        private double _sum;
        [XafDisplayName("Tổng")]
        public double Sum
        {
            get { return _sum; }
            set { SetPropertyValue("Sum", ref _sum, value); }
        }
        
        [XafDisplayName("Loại Dữ Liệu Chấm Công")]
        public DataTypeTimekeeping DataTypeTimekeeping
        {
            get { return GetPropertyValue<DataTypeTimekeeping>("DataTypeTimekeeping"); }
            set { SetPropertyValue<DataTypeTimekeeping>("DataTypeTimekeeping", value); }
        }

        #region Ngày
        private string _ngay1;
        [XafDisplayName("Ngày 1")]
        public string Ngay1
        {
            get { return _ngay1; }
            set { SetPropertyValue("Ngay1", ref _ngay1, value); }
        }

        private string _ngay2;
        [XafDisplayName("Ngày 2")]
        public string Ngay2
        {
            get { return _ngay2; }
            set { SetPropertyValue("Ngay2", ref _ngay2, value); }
        }

        private string _ngay3;
        [XafDisplayName("Ngày 3")]
        public string Ngay3
        {
            get { return _ngay3; }
            set { SetPropertyValue("Ngay3", ref _ngay3, value); }
        }

        private string _ngay4;
        [XafDisplayName("Ngày 4")]
        public string Ngay4
        {
            get { return _ngay4; }
            set { SetPropertyValue("Ngay4", ref _ngay4, value); }
        }

        private string _ngay5;
        [XafDisplayName("Ngày 5")]
        public string Ngay5
        {
            get { return _ngay5; }
            set { SetPropertyValue("Ngay5", ref _ngay5, value); }
        }

        private string _ngay6;
        [XafDisplayName("Ngày 6")]
        public string Ngay6
        {
            get { return _ngay6; }
            set { SetPropertyValue("Ngay6", ref _ngay6, value); }
        }

        private string _ngay7;
        [XafDisplayName("Ngày 7")]
        public string Ngay7
        {
            get { return _ngay7; }
            set { SetPropertyValue("Ngay7", ref _ngay7, value); }
        }

        private string _ngay8;
        [XafDisplayName("Ngày 8")]
        public string Ngay8
        {
            get { return _ngay8; }
            set { SetPropertyValue("Ngay8", ref _ngay8, value); }
        }

        private string _ngay9;
        [XafDisplayName("Ngày 9")]
        public string Ngay9
        {
            get { return _ngay9; }
            set { SetPropertyValue("Ngay9", ref _ngay9, value); }
        }

        private string _ngay10;
        [XafDisplayName("Ngày 10")]
        public string Ngay10
        {
            get { return _ngay10; }
            set { SetPropertyValue("Ngay10", ref _ngay10, value); }
        }

        private string _ngay11;
        [XafDisplayName("Ngày 11")]
        public string Ngay11
        {
            get { return _ngay11; }
            set { SetPropertyValue("Ngay11", ref _ngay11, value); }
        }

        private string _ngay12;
        [XafDisplayName("Ngày 12")]
        public string Ngay12
        {
            get { return _ngay12; }
            set { SetPropertyValue("Ngay12", ref _ngay12, value); }
        }

        private string _ngay13;
        [XafDisplayName("Ngày 13")]
        public string Ngay13
        {
            get { return _ngay13; }
            set { SetPropertyValue("Ngay13", ref _ngay13, value); }
        }

        private string _ngay14;
        [XafDisplayName("Ngày 14")]
        public string Ngay14
        {
            get { return _ngay14; }
            set { SetPropertyValue("Ngay14", ref _ngay14, value); }
        }

        private string _ngay15;
        [XafDisplayName("Ngày 15")]
        public string Ngay15
        {
            get { return _ngay15; }
            set { SetPropertyValue("Ngay15", ref _ngay15, value); }
        }
        private string _ngay16;
        [XafDisplayName("Ngày 16")]
        public string Ngay16
        {
            get { return _ngay16; }
            set { SetPropertyValue("Ngay16", ref _ngay16, value); }
        }

        private string _ngay17;
        [XafDisplayName("Ngày 17")]
        public string Ngay17
        {
            get { return _ngay17; }
            set { SetPropertyValue("Ngay17", ref _ngay17, value); }
        }

        private string _ngay18;
        [XafDisplayName("Ngày 18")]
        public string Ngay18
        {
            get { return _ngay18; }
            set { SetPropertyValue("Ngay18", ref _ngay18, value); }
        }

        private string _ngay19;
        [XafDisplayName("Ngày 19")]
        public string Ngay19
        {
            get { return _ngay19; }
            set { SetPropertyValue("Ngay19", ref _ngay19, value); }
        }

        private string _ngay20;
        [XafDisplayName("Ngày 20")]
        public string Ngay20
        {
            get { return _ngay20; }
            set { SetPropertyValue("Ngay20", ref _ngay20, value); }
        }

        private string _ngay21;
        [XafDisplayName("Ngày 21")]
        public string Ngay21
        {
            get { return _ngay21; }
            set { SetPropertyValue("Ngay21", ref _ngay21, value); }
        }

        private string _ngay22;
        [XafDisplayName("Ngày 22")]
        public string Ngay22
        {
            get { return _ngay22; }
            set { SetPropertyValue("Ngay1", ref _ngay22, value); }
        }

        private string _ngay23;
        [XafDisplayName("Ngày 23")]
        public string Ngay23
        {
            get { return _ngay23; }
            set { SetPropertyValue("Ngay23", ref _ngay23, value); }
        }

        private string _ngay24;
        [XafDisplayName("Ngày 24")]
        public string Ngay24
        {
            get { return _ngay24; }
            set { SetPropertyValue("Ngay24", ref _ngay24, value); }
        }

        private string _ngay25;
        [XafDisplayName("Ngày 25")]
        public string Ngay25
        {
            get { return _ngay25; }
            set { SetPropertyValue("Ngay25", ref _ngay25, value); }
        }

        private string _ngay26;
        [XafDisplayName("Ngày 26")]
        public string Ngay26
        {
            get { return _ngay26; }
            set { SetPropertyValue("Ngay26", ref _ngay26, value); }
        }

        private string _ngay27;
        [XafDisplayName("Ngày 27")]
        public string Ngay27
        {
            get { return _ngay27; }
            set { SetPropertyValue("Ngay27", ref _ngay27, value); }
        }

        private string _ngay28;
        [XafDisplayName("Ngày 28")]
        public string Ngay28
        {
            get { return _ngay28; }
            set { SetPropertyValue("Ngay28", ref _ngay28, value); }
        }

        private string _ngay29;
        [XafDisplayName("Ngày 29")]
        public string Ngay29
        {
            get { return _ngay29; }
            set { SetPropertyValue("Ngay29", ref _ngay29, value); }
        }

        private string _ngay30;
        [XafDisplayName("Ngày 30")]
        public string Ngay30
        {
            get { return _ngay30; }
            set { SetPropertyValue("Ngay30", ref _ngay30, value); }
        }

        private string _ngay31;
        [XafDisplayName("Ngày 31")]
        public string Ngay31
        {
            get { return _ngay31; }
            set { SetPropertyValue("Ngay31", ref _ngay31, value); }
        }
        #endregion

        public string this[int index]
        {

            get
            {
                switch (index)
                {
                    case 1:
                        return Ngay1;
                    case 2:
                        return Ngay2;
                    case 3:
                        return Ngay3;
                    case 4:
                        return Ngay4;
                    case 5:
                        return Ngay5;
                    case 6:
                        return Ngay6;
                    case 7:
                        return Ngay7;
                    case 8:
                        return Ngay8;
                    case 9:
                        return Ngay9;
                    case 10:
                        return Ngay10;
                    case 11:
                        return Ngay11;
                    case 12:
                        return Ngay12;
                    case 13:
                        return Ngay13;
                    case 14:
                        return Ngay14;
                    case 15:
                        return Ngay15;
                    case 16:
                        return Ngay16;
                    case 17:
                        return Ngay17;
                    case 18:
                        return Ngay18;
                    case 19:
                        return Ngay19;
                    case 20:
                        return Ngay10;
                    case 21:
                        return Ngay21;
                    case 22:
                        return Ngay22;
                    case 23:
                        return Ngay23;
                    case 24:
                        return Ngay24;
                    case 25:
                        return Ngay25;
                    case 26:
                        return Ngay26;
                    case 27:
                        return Ngay27;
                    case 28:
                        return Ngay28;
                    case 29:
                        return Ngay29;
                    case 30:
                        return Ngay30;
                    case 31:
                        return Ngay31;
                    default:
                        return string.Empty;
                }
            }
            set
            {
                switch (index)
                {
                    case 1:
                        Ngay1 = value;
                        break;
                    case 2:
                        Ngay2 = value;
                        break;
                    case 3:
                        Ngay3 = value;
                        break;
                    case 4:
                        Ngay4 = value;
                        break;
                    case 5:
                        Ngay5 = value;
                        break;
                    case 6:
                        Ngay6 = value;
                        break;
                    case 7:
                        Ngay7 = value;
                        break;
                    case 8:
                        Ngay8 = value;
                        break;
                    case 9:
                        Ngay9 = value;
                        break;
                    case 10:
                        Ngay10 = value;
                        break;
                    case 11:
                        Ngay11 = value;
                        break;
                    case 12:
                        Ngay12 = value;
                        break;
                    case 13:
                        Ngay13 = value;
                        break;
                    case 14:
                        Ngay14 = value;
                        break;
                    case 15:
                        Ngay15 = value;
                        break;
                    case 16:
                        Ngay16 = value;
                        break;
                    case 17:
                        Ngay17 = value;
                        break;
                    case 18:
                        Ngay18 = value;
                        break;
                    case 19:
                        Ngay19 = value;
                        break;
                    case 20:
                        Ngay20 = value;
                        break;
                    case 21:
                        Ngay21 = value;
                        break;
                    case 22:
                        Ngay22 = value;
                        break;
                    case 23:
                        Ngay23 = value;
                        break;
                    case 24:
                        Ngay24 = value;
                        break;
                    case 25:
                        Ngay25 = value;
                        break;
                    case 26:
                        Ngay26 = value;
                        break;
                    case 27:
                        Ngay27 = value;
                        break;
                    case 28:
                        Ngay28 = value;
                        break;
                    case 29:
                        Ngay29 = value;
                        break;
                    case 30:
                        Ngay30 = value;
                        break;
                    case 31:
                        Ngay31 = value;
                        break;
                    default:
                        break;
                }
            }
        }        

    }
}
