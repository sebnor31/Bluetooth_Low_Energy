using System.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlotDemo.Annotations;


public class MainWindowModel : INotifyPropertyChanged
{
   // PlotModel represents a plot
   private PlotModel plotModel1;

   public PlotModel PlotModel1
   {
      get { return plotModel1; }
      set { plotModel1 = value; OnPropertyChanged("PlotModel"); }
   }

   /* Constructor */
   public MainWindowModel()
   {
      PlotModel1 = new PlotModel();
      SetUpModel();
   }//end constructor

   /* Set up the Graph */
   private void SetUpModel()
   {
      PlotModel1.Title = "Magnetometer";

      // Create the X-Axis and add it to the plot
      var magnetoAxisX = new LinearAxis(AxisPosition.Bottom, "Time (s)")
      {
         MajorGridlineColor = OxyColor.FromArgb(40, 0, 0, 139),
         MajorGridlineStyle = LineStyle.Solid,
         MinorGridlineColor = OxyColor.FromArgb(20, 0, 0, 139),
         MinorGridlineStyle = LineStyle.Solid,
         MaximumPadding = 0.1
      };
      PlotModel1.Axes.Add(magnetoAxisX);

      // Create the Y-Axis and add it to the plot
      var magnetoAxisY = new LinearAxis(AxisPosition.Left, "Value")
      {
         MajorGridlineColor = OxyColor.FromArgb(40, 0, 0, 139),
         MajorGridlineStyle = LineStyle.Solid,
         MinorGridlineColor = OxyColor.FromArgb(20, 0, 0, 139),
         MinorGridlineStyle = LineStyle.Solid,
        MaximumPadding = 0.1,
        MinimumPadding = 0.1
      };
      PlotModel1.Axes.Add(magnetoAxisY);

      // Create a Line graph and add it to the plot
      var lineSerieX = new LineSeries
      {
         StrokeThickness = 2,
         MinimumSegmentLength = 3,
         Color = OxyColors.Blue,
         Smooth = false,
      };
      PlotModel1.Series.Add(lineSerieX);

      var lineSerieY = new LineSeries
      {
         StrokeThickness = 2,
         MinimumSegmentLength = 3,
         Color = OxyColors.Red,
         Smooth = false,
      };
      PlotModel1.Series.Add(lineSerieY);

      var lineSerieZ = new LineSeries
      {
         StrokeThickness = 2,
         MinimumSegmentLength = 3,
         Color = OxyColors.ForestGreen,
         Smooth = false,
      };
      PlotModel1.Series.Add(lineSerieZ);

   }//end method SetUpModel

   /* Update the Graph with a new point */
   public void UpdateModel(int select, double x, double y)
   {
      // Unit conversion from Millisecond to Seconds
      x = (x / 1000.0);

      var series = new LineSeries();

      if (select == 1)
      {
         series = (LineSeries)PlotModel1.Series[0];
         series.Points.Add(new DataPoint(x, y));
      }

      else if (select == 2)
      {
         y += 5000.0;
         series = (LineSeries)PlotModel1.Series[1];
         series.Points.Add(new DataPoint(x, y));
      }

      else
      {
         y -= 5000.0;
         series = (LineSeries)PlotModel1.Series[2];
         series.Points.Add(new DataPoint(x, y));
      }
         
      // Remove the first point to maintain history at 1 minute worth of data (based on freq rate of 100 Hz)  
      if (series.Points.Count > 1000)
         series.Points.RemoveAt(0);

   }//end method UpdateModel

   /* Clear Graph */
   public void ClearModel(int select)
   {
      foreach (LineSeries item in PlotModel1.Series)
         item.Points.Clear();
   }

   /**** CAN IT BE REMOVED ***/
   public event PropertyChangedEventHandler PropertyChanged;

   [NotifyPropertyChangedInvocator]
   protected virtual void OnPropertyChanged(string propertyName)
   {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) 
         handler(this, new PropertyChangedEventArgs(propertyName));
   } 

}

