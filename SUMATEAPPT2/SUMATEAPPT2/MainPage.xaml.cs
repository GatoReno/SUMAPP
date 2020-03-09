using SUMATEAPPT2.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SUMATEAPPT2
{
    public partial class MainPage : MasterDetailPage
    {
        public   MainPage()
        {
            InitializeComponent();
            this.Master = new master();
            this.Detail = new NavigationPage(new detail());         
            App.MasterD = this;
        }
        

    }
}
