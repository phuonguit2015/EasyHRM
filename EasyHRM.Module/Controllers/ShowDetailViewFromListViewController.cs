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
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace EasyHRM.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ShowDetailViewFromListViewController : ViewController<ListView>, IModelExtender
    {
        public const string EnabledKeyShowDetailView = "ShowDetailViewFromListViewController";
        protected override void OnActivated()
        {
            base.OnActivated();
            ListViewProcessCurrentObjectController targetController = Frame.GetController<ListViewProcessCurrentObjectController>();
            targetController.ProcessCurrentObjectAction.Enabled[EnabledKeyShowDetailView] = ((IModelShowDetailView)View.Model).ShowDetailView;
        }
        public void ExtendModelInterfaces(DevExpress.ExpressApp.Model.ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelViews, IModelDefaultShowDetailView>();
            extenders.Add<IModelListView, IModelShowDetailView>();
        }
    }
    public interface IModelDefaultShowDetailView : IModelNode
    {
        [DefaultValue(true)]
        bool DefaultShowDetailViewFromListView { get; set; }
    }
    public interface IModelShowDetailView : IModelNode
    {
        bool ShowDetailView { get; set; }
    }
    [DomainLogic(typeof(IModelShowDetailView))]
    public static class ModelShowDetailViewLogic
    {
        public static bool Get_ShowDetailView(IModelShowDetailView showDetailView)
        {
            IModelDefaultShowDetailView defaultShowDetailViewFromListView = showDetailView.Parent as IModelDefaultShowDetailView;
            if (defaultShowDetailViewFromListView != null)
            {
                return defaultShowDetailViewFromListView.DefaultShowDetailViewFromListView;
            }
            return true;
        }
    }

}
