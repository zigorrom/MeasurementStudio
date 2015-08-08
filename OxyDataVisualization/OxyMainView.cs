using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxyDataVisualization
{

    public class OxyMainView
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ExpressionToDisplay { get; set; }
        public List<List<DataPoint>> source { get; set; }

        public OxyMainView()
        {
            Title = "title";
            Subtitle = "subtitle";
            source = new List<List<DataPoint>>();
            for (int i = 0; i < 3; i++)
            {
                var a = new List<DataPoint>();
                
                for (int j = 0; j < 5; j++)
                {
                    a.Add(new DataPoint(i, i * j));
                }
                source.Add(a);
            }
        }
    }
}
