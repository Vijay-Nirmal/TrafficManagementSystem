using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using static TrafficManagement.Helpers;

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
        public const int vehicleLength = 50, roadWidth = 140;


        List<Uri> carType = new List<Uri>()
        {
            new Uri("ms-appx:///Assets/Vehicle/RedCar.png"),
            new Uri("ms-appx:///Assets/Vehicle/GreenCar.png"),
            new Uri("ms-appx:///Assets/Vehicle/YellowCar.png"),
            new Uri("ms-appx:///Assets/Vehicle/PinkCar.png"),
            new Uri("ms-appx:///Assets/Vehicle/BlueCar.png")
        };
        int carNo = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReCalculate();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ReCalculate();

            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private void ReCalculate()
        {
            actualHeight = Window.Current.Bounds.Height;
            actualWidth = Window.Current.Bounds.Width;

            //Row 0
            LeftTopVerticalRoadLine.Y2 = LeftTopVerticalRoad.ActualHeight;
            LeftTopVerticalRoadLineL.Y2 = LeftTopVerticalRoad.ActualHeight;
            LeftTopVerticalRoadLineR.Y2 = LeftTopVerticalRoad.ActualHeight;
            RightTopVerticalRoadLine.Y2 = RightTopVerticalRoad.ActualHeight;
            RightTopVerticalRoadLineL.Y2 = RightTopVerticalRoad.ActualHeight;
            RightTopVerticalRoadLineR.Y2 = RightTopVerticalRoad.ActualHeight;

            //Row 1
            LeftTopHorizontalRoadLine.X2 = LeftTopHorizontalRoad.ActualWidth;
            LeftTopHorizontalRoadLineT.X2 = LeftTopHorizontalRoad.ActualWidth;
            LeftTopHorizontalRoadLineB.X2 = LeftTopHorizontalRoad.ActualWidth;
            CenterTopHorizontalRoadLine.X2 = CenterTopHorizontalRoad.ActualWidth;
            CenterTopHorizontalRoadLineT.X2 = CenterTopHorizontalRoad.ActualWidth;
            CenterTopHorizontalRoadLineB.X2 = CenterTopHorizontalRoad.ActualWidth;
            RightTopHorizontalRoadLine.X2 = RightTopHorizontalRoad.ActualWidth;
            RightTopHorizontalRoadLineT.X2 = RightTopHorizontalRoad.ActualWidth;
            RightTopHorizontalRoadLineB.X2 = RightTopHorizontalRoad.ActualWidth;

            //Row 2
            LeftCenterVerticalRoadLine.Y2 = LeftCenterVerticalRoad.ActualHeight;
            LeftCenterVerticalRoadLineL.Y2 = LeftCenterVerticalRoad.ActualHeight;
            LeftCenterVerticalRoadLineR.Y2 = LeftCenterVerticalRoad.ActualHeight;
            RightCenterVerticalRoadLine.Y2 = RightCenterVerticalRoad.ActualHeight;
            RightCenterVerticalRoadLineL.Y2 = RightCenterVerticalRoad.ActualHeight;
            RightCenterVerticalRoadLineR.Y2 = RightCenterVerticalRoad.ActualHeight;

            //Row 3
            LeftBottomHorizontalRoadLine.X2 = LeftBottomHorizontalRoad.ActualWidth;
            LeftBottomHorizontalRoadLineT.X2 = LeftBottomHorizontalRoad.ActualWidth;
            LeftBottomHorizontalRoadLineB.X2 = LeftBottomHorizontalRoad.ActualWidth;
            CenterBottomHorizontalRoadLine.X2 = CenterBottomHorizontalRoad.ActualWidth;
            CenterBottomHorizontalRoadLineT.X2 = CenterBottomHorizontalRoad.ActualWidth;
            CenterBottomHorizontalRoadLineB.X2 = CenterBottomHorizontalRoad.ActualWidth;
            RightBottomHorizontalRoadLine.X2 = RightBottomHorizontalRoad.ActualWidth;
            RightBottomHorizontalRoadLineT.X2 = RightBottomHorizontalRoad.ActualWidth;
            RightBottomHorizontalRoadLineB.X2 = RightBottomHorizontalRoad.ActualWidth;

            //Row 4
            LeftBottomVerticalRoadLine.Y2 = LeftBottomHorizontalRoad.ActualHeight;
            LeftBottomVerticalRoadLineL.Y2 = LeftBottomHorizontalRoad.ActualHeight;
            LeftBottomVerticalRoadLineR.Y2 = LeftBottomHorizontalRoad.ActualHeight;
            RightBottomVerticalRoadLine.Y2 = RightBottomVerticalRoad.ActualHeight;
            RightBottomVerticalRoadLineL.Y2 = RightBottomVerticalRoad.ActualHeight;
            RightBottomVerticalRoadLineR.Y2 = RightBottomVerticalRoad.ActualHeight;
        }

        private void Click1_Click(object sender, RoutedEventArgs e)
        {
            var myCar = new Image()
            {
                Source = new BitmapImage(carType[new Random().Next(0, 4)]),
                Width = vehicleLength,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                RenderTransform = new CompositeTransform()
                {
                    TranslateX = 0,
                    TranslateY = LeftTopVerticalRoad.ActualHeight + 40
                }
            };

            carList.Add(carNo++, myCar);
            VehicleGrid.Children.Add(myCar);
        }

        private void Click2_Click(object sender, RoutedEventArgs e)
        {
            carList[(carNo - 1)].MoveForward(Orientation.Horizontal, LeftTopHorizontalRoad.ActualWidth - vehicleLength);
        }

        private void Click4_Click(object sender, RoutedEventArgs e)
        {
            carList[(carNo - 1)].MoveForward(Orientation.Vertical, actualHeight);
        }

        private void Click3_Click(object sender, RoutedEventArgs e)
        {
            carList[(carNo - 1)].Turn(Direction.Left, Orientation.Horizontal);
        }

    }

    public static class Helpers
    {
        public const int vehicleLength = MainPage.vehicleLength;
        public const int roadWidth = MainPage.roadWidth;

        public static void AnimateTransform(this UIElement target, string propertyToAnimate, Orientation? orientation, double? from, double to, int duration = 3000, int startTime = 0, EasingFunctionBase easing = null)
        {
            var transform = target.RenderTransform as CompositeTransform;
            if (transform == null)
            {
                transform = new CompositeTransform();
                target.RenderTransform = transform;
            }
            target.RenderTransformOrigin = new Point(0, 0);

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

        public static void MoveForward(this UIElement target, Orientation orientation, double to)
        {
            var currentPosition = orientation == Orientation.Horizontal ? (target.RenderTransform as CompositeTransform).TranslateX : (target.RenderTransform as CompositeTransform).TranslateY;
            target.AnimateTransform("Translate", orientation, null, to, duration: DistancetToDuration(Math.Abs(to - currentPosition)), easing: new SineEase
            {
                EasingMode = EasingMode.EaseInOut
            });
        }

        public static void MakeTurn(this UIElement target, Point to, Direction direction)
        {
            var currentRotation = (target.RenderTransform as CompositeTransform).Rotation;
            target.AnimateTransform("Translate", Orientation.Horizontal, null, to.X, duration: 2000, easing: new QuadraticEase
            {
                EasingMode = EasingMode.EaseOut
            });
            target.AnimateTransform("Translate", Orientation.Vertical, null, to.Y, duration: 2000, easing: new QuadraticEase
            {
                EasingMode = EasingMode.EaseIn
            });
            target.AnimateTransform("Rotation", null, null, direction == Direction.Right ? currentRotation + 90 : currentRotation - 90, duration: 2000, startTime: 0);
        }

        public static void Turn(this UIElement target, Direction direction, Orientation currentOrientation)
        {
            var currentPosition = new Point((target.RenderTransform as CompositeTransform).TranslateX, (target.RenderTransform as CompositeTransform).TranslateY);
            if (direction == Direction.Left)
            {
                if (currentOrientation == Orientation.Horizontal)
                    target.MakeTurn(new Point(currentPosition.X + vehicleLength + 5, currentPosition.Y + 5), direction);
                else
                    target.MakeTurn(new Point(currentPosition.X + 5, currentPosition.Y + vehicleLength + 5), direction);
            }
            else
            {
                if (currentOrientation == Orientation.Horizontal)
                {
                    target.MoveForward(currentOrientation, roadWidth);
                    Task.Delay(DistancetToDuration(roadWidth));
                    target.MakeTurn(new Point(currentPosition.X + vehicleLength + 5, currentPosition.Y + 5), direction);
                }
                else
                {
                    target.MoveForward(currentOrientation, roadWidth);
                    Task.Delay(DistancetToDuration(roadWidth));
                    target.MakeTurn(new Point(currentPosition.X + vehicleLength + 5, currentPosition.Y + 5), direction);
                }
            }
        }

        public static int DistancetToDuration(double distance)
        {
            return Convert.ToInt32(distance / 200 * 1000);
        }

        public enum Direction
        {
            Right,
            Left
        }

    }

}
