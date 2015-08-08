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
    public class Branch : Address
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Branch(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        
        private string branchName;
        private string description;
        

        [XafDisplayName("Tên Chi Nhánh")]
        [Indexed(Unique = true)]
        public string BranchName
        {
            get { return branchName; }
            set { SetPropertyValue("BranchName", ref branchName, value); }
        }

        [XafDisplayName("Mô Tả")]
        public string Description
        {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }      
    }
}
