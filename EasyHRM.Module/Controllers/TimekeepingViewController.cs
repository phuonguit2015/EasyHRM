using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win;
using EasyHRM.Module.BusinessObjects;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.XtraBars;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace EasyHRM.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class TimekeepingViewController : ViewController
    {
        public TimekeepingViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            IBarManagerHolder bmh = Frame.Template as IBarManagerHolder;
            if (bmh == null || bmh.BarManager == null) return;
            foreach (BarItem item in bmh.BarManager.Items)
            {
                BarEditItem editItem = item as BarEditItem;
                if (editItem != null && Convert.ToString(item.Tag).Contains("ID=\"FilterByEmployeeCode\""))
                {
                    item.Width = 250;
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void propertyEditor_ControlCreated(object sender, EventArgs e)
        {
            InitNullText((PropertyEditor)sender);
        }
        private void InitNullText(PropertyEditor propertyEditor)
        {
            ((BaseEdit)propertyEditor.Control).Properties.NullText = CaptionHelper.NullValueText;
        }

        private void ImportFromExcel_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Form f = new ImportData(View.ObjectSpace, Frame);
            f.ShowDialog();
        }

        private void acFilterByEmployeeCode_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            string paramValue = e.ParameterCurrentValue as string;
            if (!string.IsNullOrEmpty(paramValue))
            {
                paramValue = "%" + paramValue + "%";
                if ((View is DevExpress.ExpressApp.ListView) & (View.ObjectTypeInfo.Type == typeof(Timekeeping)))
                {
                    ((DevExpress.ExpressApp.ListView)View).CollectionSource.Criteria["Filter1"] = new BinaryOperator(
                       "Employee.EmployeeCode", paramValue, BinaryOperatorType.Like);
                }
            }
            else
            {
                ((DevExpress.ExpressApp.ListView)View).CollectionSource.Criteria.Clear();
            }
           
        }

        private void ImportToTimekeepingMonth_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            // Khởi tạo giá trị dữ liệu chấm công
            List<string> listLoaiDuLieuChamCong = new List<string>
            {
                "Thời Gian Vào", "Thời Gian Ra", "Số Phút Đi Trể", "Số Phút Về Sớm",
                "Tổng Số Giờ Làm", "Ngày Công Thực Tế", "Ngày Tính Công", "Số Giờ Tăng Ca NT",
                "Số Giờ Tăng Ca NT Ca Đêm", "Số Giờ Tăng Ca NN", "Số Giờ Tăng Ca NN Ca Đêm", "Số Giờ Tăng Ca NL"
            };
            for(int i = 0; i <listLoaiDuLieuChamCong.Count; i++)
            {
                DataTypeTimekeeping dtt = View.ObjectSpace.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", listLoaiDuLieuChamCong[i]));
                if(dtt == null)
                {
                    dtt = new DataTypeTimekeeping(((XPObjectSpace)View.ObjectSpace).Session);
                    dtt.DataType = listLoaiDuLieuChamCong[i];
                    dtt.Save();
                    View.ObjectSpace.CommitChanges();
                }
            }        

        
            
            // 
            XPQuery<Timekeeping> _Timekeeping = new XPQuery<Timekeeping>((
                   (XPObjectSpace)ObjectSpace).Session);
            IEnumerable<Timekeeping> timekeeping = (from tso in _Timekeeping select tso).ToList();
            int j = 0;
            Timekeeping tempNote = null;
            DataTypeTimekeeping thoiGianRa = null;
            DataTypeTimekeeping thoiGianVao = null;
            DataTypeTimekeeping soPhutDiTre = null;
            DataTypeTimekeeping soPhutVeSom = null;
            DataTypeTimekeeping tongThoiGianLam = null;
            DataTypeTimekeeping ngayTinhCong = null;
            DataTypeTimekeeping ngayCongThucTe = null;
            DataTypeTimekeeping soGioTangCaNT = null;
            DataTypeTimekeeping soGioTangCaNTCaDem = null;
            DataTypeTimekeeping soGioTangCaNN = null;
            DataTypeTimekeeping soGioTangCaNNCaDem = null;
            DataTypeTimekeeping soGioTangCaNL = null;
            UnitOfWork session = new UnitOfWork(((XPObjectSpace)ObjectSpace).Session.DataLayer);
            foreach (Timekeeping note in timekeeping)
            {


                if (j % 100 == 0)
                {
                    session.CommitChanges();
                    session.Dispose();
                    session = new UnitOfWork(((XPObjectSpace)ObjectSpace).Session.DataLayer);
                    tempNote = session.FindObject<Timekeeping>(new BinaryOperator("Oid", note.Oid));

                    thoiGianRa = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Thời Gian Ra"));
                    thoiGianVao = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Thời Gian Vào"));
                    soPhutDiTre = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Phút Đi Trể"));
                    soPhutVeSom = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Phút Về Sớm"));
                    tongThoiGianLam = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Tổng Số Giờ Làm"));
                    ngayTinhCong = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Ngày Tính Công"));
                    ngayCongThucTe = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Ngày Công Thực Tế"));
                    soGioTangCaNT = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Giờ Tăng Ca NT"));
                    soGioTangCaNTCaDem = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Giờ Tăng Ca NT Ca Đêm"));
                    soGioTangCaNN = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Giờ Tăng Ca NN"));
                    soGioTangCaNNCaDem = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Giờ Tăng Ca NN Ca Đêm"));
                    soGioTangCaNL = session.FindObject<DataTypeTimekeeping>(new BinaryOperator("DataType", "Số Giờ Tăng Ca NL"));
                }
                j++;

                // Lấy ngày bắt đầu của tháng
                TimekeepingName tn = session.FindObject<TimekeepingName>(new BinaryOperator("timekeepingName", tempNote.TimekeepingName.timekeepingName));
                TimeSpan ts = tempNote.Date - tn.StartDate;
                int index = ts.Days + 1;

                TimekeepingMonth _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                // Thời Gian Vào
                _timekeepingMonth.DataTypeTimekeeping = thoiGianVao;
                _timekeepingMonth[index] = tempNote.ThoiGianVao.ToString();
                _timekeepingMonth.Save();

                // Thời Gian Ra
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = thoiGianRa;
                _timekeepingMonth[index] = tempNote.ThoiGianRa.ToString();
                _timekeepingMonth.Save();


                // Số phút đi trể
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soPhutDiTre;
                _timekeepingMonth[index] = tempNote.SoPhutDiTre.ToString();
                _timekeepingMonth.Save();

                // Số Phút Về Sớm
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soPhutVeSom;
                _timekeepingMonth[index] = tempNote.SoPhutVeSom.ToString();
                _timekeepingMonth.Save();

                // Tổng Số Giờ Làm
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = tongThoiGianLam;
                _timekeepingMonth[index] = tempNote.TongSoGioLam.ToString();
                _timekeepingMonth.Save();


                // Ngày Tính Công
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = ngayTinhCong;
                _timekeepingMonth[index] = tempNote.NgayTinhCong.ToString();
                _timekeepingMonth.Save();


                // Ngày Công Thực Tế
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = ngayCongThucTe;
                _timekeepingMonth[index] = tempNote.NgayCongThucTe.ToString();
                _timekeepingMonth.Save();


                // Số Giờ Tăng Ca NT
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soGioTangCaNT;
                _timekeepingMonth[index] = tempNote.SoGioTangCaNT.ToString();
                _timekeepingMonth.Save();


                // Số Giờ Tăng Ca NT Ca Đêm
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soGioTangCaNTCaDem;
                _timekeepingMonth[index] = tempNote.SoGioTangCaNTCaDem.ToString();
                _timekeepingMonth.Save();

                // Số Giờ Tăng Ca NN
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soGioTangCaNN;
                _timekeepingMonth[index] = tempNote.SoGioTangCaNN.ToString();
                _timekeepingMonth.Save();

                // Số Giờ Tăng Ca NN Ca Đêm
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soGioTangCaNNCaDem;
                _timekeepingMonth[index] = tempNote.SoGioTangCaNNCaDem.ToString();
                _timekeepingMonth.Save();

                // Số Giờ Tăng Ca NL
                _timekeepingMonth = new TimekeepingMonth(session);
                _timekeepingMonth.Employee = tempNote.Employee;
                _timekeepingMonth.TimekeepingName = tempNote.TimekeepingName;
                _timekeepingMonth.Shift = tempNote.Shift;
                _timekeepingMonth.DataTypeTimekeeping = soGioTangCaNL;
                _timekeepingMonth[index] = tempNote.SoGioTangCaNL.ToString();
                _timekeepingMonth.Save();


               // session.CommitChanges();

            }
            session.CommitChanges();
        }
    }
}
