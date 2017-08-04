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
            carList[(carNo - 1)].AnimateTransform("Translate", Orientation.Horizontal, null, LeftRoad.ActualWidth - 140, duration: 3000, easing: new SineEase
            {
                EasingMode = EasingMode.EaseInOut
            });
        }

        private void Click4_Click(object sender, RoutedEventArgs e)
        {
            carList[(carNo - 1)].AnimateTransform("Translate", Orientation.Vertical, null, actualHeight, duration: 3000, easing: new SineEase
            {
                EasingMode = EasingMode.EaseInOut
            });
        }

        private void Click3_Click(object sender, RoutedEventArgs e)
        {
            carList[(carNo - 1)].AnimateTransform("Translate", Orientation.Horizontal, null, LeftRoad.ActualWidth, duration: 3000, easing: new SineEase
            {
                EasingMode = EasingMode.EaseOut
            });

            carList[(carNo - 1)].AnimateTransform("Translate", Orientation.Vertical, null, actualHeight / 2 - 75, duration: 3000, easing: new SineEase
            {
                EasingMode = EasingMode.EaseIn
            });
            carList[(carNo - 1)].AnimateTransform("Rotation", null, null, 90, duration: 3000, startTime: 0);
        }

    }

    public static class Helpers
    {
        public static void AnimateTransform(this UIElement target, string propertyToAnimate, Orientation? orientation, double? from, double to, int duration = 3000, int startTime = 0, EasingFunctionBase easing = null)
        {
            if (easing == null)
            {
                easing = new ExponentialEase();
            }

            var transform = target.RenderTransform as CompositeTransform;
            if (transform == null)
            {
                transform = new CompositeTransform();
                target.RenderTransform = transform;
            }
            target.RenderTransformOrigin = new Point(0.5, 0.5);

            var db = new DoubleAnimation
            {
                To = to,
                From = from,
                EasingFunction = easing,
                Duration = TimeSpan.FromMilliseconds(duration)
            };
            Storyboard.SetTarget(db, target);

            var axis = string.Empty;
            if (orientation.HasValue)
            {
                axis = orientation.Value == Orientation.Horizontal ? "X" : "Y";
            }
            Storyboard.SetTargetProperty(db, $"(UIElement.RenderTransform).(CompositeTransform.{propertyToAnimate}{axis})");

            var sb = new Storyboard
            {
                BeginTime = TimeSpan.FromMilliseconds(startTime)
            };

            sb.Children.Add(db);
            sb.Begin();
        }
    }

}
