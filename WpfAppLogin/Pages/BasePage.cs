using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfAppLogin.ViewModel;

namespace WpfAppLogin
{
    // Base page to implement basic functionality in all pages
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        private VM mViewModel;

        //ViewModel associated with the page
        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                if (mViewModel == value) // if nothing has changed return
                    return;

                //update the value
                mViewModel = value;

                // set the data context for this page
                this.DataContext = mViewModel;
            }
        }

        public BasePage()
        {
            this.Loaded += BasePage_Loaded;

            //create a default viewmodel
            this.ViewModel = new VM();
        }

        private void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
