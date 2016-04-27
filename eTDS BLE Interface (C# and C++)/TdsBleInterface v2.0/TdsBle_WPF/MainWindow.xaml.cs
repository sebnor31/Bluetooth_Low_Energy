using System;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
/*
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
*/

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

/*** Oxyplot Library ***/
using OxyPlot;



namespace TdsBle_WPF
{
   public partial class MainWindow : Window
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
      /* OxyPlot */
      private MainWindowModel viewModel;

      /*** Constructor ***/
      public MainWindow()
      {  // InitializeComponent call is required to merge the UI that is defined in markup with this class,  
         // including setting properties and registering event handlers
         viewModel = new MainWindowModel();
         DataContext = viewModel;

         InitializeComponent();
      }//end constructor

      /*** Event Handler for Form elements ***/
      /* Service checkbox selection changed */
      private void Service_CheckedChanged(object sender, RoutedEventArgs e)
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
      private async void startButton_Click(object sender, RoutedEventArgs e)
      {
         /* Cleanup procedure if an eTdsHeadset object already exist */
         if ( eTdsHeadset != null )
         {
            eTdsHeadset.myService.Dispose(); // Cleanup procedure for Gatt Services
            dataBuffer.Reset();              // Reset buffer state to default values

            // Abort all created threads
            readDataWithNotifThread.Abort();
            displayDataThread.Abort();

         }//end if

         // Clear all points for all series (collection of datapoints)
         viewModel.ClearModel(0);
  
         /* Change Start Button text */
         startButton.Content = "Start New Test";

         /* Initialize eTdsDevice object */
         eTdsHeadset = new eTdsDevice(selectedService, selectedCharacteristic, dataBuffer);
         await eTdsHeadset.InitializationAsync();

         /* Display session info to user */
         displayTextBox.AppendText("\n\n-- Started Test Session --\n");
         displayTextBox.AppendText(string.Format("Device Name: {0}\n", eTdsHeadset.TdsDevice.Name));              // Display the name of the device
         displayTextBox.AppendText(string.Format("Service UUID: {0}\n", eTdsHeadset.myService.Uuid.ToString()));  // Display the service UUID
         displayTextBox.AppendText("Frequency: TBD");

         /* Create and Start Threads */
         readDataWithNotifThread = new Thread(new ThreadStart(eTdsHeadset.GetDataFromNotification));
         readDataWithNotifThread.Priority = ThreadPriority.Highest;
         readDataWithNotifThread.Start();

         displayDataThread = new Thread(new ThreadStart(DisplayData));
         displayDataThread.Priority = ThreadPriority.Lowest;
         displayDataThread.Start();

      }//end event startButton_Click

      private void exitButton_Click(object sender, RoutedEventArgs e)
      {
         if (eTdsHeadset != null)
            eTdsHeadset.myService.Dispose();
         this.Close();
         Application.Current.Shutdown();
         Environment.Exit(0);
         
      }

      /*** Helper Methods ***/
      /* Thread method to display data to chart */
      public void DisplayData()
      {
         while (true)
         {
            displayTextBox.Dispatcher.Invoke(new DisplayFrequencyCallback(DisplayFrequency), dataBuffer.Buffer[4]);

            for (int i = 1; i <= 3; i++)
               viewModel.UpdateModel(i, dataBuffer.Buffer[0], dataBuffer.Buffer[i]);

            sensorChartX.InvalidatePlot(true);
         
            Thread.Sleep(10);
         }

      }//end method DisplayData

      /* Callback method to manipulate UI elements */
      private delegate void DisplayFrequencyCallback(long relativeElapsedTime);

      public void DisplayFrequency(long relativeElapsedTime)
      {
         double frequency = (1000.0 / relativeElapsedTime);
         displayTextBox.Text = displayTextBox.Text.Remove(displayTextBox.Text.LastIndexOf("Frequency"));
         displayTextBox.AppendText(string.Format("Frequency: {0}", (UInt16)frequency));
      }

   }
}
