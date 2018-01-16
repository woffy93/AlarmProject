using System;
using System.Windows;
using System.Windows.Input;
using WpfAppLogin.ViewModel.Base;

namespace WpfAppLogin.ViewModel
{
    // Custom Window ViewModel
    public class WindowViewModel : BaseViewModel
    {
        private Window mWindow; //controlled by this ViewModel
        private int mOuterMarginSize = 10; // margin  around window for shadow
        private int mWindowRadius = 10; //curved edges

        //public String Test { get; set; } = "prova";
        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMinimumHeigth { get; set; } = 400;

        public int ResizeBorder { get; set; } = 6; // size of resize border around the window
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } } // size of resize border around the window taking into account the outer margin

        //Inner padding of the inner content of the main window
        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }

        public int OuterMarginSize //to eliminate outermargin (no shadow) when maximized
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }

            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThikness { get { return new Thickness(OuterMarginSize); } }


        public int WindowRadius //to eliminate window rounded edges when maximized
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }

            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42; // height of the titlebar

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login; // The current page of the application


        //Command to minimize the Window
        public ICommand MinimizeCommand { get; set; }

        //Command to maximize the Window
        public ICommand MaximizeCommand { get; set; }

        //Command to close the Window
        public ICommand CloseCommand { get; set; }

        //Command to show the system menu of the Window
        public ICommand MenuCommand { get; set; }


        //Constructor
        public WindowViewModel(Window window)
        {
            mWindow = window;

            //window resizing listener
            mWindow.StateChanged += (sender, e) =>
            {
                // fire off all events affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThikness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

        }

        // Get current mouse position
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

    }
}
