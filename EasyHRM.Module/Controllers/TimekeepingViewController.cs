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
    }
}
