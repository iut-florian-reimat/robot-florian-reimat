using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.RenderableSeries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utilities;

namespace WpfOscilloscopeControl
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class WpfOscilloscope : UserControl
    {
        Dictionary<int, XyDataSeries<double, double> > lineDictionary = new Dictionary<int, XyDataSeries<double, double>>();

        public WpfOscilloscope()
        {
            InitializeComponent();
        }
        public void ClearLine(int id, string lineName)
        {
            if (lineDictionary.ContainsKey(id))
            {
                lineDictionary[id].Clear();
            }
        }
        public void AddOrUpdateLine(int id, int maxNumberOfPoints, string lineName)
        {
            if (lineDictionary.ContainsKey(id))
            {
                lineDictionary[id] = new XyDataSeries<double, double>(maxNumberOfPoints) { SeriesName = lineName };
            }
            else
            {
                lineDictionary.Add(id, new XyDataSeries<double, double>(maxNumberOfPoints) { SeriesName = lineName });
           
                var lineRenderableSerie = new FastLineRenderableSeries();
                lineRenderableSerie.Name = "lineRenderableSerie"+id.ToString();
                lineRenderableSerie.DataSeries = lineDictionary[id];
                lineRenderableSerie.DataSeries.AcceptsUnsortedData = true;

                //Ajout de la ligne dans le scichart
                sciChart.RenderableSeries.Add(lineRenderableSerie);
            }            
        }

        public void RemoveLine(int id)
        {
            if (lineDictionary.ContainsKey(id))
            {                
                sciChart.RenderableSeries.Remove(sciChart.RenderableSeries.Single(x => x.DataSeries == lineDictionary[id]));
                lineDictionary.Remove(id);
            }
        }

        public bool LineExist(int id)
        {
            if (lineDictionary.ContainsKey(id))
                return true;
            return false;
        }


        public void SetTitle(string title)
        {
            titleText.Text = title;
        }
        public void SetSerieName(int serieID, string name)
        {
            if(lineDictionary.ContainsKey(serieID))
                lineDictionary[serieID].SeriesName = name;
        }

        public void ChangeLineColor(string lineName, Color color)
        {
            sciChart.RenderableSeries.Single(x => x.DataSeries.SeriesName == lineName).Stroke = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public void ChangeLineColor(int serieID, Color color)
        {
            sciChart.RenderableSeries.Single(x => x.DataSeries.SeriesName == lineDictionary[serieID].SeriesName).Stroke = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public void AddPointToLine(int lineId, double x, double y)
        {
            if (LineExist(lineId))
            {
                lineDictionary[lineId].Append(x, y);
                if (lineDictionary[lineId].Count > lineDictionary[lineId].Capacity)
                    lineDictionary[lineId].RemoveAt(0);
            }
        }

        public void AddPointToLine(int lineId, PointD point)
        {
            lineDictionary[lineId].Append(point.X, point.Y);
            if (lineDictionary[lineId].Count > lineDictionary[lineId].Capacity)
                lineDictionary[lineId].RemoveAt(0);
        }

        public void AddPointListToLine(int lineId, List<PointD> pointList)
        {
            lineDictionary[lineId].Append( pointList.Select(e=>e.X).ToList(), pointList.Select(e2=> e2.Y).ToList());
            if (lineDictionary[lineId].Count > lineDictionary[lineId].Capacity)
                lineDictionary[lineId].RemoveAt(0);
        }
    }
}
