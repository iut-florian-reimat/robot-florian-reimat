using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    
    class Odometry
    {
        public static double actualDiameterFactor;
        public static double actualDistanceFactor;

        public static uint NUMBER_OF_POINTS = 8192;
        public static double WHEEL_DIAMETER = 0.0426;
        public static double WHEEL_DISTANCE = 0.218;
        
        public Odometry()
        {
            actualDiameterFactor = Math.PI * WHEEL_DIAMETER / NUMBER_OF_POINTS;
            actualDistanceFactor = WHEEL_DISTANCE;
        }

        public Odometry(double diameter, double distance)
        {
            actualDiameterFactor = diameter;
            actualDistanceFactor = distance;
        }

        public void CorrectDiameter(double expected_x, double measure_x)
        {
            actualDiameterFactor *= expected_x / measure_x;

            OnUpdateOdometry();
        }

        public void CorrectDistance(double expecte_angle, double measure_angle)
        {

            actualDistanceFactor *= measure_angle / expecte_angle;

            OnUpdateOdometry();
        }

        public virtual void OnUpdateOdometry()
        {
            OnUpdateOdometryEvent?.Invoke(this, new OdometryArgs(actualDiameterFactor, actualDistanceFactor));
        }

        public event EventHandler<OdometryArgs> OnUpdateOdometryEvent;
    }

    public class OdometryArgs : EventArgs
    {
        public double diameterFactor { get; set; }
        public double distanceFactor { get; set; }

        public OdometryArgs(double diam, double dist)
        {
            diameterFactor = diam;
            distanceFactor = dist;
        }
    }
}
