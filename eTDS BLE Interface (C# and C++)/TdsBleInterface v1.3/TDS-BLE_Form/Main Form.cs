using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage.Streams;
using TDS_BLE_Form;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;


public partial class TdsBleForm : Form
{
   /*** Instance Variables ***/
   /* GATT Profile variables */
   Guid selectedService = eTdsServices.MagneticSensor;                  // Selected Gatt Service from check box 
   Guid selectedCharacteristic = eTdsCharacteristics.MagneticSensor1;   // Gatt Characteristic associated to the selected service
   eTdsDevice eTdsHeadset;                                              // Instantiate an eTdsDevice object
   /* Threads */
   Thread readDataWithNotifThread;                                      // Thread to read data through a notification event handler
   Thread displayDataThread;                                            // Thread to display data on a chart
   /* Other variables */  
   CircularBuffer dataBuffer = new CircularBuffer();                    // Instantiate a circular buffer
   const UInt16 axisView = 20;                                          // Set the width of X-axis for all chart areas


   /*** Constructor ***/
   public TdsBleForm()
   {
      InitializeComponent();
   }//end constructor


   /*** Event Handler for Form elements ***/
   /* Service checkbox selection changed */
   private void Service_CheckedChanged(object sender, EventArgs e)
   {
      if (sender == accelRadio)
      {
         selectedService = eTdsServices.Accelerometer;
         selectedCharacteristic = eTdsCharacteristics.Accelerometer;
      }
      else if (sender == mpuMagnetoRadio)
      {
         selectedService = eTdsServices.MpuMagnetometer;
         selectedCharacteristic = eTdsCharacteristics.MpuMagnetometer;
      }

      else if (sender == mpuGyroRadio)
      {
         selectedService = eTdsServices.MpuGyroscope;
         selectedCharacteristic = eTdsCharacteristics.MpuGyroscope;
      }

      else if (sender == sensorMagn1Radio)
      {
         selectedService = eTdsServices.MagneticSensor;
         selectedCharacteristic = eTdsCharacteristics.MagneticSensor1;
      }

      else if (sender == sensorMagn2Radio)
      {
         selectedService = eTdsServices.MagneticSensor;
         selectedCharacteristic = eTdsCharacteristics.MagneticSensor2;
      }

      else if (sender == sensorMagn3Radio)
      {
         selectedService = eTdsServices.MagneticSensor;
         selectedCharacteristic = eTdsCharacteristics.MagneticSensor3;
      }

      else
      {
         selectedService = eTdsServices.MagneticSensor;
         selectedCharacteristic = eTdsCharacteristics.MagneticSensor4;
      }

   }//end event Service_CheckedChanged

   /* Main button has been clicked on */
   // ("async" required due to async method calls) 
   private async void mainButton_Click(object sender, EventArgs e)
   {
      /* Cleanup procedure if an eTdsHeadset object already exist */
      if (eTdsHeadset != null)
      {
         eTdsHeadset.myService.Dispose(); // Cleanup procedure for Gatt Services
         dataBuffer.Reset();              // Reset buffer state to default values

         // Abort all created threads
         readDataWithNotifThread.Abort();
         displayDataThread.Abort();

      }//end if

      /* Format Chart */
      // Clear all points for all series (collection of datapoints)
      foreach (var series in sensorChart.Series)
         series.Points.Clear();

      // Set the width and initial value of the X-axis
      foreach (var chart in sensorChart.ChartAreas)
      {
         chart.AxisX.ScaleView.Size = axisView; // Set width of X-axis to a constant value
         chart.AxisX.ScaleView.Position = 0;    // Set initial position to 0
      }
                 
      /* Change Start Button text */
      StartButton.Text = "Start New Test";

      /* Initialize eTdsDevice object */
      eTdsHeadset = new eTdsDevice(selectedService, selectedCharacteristic, dataBuffer);
      await eTdsHeadset.InitializationAsync();

      /* Display session info to user */
      displayTextBox.AppendText("\r\n\r\n-- Started Test Session --\r\n");
      displayTextBox.AppendText(string.Format("Device Name: {0} \r\n", eTdsHeadset.TdsDevice.Name));              // Display the name of the device
      displayTextBox.AppendText(string.Format("Service UUID: {0} \r\n", eTdsHeadset.myService.Uuid.ToString()));  // Display the service UUID
      displayTextBox.AppendText("Frequency: TBD");
      /* Create and Start Threads */
      readDataWithNotifThread = new Thread(new ThreadStart(eTdsHeadset.GetDataFromNotification));
      readDataWithNotifThread.Priority = ThreadPriority.Highest;
      readDataWithNotifThread.Start();

      displayDataThread = new Thread(new ThreadStart(DisplayData));
      displayDataThread.Priority = ThreadPriority.Lowest;
      displayDataThread.Start();

   }//end event mainButton_Click

   /* Exit button has been clicked on */
   private void exitButton_Click(object sender, EventArgs e)
   {
      sensorChart.Dispose();
      if (eTdsHeadset != null)
         eTdsHeadset.myService.Dispose();
      TdsBleForm.ActiveForm.Close();
      Environment.Exit(0);

   }//end method exitButton_Click


   /*** Helper Methods ***/
   /* Thread method to display data to chart */
   public void DisplayData()
   {
      while(true)
      {
         this.SetGraph(dataBuffer.Buffer);
         this.DisplayFrequency(dataBuffer.Buffer[4]);
         Thread.Sleep(1);
      }
      
   }//end method DisplayData

   /* Callback method to manipulate the UI controls */
   delegate void SetGraphCallback(long[] sensorValue);
   private void SetGraph(long[] sensorValue)
   {
      // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (this.sensorChart.InvokeRequired)
      {
         SetGraphCallback d = new SetGraphCallback(SetGraph);
         this.Invoke(d, sensorValue);
      }
      else
      {
         // A cast is needed to convert the stored elapsed time from a long to a double type
         double timeInSeconds = ((double)sensorValue[0]) / 1000;

         // Add the new datapoints to the charts
         this.sensorChart.Series["X-Axis"].Points.AddXY(timeInSeconds, sensorValue[1]);
         this.sensorChart.Series["Y-Axis"].Points.AddXY(timeInSeconds, sensorValue[2]);
         this.sensorChart.Series["Z-Axis"].Points.AddXY(timeInSeconds, sensorValue[3]);

         // Keep only a minute-worth of data history (based on 10 Hz) 
         foreach (var series in this.sensorChart.Series)
         {
            if (series.Points.Count > 600)
               series.Points.RemoveAt(0);
         }//end foreach

         // Generate an auto-scrolling for all charts
         if (this.sensorChart.ChartAreas[0].AxisX.ScrollBar.IsVisible)
         {
            this.sensorChart.ChartAreas[0].AxisX.ScaleView.Position = this.sensorChart.ChartAreas[0].AxisX.Maximum - (0.9 * axisView);
            this.sensorChart.ChartAreas[1].AxisX.ScaleView.Position = this.sensorChart.ChartAreas[1].AxisX.Maximum - (0.9 * axisView);
            this.sensorChart.ChartAreas[2].AxisX.ScaleView.Position = this.sensorChart.ChartAreas[2].AxisX.Maximum - (0.9 * axisView);
         }//end if
        
      }//end else

   }//end method SetGraph

   delegate void DisplayFrequencyCallback(long relativeElapsedTime);
   private void DisplayFrequency(long relativeElapsedTime)
   {
      if (this.displayTextBox.InvokeRequired)
      {
         DisplayFrequencyCallback display = new DisplayFrequencyCallback(DisplayFrequency);
         this.Invoke(display, relativeElapsedTime);
      }
      else
      {
         double frequency = (1000.0 / relativeElapsedTime);         
         displayTextBox.Text = displayTextBox.Text.Remove(displayTextBox.Text.LastIndexOf("Frequency"));
         displayTextBox.AppendText(string.Format("Frequency: {0}", (UInt16)frequency));
      }
   }//end method SetDisplayTextBox

}//end class TdsBleForm

