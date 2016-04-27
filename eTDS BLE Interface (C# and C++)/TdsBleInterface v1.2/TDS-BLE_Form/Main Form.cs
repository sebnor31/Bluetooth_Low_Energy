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
   /*** GATT Profile variables ***/
   Guid selectedService = eTdsServices.MagneticSensor;                  // Selected Gatt Service
   Guid selectedCharacteristic = eTdsCharacteristics.MagneticSensor1;   // Selected Gatt Characteristic
   eTdsDevice eTdsHeadset;
   CircularBuffer dataBuffer = new CircularBuffer();
   UInt16 axisView;

   /*** Constructor ***/
   public TdsBleForm()
   {
      InitializeComponent();

      // Format Charts
      axisView = 20;
      this.sensorChart.ChartAreas[0].AxisX.ScaleView.Size = axisView;
      this.sensorChart.ChartAreas[1].AxisX.ScaleView.Size = axisView;
      this.sensorChart.ChartAreas[2].AxisX.ScaleView.Size = axisView;

      

   }//end constructor


   /*** Event Handler ***/
   // Service checkbox selection changed
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

   // Main button has been clicked on ("async" required due to async method calls)
   private async void mainButton_Click(object sender, EventArgs e)
   {      
      
      /*** Clear Chart ***/
      foreach (var series in sensorChart.Series)
         series.Points.Clear();

      /*** Initialize eTDS object ***/
      eTdsHeadset = new eTdsDevice(selectedService, selectedCharacteristic, dataBuffer);
      await eTdsHeadset.InitializationAsync();

      /*** Display session info to user ***/
      displayTextBox.AppendText("\r\n\r\n-- Started Test Session --\r\n");
      displayTextBox.AppendText(string.Format("Device Name: {0} \r\n", eTdsHeadset.TdsDevice.Name));              // Display the name of the device
      displayTextBox.AppendText(string.Format("Service UUID: {0} \r\n", eTdsHeadset.myService.Uuid.ToString()));  // Display the service UUID

      Thread readDataWithNotifThread = new Thread(new ThreadStart(eTdsHeadset.GetDataFromNotification));
      readDataWithNotifThread.Priority = ThreadPriority.Highest;
      readDataWithNotifThread.Start();

      Thread displayDataThread = new Thread(new ThreadStart(DisplayData));
      displayDataThread.Priority = ThreadPriority.Lowest;
      displayDataThread.Start();

   }//end event mainButton_Click

   public void DisplayData()
   {
      while(true)
      {
         this.SetGraph(dataBuffer.Buffer);
         Thread.Sleep(20);
      }
      
   }

   // This delegate enables asynchronous calls for setting 
   // the text property on a TextBox control. 
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

         if (this.sensorChart.ChartAreas[0].AxisX.ScrollBar.IsVisible)
         {
            this.sensorChart.ChartAreas[0].AxisX.ScaleView.Position = this.sensorChart.ChartAreas[0].AxisX.Maximum - (0.9 * axisView);
            this.sensorChart.ChartAreas[1].AxisX.ScaleView.Position = this.sensorChart.ChartAreas[1].AxisX.Maximum - (0.9 * axisView);
            this.sensorChart.ChartAreas[2].AxisX.ScaleView.Position = this.sensorChart.ChartAreas[2].AxisX.Maximum - (0.9 * axisView);
         }
        
      }
   }//end method SetGraph

   private void exitButton_Click(object sender, EventArgs e)
   {
      sensorChart.Dispose();
      eTdsHeadset.myService.Dispose();
      TdsBleForm.ActiveForm.Close();
      Environment.Exit(0);

   }//end method exitButton_Click

}//end class TdsBleForm

