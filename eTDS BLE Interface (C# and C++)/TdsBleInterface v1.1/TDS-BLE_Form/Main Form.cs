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
   CircularBuffer dataBuffer = new CircularBuffer();

   /*** Constructor ***/
   public TdsBleForm()
   {
      InitializeComponent();
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

   // Main button has been clicked on 
   // "async" is required due to asynchronous method calls
   private async void mainButton_Click(object sender, EventArgs e)
   {      
      displayTextBox.AppendText("\r\n\r\n-- Started Test Session --\r\n");

      /*** Clear Chart ***/
      foreach (var series in sensorChart.Series)
         series.Points.Clear();

      /*** Initialize eTDS object ***/
      eTdsDevice eTdsHeadset = new eTdsDevice(selectedService, selectedCharacteristic, dataBuffer);
      await eTdsHeadset.InitializationAsync();

      displayTextBox.AppendText(string.Format("Device Name: {0} \r\n", eTdsHeadset.TdsDevice.Name));              // Display the name of the device
      displayTextBox.AppendText(string.Format("Service UUID: {0} \r\n", eTdsHeadset.myService.Uuid.ToString()));  // Display the service UUID

      //Thread readDataManualThread = new Thread(new ThreadStart(eTdsHeadset.GetData));
      //readDataManualThread.Start();

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
         Thread.Sleep(10);
      }
      
   }

   // This delegate enables asynchronous calls for setting 
   // the text property on a TextBox control. 
   delegate void SetGraphCallback(short[] sensorValue);

   private void SetGraph(short[] sensorValue)
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
         this.sensorChart.Series["X-Axis"].Points.AddY(sensorValue[0]);
         this.sensorChart.Series["Y-Axis"].Points.AddY(sensorValue[1]);
         this.sensorChart.Series["Z-Axis"].Points.AddY(sensorValue[2]);
      }
   }//end method SetGraph

}//end class TdsBleForm

