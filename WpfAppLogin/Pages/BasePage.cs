using System.Windows.Controls;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System;

namespace WpfAppLogin
{
    // Base page to implement basic functionality in all pages
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {

        private VM mViewModel;



        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {

                if (mViewModel == value) // if nothing has changed, return
                    return;


                mViewModel = value; // update the value


                this.DataContext = mViewModel; //set the dataContext for this page
            }
        }


        public BasePage()
        {

            //this.Loaded += BasePage_Loaded;

            // Create a default view model
            this.ViewModel = new VM();
        }
        private void BasePage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
