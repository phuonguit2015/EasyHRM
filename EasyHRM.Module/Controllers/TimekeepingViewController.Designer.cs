namespace EasyHRM.Module.Controllers
{
    partial class TimekeepingViewController
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
            this.acImportFromExcel = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ImportToTimekeepingMonth = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acFilterByTimekeepingMonth = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // acImportFromExcel
            // 
            this.acImportFromExcel.Caption = "Import From Excel";
            this.acImportFromExcel.ConfirmationMessage = null;
            this.acImportFromExcel.Id = "ImportFromExcel";
            this.acImportFromExcel.ImageName = "Action_Expor";
            this.acImportFromExcel.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.acImportFromExcel.ToolTip = null;
            this.acImportFromExcel.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.acImportFromExcel.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ImportFromExcel_Execute);
            // 
            // ImportToTimekeepingMonth
            // 
            this.ImportToTimekeepingMonth.Caption = "Convert To Timekeeping Month";
            this.ImportToTimekeepingMonth.ConfirmationMessage = null;
            this.ImportToTimekeepingMonth.Id = "ConvertToTimekeepingMonth";
            this.ImportToTimekeepingMonth.ToolTip = null;
            this.ImportToTimekeepingMonth.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ImportToTimekeepingMonth_Execute);
            // 
            // acFilterByTimekeepingMonth
            // 
            this.acFilterByTimekeepingMonth.Caption = "Filter By Timekeeping Month";
            this.acFilterByTimekeepingMonth.ConfirmationMessage = null;
            this.acFilterByTimekeepingMonth.Id = "FilterByTimekeepingMonth";
            this.acFilterByTimekeepingMonth.ToolTip = null;
            this.acFilterByTimekeepingMonth.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.acFilterByTimekeepingMonth_Execute);
            // 
            // TimekeepingViewController
            // 
            this.TargetObjectType = typeof(EasyHRM.Module.BusinessObjects.Timekeeping);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acImportFromExcel;
        private DevExpress.ExpressApp.Actions.SimpleAction ImportToTimekeepingMonth;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction acFilterByTimekeepingMonth;

    }
}
