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
   UInt32 count = 0;


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
      eTdsDevice eTdsHeadset = new eTdsDevice(selectedService, selectedCharacteristic);
      await eTdsHeadset.InitializationAsync();
     
      displayTextBox.AppendText(string.Format("Device Name: {0} \r\n", eTdsHeadset.TdsDevice.Name));              // Display the name of the device
      displayTextBox.AppendText(string.Format("Service UUID: {0} \r\n", eTdsHeadset.myService.Uuid.ToString()));  // Display the service UUID

      /*** Read Data ***/
      GattReadResult dataRaw;
      byte[] dataArray = new byte[6];

      for (int i = 0; i < 10000; i++)
      {
         /* Read data from buffer */
         dataRaw = null;
         dataRaw = await eTdsHeadset.myCharacteristic.ReadValueAsync(BluetoothCacheMode.Cached);
         DataReader.FromBuffer(dataRaw.Value).ReadBytes(dataArray);

         /* Convert raw data into meaningful values (3-D axis) */
         short[] sensorValue = convertSensorData(dataArray);

         /* Display and graph axis values */
         count++;
//         this.SetText(string.Format("Sensor Data Readout\r\n\r\nX-Axis: {0}\r\nY-Axis: {1}\r\nZ-Axis: {2}\r\nCount: {3}", sensorValue[0], sensorValue[1], sensorValue[2], count));
         this.SetGraph(sensorValue);

         /* Pause thread execution */
         Thread.Sleep(10);
      }

      /*** Create an event handler when the characteristic value changes ***/
      //eTdsHeadset.myCharacteristic.ValueChanged += myCharacteristic_ValueChanged;
      //await eTdsHeadset.myCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
      
   }//end event mainButton_Click

   // New characteristic received
   //void myCharacteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
   //{
   //   //this.SetText(string.Format("count: {0}", count));
   //   //count++;
   //   /* Create an array to hold sensor data */
   //   byte[] sensorData = new byte[args.CharacteristicValue.Length];

   //   Windows.Storage.Streams.DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(sensorData);

   //   short[] sensorValue = convertSensorData(sensorData);

   //   this.SetText(string.Format("Sensor Data Readout\r\n\r\nX-Axis: {0}\r\nY-Axis: {1}\r\nZ-Axis: {2}", sensorValue[0], sensorValue[1], sensorValue[2]));

   //   this.SetGraph(sensorValue);

   //}//end event myCharacteristic_ValueChanged


   /*** Helper Methods ***/
   private short[] convertSensorData(byte[] sensorData)
   {
      /* Re-construct axis values by concatenating MSB and LSB parts */
      short axisX = (short)((UInt16)(sensorData[1] << 8) | sensorData[0]);
      short axisY = (short)((UInt16)(sensorData[3] << 8) | sensorData[2]);
      short axisZ = (short)((UInt16)(sensorData[5] << 8) | sensorData[4]);

      /* Return an a ushort array composed of each axis (X, Y, Z) */
      return new short[] { axisX, axisY, axisZ };

   }//end method convertSensorData


   /*** Asynchronous calls handler ***/
   // Delegate methods
   delegate void SetTextCallback(string text);
   delegate void SetGraphCallback(short[] sensorValue);

   // Methods to perform actions on controls
   private void SetText(string text)
   {
      // InvokeRequired required compares the thread ID of the 
      // calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (this.sensorReadout.InvokeRequired)
      {
         SetTextCallback d = new SetTextCallback(SetText);
         this.Invoke(d, new object[] { text });
      }
      else
      {
        this.sensorReadout.Text = text;
      }
   }//end method SetText

   private void SetGraph(short[] sensorValue)
   {
      // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread. 
      // If these threads are different, it returns true. 
      if (this.sensorChart.InvokeRequired)
      {
         SetGraphCallback d = new SetGraphCallback(SetGraph);
         this.Invoke(d, new object[] { sensorValue });
      }
      else
      {
         this.sensorChart.Series["X-Axis"].Points.AddY(sensorValue[0]);
         this.sensorChart.Series["Y-Axis"].Points.AddY(sensorValue[1]);
         this.sensorChart.Series["Z-Axis"].Points.AddY(sensorValue[2]);
      }
   }//end method SetGraph

}//end class TdsBleForm

