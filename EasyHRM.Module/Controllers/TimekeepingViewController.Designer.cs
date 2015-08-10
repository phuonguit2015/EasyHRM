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
            this.acFilterByEmployeeCode = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            this.ImportToTimekeepingMonth = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
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
            // acFilterByEmployeeCode
            // 
            this.acFilterByEmployeeCode.Caption = "Filter By Employee Code";
            this.acFilterByEmployeeCode.ConfirmationMessage = null;
            this.acFilterByEmployeeCode.Id = "FilterByEmployeeCode";
            this.acFilterByEmployeeCode.NullValuePrompt = null;
            this.acFilterByEmployeeCode.ShortCaption = null;
            this.acFilterByEmployeeCode.ToolTip = null;
            this.acFilterByEmployeeCode.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.acFilterByEmployeeCode_Execute);
            // 
            // ImportToTimekeepingMonth
            // 
            this.ImportToTimekeepingMonth.Caption = null;
            this.ImportToTimekeepingMonth.ConfirmationMessage = null;
            this.ImportToTimekeepingMonth.Id = "6f59fa7a-31ef-41f0-97c1-df6497d378a5";
            this.ImportToTimekeepingMonth.ToolTip = null;
            this.ImportToTimekeepingMonth.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ImportToTimekeepingMonth_Execute);
            // 
            // TimekeepingViewController
            // 
            this.TargetObjectType = typeof(EasyHRM.Module.BusinessObjects.Timekeeping);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acImportFromExcel;
        private DevExpress.ExpressApp.Actions.ParametrizedAction acFilterByEmployeeCode;
        private DevExpress.ExpressApp.Actions.SimpleAction ImportToTimekeepingMonth;

    }
}
