namespace EasyHRM.Module.Controllers
{
    partial class TimekeepingMonthViewController 
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.singleChoiceAction1 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // singleChoiceAction1
            // 
            this.singleChoiceAction1.Caption = null;
            this.singleChoiceAction1.ConfirmationMessage = null;
            this.singleChoiceAction1.Id = "fb80ec9c-781e-49eb-bba3-b4f6adc80264";
            this.singleChoiceAction1.ToolTip = null;
            this.singleChoiceAction1.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.singleChoiceAction1_Execute);
            // 
            // TimekeepingMonthViewController
            // 
            this.TargetObjectType = typeof(EasyHRM.Module.BusinessObjects.TimekeepingMonth);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceAction1;

    }
}
