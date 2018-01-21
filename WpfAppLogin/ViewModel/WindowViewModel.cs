using System;
using System.Windows;
using System.Windows.Input;


namespace WpfAppLogin.ViewModel
{
    // Custom Window ViewModel
    public class WindowViewModel : BaseViewModel
    {

        private Window mWindow; //controlled by this ViewModel
        private int mOuterMarginSize = 10; // margin  around window for shadow
        private int mWindowRadius = 10; //curved edges


        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;


        public double WindowMinimumWidth { get; set; } = 800;

        public double WindowMinimumHeight { get; set; } = 400;


        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); } }

        public int ResizeBorder { get { return Borderless ? 0 : 6; } } // size of resize border around the window


        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } } // size of resize border around the window taking into account the outer margin

        //Inner padding of the inner content of the main window
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);


        public int OuterMarginSize //to eliminate outermargin (no shadow) when maximized
        {
            get
            {
                // if it is maximized or docked, no border
                return Borderless ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }


        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }


        public int WindowRadius
        {
            get
            {
                // if it is maximized or docked, no border
                return Borderless ? 0 : mWindowRadius;
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


        public ICommand MaximizeCommand { get; set; }


        public ICommand CloseCommand { get; set; }

        //Command to show the system menu of the Window
        public ICommand MenuCommand { get; set; }

        // Constructor
        public WindowViewModel(Window window)
        {
            mWindow = window;

            // window resizing listener
            mWindow.StateChanged += (sender, e) =>
            {
                // fire off events for all properties that are affected by a resize
                WindowResized();
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);

            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }


        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }


        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }

    } 

}
