using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAccel
{
    public partial class MainPage : ContentPage
    {
        IDeviceMotion motion = CrossDeviceMotion.Current;

        double accelX;
        double accelY;
        double accelZ;
        int changeCount = 0;
        const int averageNum = 5;
 
        public MainPage()
        {
            InitializeComponent( );
        }

        private void StartClicked(object sender, EventArgs e)
        {
            motion.Start( MotionSensorType.Accelerometer, MotionSensorDelay.Default );
            if( motion.IsActive(MotionSensorType.Accelerometer))
            {
                motion.SensorValueChanged += ( object s, SensorValueChangedEventArgs a ) =>
                {
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        accelX += ( (MotionVector)a.Value ).X;
                        accelY += ( (MotionVector)a.Value ).Y;
                        accelZ += ( (MotionVector)a.Value ).Z;
                        changeCount++;

                        if( changeCount == averageNum )
                        {
                            xLabel.Text = "x = " + (accelX / averageNum).ToString( "0.000" );
                            yLabel.Text = "y = " + (accelY / averageNum).ToString( "0.000" );
                            zLabel.Text = "z = " + (accelZ / averageNum).ToString( "0.000" );

                            accelX = 0;
                            accelY = 0;
                            accelZ = 0;
                            changeCount = 0;
                        }
                    } );
                };
            }
        }

        private void StopClicked( object sender, EventArgs e )
        {
            motion.Stop( MotionSensorType.Accelerometer );
        }
    }
}
