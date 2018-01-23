using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WpfAppLogin
{

    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };


        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //Command Helpers

        // runs a command if the flag is not set (meaning the function is running) then the action will run and the flag will be resetted at the end
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if (updatingFlag.GetPropertyValue())
                return;

            updatingFlag.SetPropertyValue(true);

            try
            {
                // run the passed in action
                await action();
            }
            finally
            {
                // set the property value back to false
                updatingFlag.SetPropertyValue(false);
            }
        }

    }
}
