﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Shapes;

namespace Utilities
{
    public class PointD
    {
        public double X;// { get; set; }
        public double Y;// { get; set; }
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    
    public class Point3D
    {
        public double X;// { get; set; }
        public double Y;// { get; set; }
        public double Z;// { get; set; }
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class PolarPoint
    {
        public double Distance;
        public double Angle;

        public PolarPoint(double distance, double angle)
        {
            Distance = distance;
            Angle = angle;
        }
    }

    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Theta { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }
        public double Vtheta { get; set; }

        public Location(double x, double y, double theta, double vx, double vy, double vtheta)
        {
            X = x;
            Y = y;
            Theta = theta;
            Vx = vx;
            Vy = vy;
            Vtheta = vtheta;
        }
    }

    public class PolygonExtended
    {
        public Polygon polygon = new Polygon();
        public float borderWidth = 1;
        public System.Drawing.Color borderColor = System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
        public double borderOpacity = 1;
        public double[] borderDashPattern = new double[] { 1.0 };
        public System.Drawing.Color backgroundColor = System.Drawing.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
    }

    public class PolarPointListExtended
    {
        public List<PolarPoint> PolarPointList;        
        public System.Drawing.Color DisplayColor;
        public double DisplayWidth;

        public PolarPointListExtended(List<PolarPoint> polarPointList, System.Drawing.Color displayColor, double displayWidth)
        {
            PolarPointList = polarPointList;
            DisplayColor = displayColor;
            DisplayWidth = displayWidth;
        }
    }
}
