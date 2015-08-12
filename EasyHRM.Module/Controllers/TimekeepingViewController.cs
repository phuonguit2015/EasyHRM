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
using DevExpress.Xpo.DB;

namespace EasyHRM.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class TimekeepingViewController : ViewController
    {
        ChoiceActionItem setLookupItem;
        TimekeepingName tkname = null;

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
            View.ControlsCreated += new EventHandler(View_ControlsCreated);

            XPQuery<TimekeepingName> _TimekeepingName = new XPQuery<TimekeepingName>((
                   (XPObjectSpace)ObjectSpace).Session);

            List<TimekeepingName> listTimekeepingName = (from tso in _TimekeepingName select tso).ToList();
            acFilterByTimekeepingMonth.Items.Clear();

            foreach (TimekeepingName item in listTimekeepingName)
            {
                if (item.StartDate <= DateTime.Now && DateTime.Now <= item.EndDate)
                {
                    tkname = item;
                }
                setLookupItem = new ChoiceActionItem(item.timekeepingName, item);

                acFilterByTimekeepingMonth.Items.Add(setLookupItem);
            }
            if (listTimekeepingName != null)
            {
                if (tkname == null)
                {
                    tkname = listTimekeepingName[0];
                }
                ((DevExpress.ExpressApp.ListView)View).CollectionSource.Criteria["Filter1"] = new BinaryOperator(
                        "TimekeepingName.Oid", tkname.Oid, BinaryOperatorType.Equal);
            }          
          
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            GridListEditor editor1 = (GridListEditor)((DevExpress.ExpressApp.ListView)View).Editor;
            editor1.GridView.OptionsView.ColumnAutoWidth = false;
            foreach(XafGridColumn col in editor1.GridView.Columns)
            {
                col.Width = 80;
            }
            editor1.GridView.ColumnPanelRowHeight = 30;
            editor1.GridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
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

        private void CreateNewRowTimekeeping(Session session, string value,
            Guid loaiDuLieuChamCongOid, Guid tenBangChamCongOid, Guid nhanVienOid, int index, ref List<string> strArr)
        {
            string s = string.Format("SELECT * FROM \"TimekeepingMonth\" WHERE" +
                    "\"TimekeepingName\" = '{0}' AND \"Employee\" = '{1}' AND \"DataTypeTimekeeping\" = '{2}'",
                tenBangChamCongOid, nhanVienOid, loaiDuLieuChamCongOid);
            SelectedData data = session.ExecuteQuery(s);
            //if(session.FindObject<TimekeepingMonth>(CriteriaOperator.And(new BinaryOperator("TimekeepingName.Oid", tenBangChamCongOid),
            //    new BinaryOperator("Employee.Oid",nhanVienOid), new BinaryOperator("DataTypeTimekeeping.Oid",loaiDuLieuChamCongOid))) == null)
            if(data.ResultSet[0].Rows.Length == 0)
            {
                session.BeginTransaction();
                Guid id = Guid.NewGuid();
                while (true)
                {
                    try
                    {

                        s = string.Format("INSERT INTO \"TimekeepingMonth\" (\"Oid\",\"TimekeepingName\", \"Employee\", \"DataTypeTimekeeping\", \"Ngay" + index.ToString() + "\")" +
                        "VALUES ('{4}','{0}','{1}','{2}','{3}')", tenBangChamCongOid, nhanVienOid, loaiDuLieuChamCongOid, value, id);
                        session.ExecuteNonQuery(s);
                        break;
                    }
                    catch
                    {
                        id = Guid.NewGuid();
                    }
                }
                session.CommitTransaction();
            }
            else
            {
                strArr.Add(string.Format("UPDATE \"TimekeepingMonth\" SET \"Ngay" + index.ToString() + "\" = '{3}', \"OptimisticLockField\" = null, \"GCRecord\" = null WHERE" +
                    "\"TimekeepingName\" = '{0}' AND \"Employee\" = '{1}' AND \"DataTypeTimekeeping\" = '{2}'",
                tenBangChamCongOid, nhanVienOid, loaiDuLieuChamCongOid, value));                
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

        
            //((XPObjectSpace)ObjectSpace).Session.ExecuteQuery()
            // 
            //XPQuery<Timekeeping> _Timekeeping = new XPQuery<Timekeeping>((
            //       (XPObjectSpace)ObjectSpace).Session);
            //IEnumerable<Timekeeping> timekeeping = (from tso in _Timekeeping select tso).ToList();
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
            Session session = ((XPObjectSpace)ObjectSpace).Session;
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
           
            List<string> strArr = new List<string>();
            
            foreach (Timekeeping note in ((DevExpress.ExpressApp.ListView)View).CollectionSource.List)
            {

                tempNote = note;
                
                j++;

                // Lấy ngày bắt đầu của tháng
                TimekeepingName tn = session.FindObject<TimekeepingName>(new BinaryOperator("timekeepingName", tempNote.TimekeepingName.timekeepingName));
                TimeSpan ts = tempNote.Date - tn.StartDate;
                int index = ts.Days + 1;
                
                CreateNewRowTimekeeping(session, tempNote.ThoiGianVao.ToString("HH:mm"), thoiGianVao.Oid, tempNote.TimekeepingName.Oid,
                    tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.ThoiGianRa.ToString("HH:mm"), thoiGianRa.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoGioTangCaNL.ToString(), soGioTangCaNL.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoGioTangCaNN.ToString(), soGioTangCaNN.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoGioTangCaNNCaDem.ToString(), soGioTangCaNNCaDem.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoGioTangCaNT.ToString(), soGioTangCaNT.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoGioTangCaNTCaDem.ToString(), soGioTangCaNTCaDem.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoPhutDiTre.ToString(), soPhutDiTre.Oid, tempNote.TimekeepingName.Oid,
                   tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.SoPhutVeSom.ToString(), soPhutVeSom.Oid, tempNote.TimekeepingName.Oid,
                  tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.TongSoGioLam.ToString(), tongThoiGianLam.Oid, tempNote.TimekeepingName.Oid,
                  tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.NgayCongThucTe.ToString(), ngayCongThucTe.Oid, tempNote.TimekeepingName.Oid,
                  tempNote.Employee.Oid, index, ref strArr);
                CreateNewRowTimekeeping(session, tempNote.NgayTinhCong.ToString(), ngayTinhCong.Oid, tempNote.TimekeepingName.Oid,
                  tempNote.Employee.Oid, index, ref strArr);

                
            }
            session.BeginTransaction();
            foreach(string str in strArr)
            {
                session.ExecuteNonQuery(str);
            }
            session.CommitTransaction();
        }

        private void acFilterByTimekeepingMonth_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            tkname = e.SelectedChoiceActionItem.Data as TimekeepingName;
            ((DevExpress.ExpressApp.ListView)View).CollectionSource.Criteria["Filter1"] = new BinaryOperator(
           "TimekeepingName.Oid", tkname.Oid, BinaryOperatorType.Equal);
        }
    }
}
