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
using EasyHRM.Module.BusinessObjects;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.XtraBars;
using DevExpress.ExpressApp.Win.Templates.ActionContainers;

namespace EasyHRM.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class TimekeepingDetailViewController : ViewController
    {
        ControlDetailItem propertyEditor;
        public TimekeepingDetailViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.

            IBarManagerHolder bmh = Frame.Template as IBarManagerHolder;
            if (bmh == null || bmh.BarManager == null) return;
            foreach (BarItem item in bmh.BarManager.Items)
            {
                BarButtonItem editItem = item as BarButtonItem;
                if (editItem != null && Convert.ToString(item.Tag).Contains("ID=\"ShowWorkflowInstances\""))
                {
                    item.Visibility = BarItemVisibility.Never;
                    item.VisibleWhenVertical = false;
                }
                ActionContainerBarItem acBarItem = item as ActionContainerBarItem;
                if (acBarItem != null && Convert.ToString(item.Caption).Contains("Workflow"))
                {
                    item.Visibility = BarItemVisibility.Never;
                    item.VisibleWhenVertical = false;
                }
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            PropertyEditor propertyEditor = ((DetailView)View).FindItem("Shift") as PropertyEditor;
            if (propertyEditor != null)
            {
                propertyEditor.ControlValueChanged += propertyEditor_ControlValueChanged;
            }

        }

        void propertyEditor_ControlValueChanged(object sender, EventArgs e)
        {
            PropertyEditor propertyEditor1 = sender as PropertyEditor;
            Shift s = propertyEditor1.ControlValue as Shift;
            if (s != null)
            {
                Timekeeping t = propertyEditor1.CurrentObject as Timekeeping;
                t.Shift = s;
                double _SoGioLamCuaCa = 0;
                TimeSpan tsValue = (t.ThoiGianRa - t.ThoiGianVao);

                Parameter parameter;
                using (IObjectSpace objNewSpace = Frame.Application.CreateObjectSpace())
                {
                    XPCollection xpcPar = new XPCollection(((XPObjectSpace)objNewSpace).Session, typeof(Parameter));
                    parameter = xpcPar[0] as Parameter;
                }


                // 1. Cập nhật đi trể về sớm
                DefaultValueTimekeeping value = ObjectSpace.FindObject<DefaultValueTimekeeping>(
                    new BinaryOperator("Shift", s));
                if (value != null)
                {
                    TimeSpan ts = t.ThoiGianVao - value.ThoiGianVao;
                    if (ts.TotalMinutes > 0)
                    {
                        t.SoPhutDiTre = Math.Round(ts.TotalMinutes, 2);
                    }
                    ts = t.ThoiGianRa - value.ThoiGianRa;
                    if (ts.TotalMinutes < 0)
                    {
                        t.SoPhutVeSom = Math.Round(-ts.TotalMinutes, 2);
                    }
                    _SoGioLamCuaCa = value.TongSoGioLam;
                }

                // 2. Cập nhật tổng số giờ làm 


                // 3. Cập nhật ngày công thực tế, ngày tính công
                t.NgayTinhCong = 1;
                t.NgayCongThucTe = 1;

                // 4. Cập nhật tính giờ tăng ca

                // Kiểm tra ngày chấm công có phải ngày lễ không
                t.SoGioTangCaNL = 0;
                t.SoGioTangCaNN = 0;
                t.SoGioTangCaNNCaDem = 0;
                t.SoGioTangCaNT = 0;
                t.SoGioTangCaNTCaDem = 0;

                Holiday holidayDate = ObjectSpace.FindObject<Holiday>(CriteriaOperator.And(new BinaryOperator(
                    "StartDate", t.Date, BinaryOperatorType.GreaterOrEqual), new BinaryOperator(
                        "EndDate", t.Date, BinaryOperatorType.LessOrEqual)));
                if (holidayDate != null)
                {
                    t.SoGioTangCaNL = Math.Round(tsValue.TotalHours, 2);

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
                                t.SoGioTangCaNT = Math.Round(d, 2);

                                TimeSpan ts2 = t.ThoiGianRa - parameter.ThoiGianTinhCaDem;
                                // Nếu giờ ra sau giờ ra tính ca đêm, thì mới tính ca đêm
                                if (ts2.TotalHours >= parameter.SoGioToiThieuTinhTangCa)
                                {
                                    t.SoGioTangCaNTCaDem = Math.Round(ts2.TotalHours, 2);
                                    t.SoGioTangCaNT = Math.Round((tsValue.TotalHours - ts2.TotalHours), 2);
                                }
                            }
                        }
                    }
                    else
                    {
                        t.SoGioTangCaNN = Math.Round(tsValue.TotalHours, 2);
                        TimeSpan ts2 = t.ThoiGianRa - parameter.ThoiGianTinhCaDem;
                        // Nếu giờ ra sau giờ ra tính ca đêm, thì mới tính ca đêm
                        if (ts2.TotalHours >= parameter.SoGioToiThieuTinhTangCa)
                        {
                            t.SoGioTangCaNNCaDem = Math.Round(ts2.TotalHours, 2);
                            t.SoGioTangCaNN = Math.Round((tsValue.TotalHours - ts2.TotalHours), 2);
                        }
                    }
                }
                t.Save();
                ObjectSpace.CommitChanges();
            }
        }
        
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
