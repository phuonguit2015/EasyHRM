using DevExpress.Spreadsheet;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using EasyHRM.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyHRM.Module.Controllers
{
    public partial class Salaries : Form
    {
        Session session;
        string fileName;
        public Salaries(Session s)
        {
            InitializeComponent();
            session = s;
            fileName = System.IO.Directory.GetCurrentDirectory() + @"\Config Map Collumn\config.txt";
        }

        private void Salaries_Load(object sender, EventArgs e)
        {
            spreadControl.LoadDocument();

            DuaDuLieuLenBangLuong();
            /*
            spreadControl.LoadDocument();

            IWorkbook workbook = spreadControl.Document;
            Worksheet worksheet = workbook.Worksheets[0];

            Worksheet configSheet = workbook.Worksheets[1];
           
            Dictionary<int, object> config = new Dictionary<int, object>();
           



            XPQuery<Employee> _Employee = new XPQuery<Employee>(session);

            List<Employee> listEmployee = (from tso in _Employee select tso).ToList();
            int countEmp = 2;
            foreach (Employee emp in listEmployee)
            {
                int row = 1;
                while (configSheet.Cells[row, 0].Value != "")
                {
                    int col = int.Parse(configSheet.Cells[row, 0].Value.ToString());
                    string table = configSheet.Cells[row, 1].Value.ToString();
                    string field = configSheet.Cells[row, 2].Value.ToString();
                    string condition = configSheet.Cells[row, 3].Value.ToString();
                    string sql = string.Empty;
                    if (condition != "")
                    {
                       sql = string.Format("SELECT \"{0}\" FROM \"{1}\" WHERE {2} AND \"Employee.Oid\" = '{3}'",
                            field, table, condition, emp.Oid);
                    }
                    else
                    {
                        sql = string.Format("SELECT \"{0}\" FROM \"{1}\" WHERE \"Oid\" = '{2}'",
                           field, table, emp.Oid);
                    }
                    SelectedData sdata = session.ExecuteQuery(sql);

                    worksheet[countEmp, col].Value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    row++;
                }
                worksheet[countEmp, 0].Value = countEmp - 1;

                countEmp++;
            }*/
        }

        private void btnConfigMapCollumn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fileName = System.IO.Directory.GetCurrentDirectory() + @"\Config Map Collumn\config.txt";
            Process.Start("notepad.exe", fileName);
        }
        private string LayDuLieu(string strLoaiDuLieu, string employeeOid)
        {
            string value = string.Empty;
            SelectedData sdata;
            switch(strLoaiDuLieu)
            {
                case "Tên Nhân Viên":
                    sdata = session.ExecuteQuery(string.Format("SELECT \"LastName\" FROM \"Person\" WHERE \"Oid\" = '{0}'", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    sdata = session.ExecuteQuery(string.Format("SELECT \"FirstName\" FROM \"Person\" WHERE \"Oid\" = '{0}'",employeeOid));
                    value += " " + sdata.ResultSet[0].Rows[0].Values[0].ToString();                  
                    break;
                case "Mã Nhân Viên":
                    sdata = session.ExecuteQuery(string.Format("SELECT \"EmployeeCode\" FROM \"Employee\" WHERE \"Oid\" = '{0}'", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Phòng Ban":
                    sdata = session.ExecuteQuery(string.Format("SELECT b.\"DepartmentName\" FROM \"Employee\" a, \"Department\" b WHERE a.\"Department\" = b.\"Oid\""));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Vị Trí":
                    sdata = session.ExecuteQuery(string.Format("SELECT b.\"PositionName\" FROM \"Employee\" a, \"Position\" b WHERE a.\"Position\" = b.\"Oid\""));
                    try
                    {
                        value = sdata.ResultSet[0].Rows[0].Values[0].ToString();                    
                    }
                    catch
                    {
                        value = "";
                    }
                    break;
                case "Chi Nhánh":
                    sdata = session.ExecuteQuery(string.Format("SELECT b.\"BranchName\" FROM \"Employee\" a, \"Branch\" b WHERE a.\"Branch\" = b.\"Oid\""));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Lương Cơ Bản":
                    sdata = session.ExecuteQuery(string.Format("SELECT \"LuongCoBan\" FROM \"Employee\" WHERE \"Oid\" = '{0}'", employeeOid));
                    value = (sdata.ResultSet[0].Rows[0].Values[0] != null) ? sdata.ResultSet[0].Rows[0].Values[0].ToString() : "0";
                    break;
                case "Mã Số Thuế":
                      sdata = session.ExecuteQuery(string.Format("SELECT \"MaSoThue\" FROM \"Employee\" WHERE \"Oid\" = '{0}'", employeeOid));
                    value = (sdata.ResultSet[0].Rows[0].Values[0] != null) ? sdata.ResultSet[0].Rows[0].Values[0].ToString() : "";
                    break;
                case "Số Ngày Công":
                      sdata = session.ExecuteQuery(string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Ngày Tính Công' AND a.\"DataTypeTimekeeping\" = b.\"Oid\"", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Số Ngày Công Thực Tế":
                     sdata = session.ExecuteQuery(string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Ngày Công Thực Tế'  AND a.\"DataTypeTimekeeping\" = b.\"Oid\" ", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Số Giờ Tăng Ca NT":
                    string s = string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Số Giờ Tăng Ca NT'  AND a.\"DataTypeTimekeeping\" = b.\"Oid\" ", employeeOid);
                     sdata = session.ExecuteQuery(s);
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Số Giờ Tăng Ca NT Ca Đêm":
                      sdata = session.ExecuteQuery(string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Số Giờ Tăng Ca NT Ca Đêm'  AND a.\"DataTypeTimekeeping\" = b.\"Oid\" ", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Số Giờ Tăng Ca NN":
                      sdata = session.ExecuteQuery(string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Số Giờ Tăng Ca NN'  AND a.\"DataTypeTimekeeping\" = b.\"Oid\" ", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Số Giờ Tăng Ca NN Ca Đêm":
                      sdata = session.ExecuteQuery(string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Số Giờ Tăng Ca NT Ca Đêm'  AND a.\"DataTypeTimekeeping\" = b.\"Oid\" ", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
                case "Số Giờ Tăng Ca NL":
                      sdata = session.ExecuteQuery(string.Format("SELECT a.\"Sum\" FROM \"TimekeepingMonth\" a, \"DataTypeTimekeeping\" b WHERE \"Employee\" = '{0}' AND" +
                      " b.\"DataType\" = 'Số Giờ Tăng Ca NL'  AND a.\"DataTypeTimekeeping\" = b.\"Oid\" ", employeeOid));
                    value = sdata.ResultSet[0].Rows[0].Values[0].ToString();
                    break;
            }
            return value;
        }       

        

        private void DuaDuLieuLenBangLuong()
        {
            IWorkbook workbook = spreadControl.Document;
            Worksheet worksheet = workbook.Worksheets[0];

            Dictionary<int, string> dic = new Dictionary<int, string>();
            StreamReader sr = new StreamReader(fileName);
            while (sr.Peek() >= 0)
            {
                string line = sr.ReadLine();
                if (line != null)
                {
                    string[] result = line.Split('|');
                    int value;
                    if (int.TryParse(result[1].ToString(), out value))
                    {
                        dic.Add(value, result[0]);
                    }
                }
            }
            sr.Close();
        
           
            

            XPQuery<Employee> _Employee = new XPQuery<Employee>(session);

            List<Employee> listEmployee = (from tso in _Employee select tso).ToList();
            int countEmp = 2;
            foreach (Employee emp in listEmployee)
            {               
                foreach(KeyValuePair<int, string> pair in dic)
                {
                    if(pair.Key > 0)
                    {
                        worksheet[countEmp, pair.Key].Value = LayDuLieu(pair.Value, emp.Oid.ToString());                       
                    }
                }
                countEmp++;
            }

        }

    }
}
