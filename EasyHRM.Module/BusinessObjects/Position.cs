using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace EasyHRM.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("List Manager")]
    public class Position : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Position(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }

        private string positionName;
        private string description;


        [XafDisplayName("Chức Vụ")]
        [Indexed(Unique = true)]
        public string PositionName
        {
            get { return positionName; }
            set { SetPropertyValue("PositionName", ref positionName, value); }
        }

        [XafDisplayName("Mô Tả")]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }      
    }
}
