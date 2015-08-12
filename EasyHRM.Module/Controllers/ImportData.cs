using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.XtraEditors;
using EasyHRM.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EasyHRM.Module.Controllers
{
    public partial class ImportData : Form 
    {
        DataTable dt;
        IObjectSpace objSpace;
        Frame frame;
        ErrorProvider er = new ErrorProvider();

       public ImportData(IObjectSpace obj, Frame frm)
       {            
            InitializeComponent();
            dt = new DataTable();
            dt.Columns.Add("IsChecked",typeof(bool));
            objSpace = obj;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lkupTenBangChamCong.Visible = false;
            frame = frm;

            LoadDataLookup();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            er.Clear();
            if(string.IsNullOrEmpty(lkupTenBangChamCong.Text))
            {
                er.SetError(lkupTenBangChamCong, "Bạn chưa nhập tên bảng chấm công!");
                return;
            }
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Bạn chưa chọn dòng để import!", "THÔNG BÁO");
                return;
            }

            if (dtNgayBatDau.EditValue == System.DBNull.Value)
            {
                er.SetError(dtNgayBatDau, "Bạn chưa chọn ngày bắt đầu!");
                return;
            }

            if (dtNgayKetThuc.EditValue == System.DBNull.Value)
            {
                er.SetError(dtNgayKetThuc, "Bạn chưa chọn ngày kết thúc!");
                return;
            }
           
            Department _phongBan = null;
            Employee _nhanVien = null;
            Timekeeping _chamCong = null;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["IsChecked"] == System.DBNull.Value || Convert.ToBoolean(dt.Rows[i]["IsChecked"]) == false)
                        continue;
                    if (dt.Rows[i]["Mã NV"] == System.DBNull.Value)
                        continue;

                    #region Kiểm tra xem phòng ban có trong cở sở dữ liệu chưa, nếu chưa có tạo mới
                    if (dt.Rows[i]["Phòng ban"] != System.DBNull.Value)
                    {
                        _phongBan = objSpace.FindObject<Department>(new BinaryOperator("DepartmentName", dt.Rows[i]["Phòng ban"]));

                        if (_phongBan == null)
                        {
                            _phongBan = objSpace.CreateObject<Department>();
                            _phongBan.DepartmentName = dt.Rows[i]["Phòng ban"].ToString();
                            _phongBan.Save();
                        }
                    }
                    #endregion

                    #region Kiểm tra xem nhân viên có trong cở sở dữ liệu chưa, nếu chưa có tạo mới
                    // Them Nhan Vien
                    if (dt.Rows[i]["Mã NV"] != System.DBNull.Value)
                    {
                        _nhanVien = objSpace.FindObject<Employee>(new BinaryOperator("EmployeeCode", dt.Rows[i]["Mã NV"]));

                        if (_nhanVien == null)
                        {
                            _nhanVien = objSpace.CreateObject<Employee>();
                            _nhanVien.EmployeeCode = dt.Rows[i]["Mã NV"].ToString();
                            string[] strArr = GetNames(dt.Rows[i]["Tên nhân viên"].ToString());
                            if (strArr.Length == 2)
                            {
                                _nhanVien.FirstName = strArr[1];
                                _nhanVien.LastName = strArr[0];
                            }
                            else
                            {
                                _nhanVien.FirstName = strArr[0];
                            }
                            _nhanVien.Department = _phongBan;
                            _nhanVien.Save();
                        }
                    }
                    else
                    {
                        continue;
                    }
                    #endregion

                    #region Thêm một chấm công
                    DateTime _date = new DateTime(0001, 1, 1);
                    if (dt.Rows[i]["Ngày"] != System.DBNull.Value)
                    {
                        _date = DateTime.Parse(dt.Rows[i]["Ngày"].ToString());
                    }
                    // Them Cham Cong   
                    _chamCong = objSpace.FindObject<Timekeeping>(CriteriaOperator.And(new BinaryOperator("Employee", _nhanVien),
                        new BinaryOperator("Date", _date)));
                    if (_chamCong == null)
                    {
                        _chamCong = objSpace.CreateObject<Timekeeping>();
                    }
                    _chamCong.Employee = _nhanVien;
                    _chamCong.Date = _date;
                    _chamCong.ThoiGianVao = TimeSpan.Parse(dt.Rows[i]["ThoiGianVao"].ToString());
                    _chamCong.ThoiGianRa = TimeSpan.Parse(dt.Rows[i]["ThoiGianRa"].ToString());
                    _chamCong.TimekeepingName = (TimekeepingName)lkupTenBangChamCong.EditValue;
                    _chamCong = CalProperties(_chamCong);
                    _chamCong.Save();
                    #endregion

                    objSpace.CommitChanges();
                }
            }

           
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDuongDanFile.Text))
                return;
            lkupTenBangChamCong.Visible = true;
            layoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            
            //Convert To Table Template
            DataTable dt_temp = dt_temp = gridControl1.DataSource as DataTable;
            if (dt_temp == null)
            {
                return;
            }


            // Bo tieu de
            if (dt_temp.Rows.Count > 0)
            {
                dt_temp.DefaultView.RowFilter = "F3 <> ''";
                dt_temp = dt_temp.DefaultView.ToTable();
            }
            //Fomart cấu trúc bảng
            List<string> dsCol = new List<string>(){"Phòng ban","Mã NV","Tên nhân viên", "Ngày", "Vào 1", "Vào 2", "Vào 3",
            "Ra 1", "Ra 2", "Ra 3"};

            foreach (DataRow itemrow in dt_temp.Rows)
            {
                foreach (string col in dsCol)
                {
                    for (int i = 0; i < dt_temp.Columns.Count; i++)
                    {
                        if (itemrow[i].ToString() == col)
                        {
                            dt_temp.Columns[i].ColumnName = col;
                            break;
                        }
                    }
                }
            }            
            
            //Xóa dòng đầu tiền trong bảng
            dt_temp.Rows.RemoveAt(0);

            //Đưa về bảng 8 cột

            for (int i = 0; i < dt_temp.Columns.Count; i++)
            {
                DataColumn columns = dt_temp.Columns[i];
                foreach (string col in dsCol)
                {
                    if (col == columns.ColumnName && !dt.Columns.Contains(col))
                    {
                        dt.Columns.Add(col);
                        break;
                    }
                }
            } 

            //Them 2 cot thoi gian vao thoi gian ra
            DataColumn column = dt.Columns.Add("ThoiGianVao", typeof(string));
            column.Caption = "Thời Gian Vào";
            column = dt.Columns.Add("ThoiGianRa", typeof(string));
            column.Caption = "Thời Gian Ra";
            for (int i = 0; i < dt_temp.Rows.Count; i++)
            {
                DataRow dr = dt_temp.Rows[i];
                dt.ImportRow(dr);

                if (dt_temp.Rows[i]["Vào 1"] != System.DBNull.Value)
                {
                    dt.Rows[i]["ThoiGianVao"] = dt_temp.Rows[i]["Vào 1"];
                }
                else
                {
                    dt.Rows[i]["ThoiGianVao"] = "00:00";
                }


                if (dt_temp.Rows[i]["Ra 3"] != System.DBNull.Value)
                {
                    dt.Rows[i]["ThoiGianRa"] = dt_temp.Rows[i]["Ra 3"];
                }
                else if (dt_temp.Rows[i]["Ra 2"] != System.DBNull.Value)
                {
                    dt.Rows[i]["ThoiGianRa"] = dt_temp.Rows[i]["Ra 2"];
                }
                else if (dt_temp.Rows[i]["Ra 1"] != System.DBNull.Value)
                {
                    dt.Rows[i]["ThoiGianRa"] = dt_temp.Rows[i]["Ra 1"];
                }
                else
                {
                    dt.Rows[i]["ThoiGianRa"] = "00:00";
                }
            }

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            gridControl1.DataSource = dt;

            //Enable button Finish
            btnFinish.Enabled = true;
            btnNext.Enabled = false;
            gridView1.SelectAll();
        }

        private void btnOnpen_Click(object sender, EventArgs e)
        {
            //Open file
            #region Open File
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.Title = "Select a File Excel";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string urlFile = openFileDialog1.FileName;
                txtDuongDanFile.Text = urlFile;
                Cursor = Cursors.WaitCursor;
                //To do: Load data to gridview
                DataTable temp = new DataTable();
                string cmdText = "select * from [Sheet1$]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmdText, GetConnectionString(urlFile)))
                {
                    adapter.Fill(temp);
                }                

                gridControl1.DataSource = temp;

                Cursor = Cursors.Default;
            }
            #endregion

            //Disable button Finish
            btnFinish.Enabled = false;
            btnNext.Enabled = true;
        }

        private string GetConnectionString(string excelFileName)
        {
            string strConnectionString = "";
            if (Path.GetExtension(excelFileName).ToLower() == ".xlsx")
                strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelFileName);
            else if (Path.GetExtension(excelFileName).ToLower() == ".xls")
                strConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", excelFileName);
            return strConnectionString;
        }

       private string[] GetNames(string str)
       {
           string[] strtemp = str.Trim().Split(' ');
           string ho = strtemp[0];
           if(strtemp.Length == 1)
           {
               return strtemp;
           }
           for(int i = 1; i < strtemp.Length - 1; i++)
           {
               ho += " " + strtemp[i];
           }
           string ten = strtemp[strtemp.Length - 1];
           return new string[] { ho, ten };
        }


        //
        // Set value to properties IsChecked in dt
        //
       private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
       {
           for (int j = 0; j < gridView1.RowCount; j++)
           {
               dt.Rows[j]["IsChecked"] = false;

           }
           
           if (gridView1.SelectedRowsCount > 0)
           {
               for (int i = 0; i < gridView1.SelectedRowsCount; i++)
               {
                   dt.Rows[gridView1.GetSelectedRows()[i]]["IsChecked"] = true;
               }
           }
       }


        //
        // Xử lý các dữ liệu của 1 chấm công
        //
       private Timekeeping CalProperties(Timekeeping t)
       {              
           XPQuery<DefaultValueTimekeeping> _DefaultValueTimekeeping = new XPQuery<DefaultValueTimekeeping>((
                   (XPObjectSpace)objSpace).Session);
           Parameter parameter;
           List<DefaultValueTimekeeping> xpcDefaultValueTimekeeping;
           xpcDefaultValueTimekeeping = (from tso in _DefaultValueTimekeeping select tso).ToList();

           using (IObjectSpace objNewSpace = frame.Application.CreateObjectSpace())
           {
               XPCollection xpcPar = new XPCollection(((XPObjectSpace)objNewSpace).Session, typeof(Parameter));
               parameter = xpcPar[0] as Parameter;
           }
           if (t.ThoiGianVao != null && t.ThoiGianRa != null)
           {
               TimeSpan tsValue = (t.ThoiGianRa - t.ThoiGianVao);
               double _SoGioLamCuaCa = 0;
               #region Dự đoán ca làm việc
               foreach (DefaultValueTimekeeping value in xpcDefaultValueTimekeeping)
               {
                   if (value.ThoiGianVao != null && value.ThoiGianRa != null)
                   {
                       TimeSpan ts = t.ThoiGianVao - value.ThoiGianVao;
                       if (ts.Hours == 0 && ts.Minutes > -30 && ts.Minutes < 30)
                       {
                           t.Shift = value.Shift;
                           #region Tính thời gian đi trể về sớm
                           if (ts.TotalMinutes > 0)
                           {
                               t.SoPhutDiTre = Math.Round(ts.TotalMinutes,2);
                           }
                           ts = t.ThoiGianRa - value.ThoiGianRa;
                           if (ts.TotalMinutes < 0)
                           {
                               t.SoPhutVeSom = Math.Round(-ts.TotalMinutes, 2);
                           }

                           _SoGioLamCuaCa = value.TongSoGioLam;
                           break;
                       }
                   }
               }
                           #endregion

               #region Tổng số giờ làm
               t.TongSoGioLam = Math.Round(tsValue.TotalHours,2);
               #endregion

               //
               // Ngày công thực tế nếu có làm thì gán bằng 1
               //

               #region Ngày công thực tế

               t.NgayCongThucTe = 1;
               #endregion


               // 
               // Ngày tính công nếu làm mà không phải tăng ca thì tính bằng 1, ngược lại bằng 0. Mặc định bằng 1
               //
               #region Ngày tính công
               if (t.NgayTinhCong != 0)
               {
                   t.NgayTinhCong = 1;
               }
               #endregion

               //
               // Kiểm tra ngày chấm công có phải ngày lễ không
               //
               t.SoGioTangCaNL = 0;
               t.SoGioTangCaNN = 0;
               t.SoGioTangCaNNCaDem = 0;
               t.SoGioTangCaNT = 0;
               t.SoGioTangCaNTCaDem = 0;

               Holiday holidayDate = objSpace.FindObject<Holiday>(CriteriaOperator.And(new BinaryOperator(
                   "StartDate", t.Date, BinaryOperatorType.GreaterOrEqual), new BinaryOperator(
                       "EndDate",t.Date, BinaryOperatorType.LessOrEqual)));
               if (holidayDate != null)
               {
                   t.SoGioTangCaNL = Math.Round(tsValue.TotalHours,2);

                   t.NgayTinhCong = 0;
               }
               else
               {
                   //
                   // Kiểm tra xem ngày đó có ca không nếu có ca thì là tăng ca ngày thường, không ca tăng ca ngày nghĩ.
                   //
                   if (t.Shift != null)
                   {
                       if (_SoGioLamCuaCa != 0)
                       {
                           double d = tsValue.TotalHours - _SoGioLamCuaCa;
                           if (d >= parameter.SoGioToiThieuTinhTangCa) //  Lớn hơn 15p thì mới tính tăng ca (0.25 giờ)
                           {
                               t.SoGioTangCaNT = Math.Round(d,2);

                               TimeSpan ts2 = t.ThoiGianRa - parameter.ThoiGianTinhCaDem;
                               // Nếu giờ ra sau giờ ra tính ca đêm, thì mới tính ca đêm
                               if (ts2.TotalHours >= parameter.SoGioToiThieuTinhTangCa)
                               {
                                   t.SoGioTangCaNTCaDem = Math.Round(ts2.TotalHours,2);
                                   t.SoGioTangCaNT = Math.Round((tsValue.TotalHours - ts2.TotalHours),2);
                               }
                           }
                       }
                   }
                   else
                   {
                       t.SoGioTangCaNN = Math.Round(tsValue.TotalHours,2);
                       TimeSpan ts2 = t.ThoiGianRa - parameter.ThoiGianTinhCaDem;
                       // Nếu giờ ra sau giờ ra tính ca đêm, thì mới tính ca đêm
                       if (ts2.TotalHours >= parameter.SoGioToiThieuTinhTangCa)
                       {
                           t.SoGioTangCaNNCaDem = Math.Round(ts2.TotalHours,2);
                           t.SoGioTangCaNN = Math.Round((tsValue.TotalHours - ts2.TotalHours),2);
                       }
                   }
               }
               #endregion
           }

           return t;
       }

       private void dtNgayBatDau_EditValueChanged(object sender, EventArgs e)
       {
           if(dtNgayBatDau.EditValue != System.DBNull.Value)
           {
               dtNgayKetThuc.EditValue = dtNgayBatDau.EditValue;
           }
       }

       private void dtNgayKetThuc_EditValueChanged(object sender, EventArgs e)
       {
           if(dtNgayKetThuc.EditValue != System.DBNull.Value)
           {
               TimeSpan ts = dtNgayKetThuc.DateTime - dtNgayBatDau.DateTime;
               if(ts.TotalSeconds < 0)
               {
                   er.Clear();
                   er.SetError(dtNgayKetThuc, "Ngày kết thúc phải lớn hơn ngày bắt đầu!");
                   dtNgayKetThuc.EditValue = dtNgayBatDau.EditValue;
               }
           }
       }

       private void btnNew_Click(object sender, EventArgs e)
       {
           IObjectSpace aObjectSpace = frame.Application.CreateObjectSpace();
           TimekeepingName tkn = aObjectSpace.CreateObject<TimekeepingName>();
           
           ShowViewParameters svp = new ShowViewParameters();
           DetailView dv = frame.Application.CreateDetailView(aObjectSpace, tkn);
           dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
           svp.CreatedView = dv;


           svp.TargetWindow = TargetWindow.NewModalWindow;
           svp.Context = TemplateContext.PopupWindow;
           svp.CreateAllControllers = true;

           var svs = new ShowViewSource(null, null);
           frame.Application.ShowViewStrategy.ShowView(svp, svs);

           LoadDataLookup();

       }

      private void LoadDataLookup()
       {
           XPQuery<TimekeepingName> _TimekeepingName = new XPQuery<TimekeepingName>((
                   (XPObjectSpace)objSpace).Session);

           List<TimekeepingName> listTimekeepingName = (from tso in _TimekeepingName select tso).ToList();
           lkupTenBangChamCong.Properties.DataSource = listTimekeepingName;
           lkupTenBangChamCong.Properties.DisplayMember = "timekeepingName";
       }

      private void lkupTenBangChamCong_EditValueChanged(object sender, EventArgs e)
      {
          if(lkupTenBangChamCong.EditValue != System.DBNull.Value)
          {
              TimekeepingName tkn = (TimekeepingName)lkupTenBangChamCong.EditValue;
              dtNgayBatDau.EditValue = tkn.StartDate;
              dtNgayKetThuc.EditValue = tkn.EndDate;
          }
      }

      
    }
}
