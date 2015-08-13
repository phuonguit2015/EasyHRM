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
using DevExpress.ExpressApp.Win.Editors;
using EasyHRM.Module.BusinessObjects;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraLayout;
using System.Windows.Forms;

namespace EasyHRM.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class TimekeepingMonthViewController : ViewController
    {
        ChoiceActionItem setLookupItem;
        TimekeepingName tkname = null;

        public TimekeepingMonthViewController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.

            TargetViewType = ViewType.ListView;

         
        }

       
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.ControlsCreated += new EventHandler(View_ControlsCreated);
            XPQuery<TimekeepingName> _TimekeepingName = new XPQuery<TimekeepingName>((
                   (XPObjectSpace)ObjectSpace).Session);

            List<TimekeepingName> listTimekeepingName = (from tso in _TimekeepingName select tso).ToList();
            singleChoiceAction1.Items.Clear();
            
            foreach (TimekeepingName item in listTimekeepingName)
            {
                if(item.StartDate <= DateTime.Now && DateTime.Now <= item.EndDate)
                {
                    tkname = item;
                }
                setLookupItem = new ChoiceActionItem(item.timekeepingName, item);

                singleChoiceAction1.Items.Add(setLookupItem);
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


        private void ChangeColumnCaption()
        {
            GridListEditor editor1 = (GridListEditor)((DevExpress.ExpressApp.ListView)View).Editor;
            editor1.GridView.OptionsView.ColumnAutoWidth = false;
            int i = 0;
            foreach (XafGridColumn col in editor1.GridView.Columns)
            {
                col.VisibleIndex = 0;
                col.Width = 120;
                if (tkname != null)
                {
                    DateTime d = tkname.StartDate.AddDays(i);
                    if (d <= tkname.EndDate && col.FieldName.Contains("Ngay"))
                    {
                        col.Caption = d.ToString("dd/MM");
                        col.Width = 55;
                        i++;
                        col.VisibleIndex = i + 4;
                    }
                    else if (col.FieldName.Contains("Ngay"))
                    {
                        col.VisibleIndex = -1;
                    }

                }
            }
            editor1.GridView.ColumnPanelRowHeight = 30;
            editor1.GridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {

            ChangeColumnCaption();           

        }

      
     
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
             tkname = e.SelectedChoiceActionItem.Data as TimekeepingName;
             ((DevExpress.ExpressApp.ListView)View).CollectionSource.Criteria.Clear();
             ((DevExpress.ExpressApp.ListView)View).CollectionSource.Criteria["Filter1"] = new BinaryOperator(
            "TimekeepingName.Oid", tkname.Oid, BinaryOperatorType.Equal);
            
             ChangeColumnCaption();
        }

        private void acSum_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
           
            foreach (TimekeepingMonth tkmonth in ((DevExpress.ExpressApp.ListView)View).CollectionSource.List)
            {
                double sum = 0;
                for (int i = 1; i < 32; i++)
                {
                    try
                    {
                        sum += Convert.ToDouble(tkmonth[i]);
                    }
                    catch
                    {
                        break;
                    }
                }
                double j = Math.Round(sum,2);
                string str = string.Format("UPDATE \"TimekeepingMonth\" SET \"Sum\" = '{1}', \"OptimisticLockField\" = null, \"GCRecord\" = null WHERE" +
                    "\"Oid\" = '{0}'", tkmonth.Oid,j);
                ((XPObjectSpace)ObjectSpace).Session.BeginTransaction();
                ((XPObjectSpace)ObjectSpace).Session.ExecuteNonQuery(str);
                ((XPObjectSpace)ObjectSpace).Session.CommitTransaction();
            }
        }

        private void acTinhLuong_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Form f = new Salaries(((XPObjectSpace)ObjectSpace).Session);
            f.ShowDialog();
        }

    }
}
