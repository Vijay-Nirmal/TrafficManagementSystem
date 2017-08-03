using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TrafficManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double actualHeight = 0, actualWidth = 0;
        Dictionary<int, UIElement> carList = new Dictionary<int, UIElement>();
        int carNo = 0;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            actualHeight = Window.Current.Bounds.Height;
            actualWidth = Window.Current.Bounds.Width;

            RightRoadLine.X2 = actualWidth / 2 - 150;
            RightRoadTopLine.X2 = actualWidth / 2 - 150;
            RightRoadBottomLine.X2= actualWidth / 2 - 150;
            LeftRoadLine.X2 = actualWidth / 2 - 150;
            LeftRoadTopLine.X2 = actualWidth / 2 - 150;
            LeftRoadBottomLine.X2 = actualWidth / 2 - 150;
            TopRoadLine.Y2 = actualHeight / 2 - 150;
            TopRoadRightLine.Y2 = actualHeight / 2 - 150;
            TopRoadLeftLine.Y2 = actualHeight / 2 - 150;
            BottomRoadLine.Y2 = actualHeight / 2 - 150;
            BottomRoadRightLine.Y2 = actualHeight / 2 - 150;
            BottomRoadLeftLine.Y2 = actualHeight / 2 - 150;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            actualHeight = Window.Current.Bounds.Height;
            actualWidth = Window.Current.Bounds.Width;

            RightRoadLine.X2 = actualWidth / 2 - 150;
            RightRoadTopLine.X2 = actualWidth / 2 - 150;
            RightRoadBottomLine.X2 = actualWidth / 2 - 150;
            LeftRoadLine.X2 = actualWidth / 2 - 150;
            LeftRoadTopLine.X2 = actualWidth / 2 - 150;
            LeftRoadBottomLine.X2 = actualWidth / 2 - 150;
            TopRoadLine.Y2 = actualHeight / 2 - 150;
            TopRoadRightLine.Y2 = actualHeight / 2 - 150;
            TopRoadLeftLine.Y2 = actualHeight / 2 - 150;
            BottomRoadLine.Y2 = actualHeight / 2 - 150;
            BottomRoadRightLine.Y2 = actualHeight / 2 - 150;
            BottomRoadLeftLine.Y2 = actualHeight / 2 - 150;

            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private void Click1_Click(object sender, RoutedEventArgs e)
        {
            /*var transformGroup = new TransformGroup();

            var translateTransform = new TranslateTransform()
            {
                X = 0,
                Y = actualHeight / 2 - 145
            };
            var rotateTransform = new RotateTransform()
            {
                Angle = 90
            };

            transformGroup.Children.Add(translateTransform);
            transformGroup.Children.Add(rotateTransform);*/


            var myCar = new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/RedCar.png")),
                Width = 140,
                Height = 65,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                RenderTransform = new TranslateTransform()
                {
                    X = 0,
                    Y = actualHeight / 2 - 145
                }
            };

            carList.Add(carNo++, myCar);
            VehicleGrid.Children.Add(myCar);
        }

        private void Click2_Click(object sender, RoutedEventArgs e)
        {
            MoreForward(carList[(carNo-1)]);
        }

        private void MoreForward(UIElement element)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                Duration = new Duration(new TimeSpan(0, 0, 3)),
                To = LeftRoad.ActualWidth - 140,
                /*EasingFunction = new CircleEase()
                {
                    EasingMode = EasingMode.EaseOut
                }*/
            };

            Storyboard.SetTarget(doubleAnimation, element.RenderTransform);
            Storyboard.SetTargetProperty(doubleAnimation, "X");
            storyboard.Children.Add(doubleAnimation);

            storyboard.Begin();
        }

        private void Turn(UIElement element)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                Duration = new Duration(new TimeSpan(0, 0, 2)),
                To = LeftRoad.ActualWidth - 140,
                EasingFunction = new CircleEase()
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            Storyboard.SetTarget(doubleAnimation, element.RenderTransform);
            Storyboard.SetTargetProperty(doubleAnimation, "X");
            storyboard.Children.Add(doubleAnimation);


            storyboard.Begin();
        }

    }
}
