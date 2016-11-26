using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace Medical_tp.Tool
{
    class DiagramPlot
    {
        public PlotModel model { get; private set; }

        private LineSeries lineSeries;

        private int horizontalAvancement;
        private List<int> points;

        public DiagramPlot(string title, OxyColor color)
        {
            model = new PlotModel { Title = title };

            horizontalAvancement = 0;
            points = new List<int>();
            points.Add(0);

            lineSeries = new LineSeries();
            lineSeries.Color = color;
            
            model.Series.Add(lineSeries);

        }

        public void updateValues(int newValue)
        {
            if (newValue != points[points.Count - 1])
            {
                points.Add(newValue);
                lineSeries.Points.Add(new DataPoint(horizontalAvancement++, newValue));
                model.InvalidatePlot(true);

            }
            
        }
    }
}
