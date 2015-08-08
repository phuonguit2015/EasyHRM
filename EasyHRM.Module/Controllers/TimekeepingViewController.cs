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
            PropertyEditor propertyEditor = ((DetailView)View).FindItem("DataTypeTimekeeping") as PropertyEditor;
            if (propertyEditor != null)
            {
                if (propertyEditor.Control != null)
                {
                    InitNullText(propertyEditor);
                }
                else
                {
                    propertyEditor.ControlValueChanged += propertyEditor_ControlValueChanged;
                }
            }
        }

        void propertyEditor_ControlValueChanged(object sender, EventArgs e)
        {
            //PropertyEditor propertyEditor = ((DetailView)View).FindItem("Value") as PropertyEditor;
            //PropertyEditor newp = new DatePropertyEditor(propertyEditor.GetType(),
            //    propertyEditor.Model); 
            ////if (propertyEditor != null)
            ////{
            ////    ((DetailView)View).RemoveItem("Value");
            ////    ((DetailView)View).AddItem(newp);
              
            ////}
            //propertyEditor.ObjectType = 
        }

        void propertyEditor_ControlCreating(object sender, EventArgs e)
        {
            PropertyEditor p = sender as PropertyEditor;
           
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

        private void propertyEditor_ControlCreated(object sender, EventArgs e)
        {
            InitNullText((PropertyEditor)sender);
        }
        private void InitNullText(PropertyEditor propertyEditor)
        {
            ((BaseEdit)propertyEditor.Control).Properties.NullText = CaptionHelper.NullValueText;
        }
    }
}
