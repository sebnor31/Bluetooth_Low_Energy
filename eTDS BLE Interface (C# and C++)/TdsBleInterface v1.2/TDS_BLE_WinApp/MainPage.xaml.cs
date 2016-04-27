using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;

namespace TDS_BLE_WinApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
           int count = 0;

           /*** Get a list of devices that match desired service UUID  ***/
           Guid selectedService = new Guid("0000AA00-0000-1000-8000-00805F9B34FB");
           var devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(selectedService));

           /*** Create an instance of the eTDS device ***/
           DeviceInformation eTdsDevice = devices[0];         // Only one device should be matching the eTDS-specific service UUID
           displayTextBox.Text = string.Format("Device Name: {0}", eTdsDevice.Name); // Display the name of the device

           /*** Create an instance of the specified eTDS service ***/
           GattDeviceService myService = await GattDeviceService.FromIdAsync(eTdsDevice.Id);
           displayTextBox.Text = string.Format("\r\nService UUID: {0}", myService.Uuid.ToString());

           /*** Create an instance of the characteristic of the specified service ***/
           Guid selectedCharacteristic = new Guid("0000AA01-0000-1000-8000-00805F9B34FB");
           const int CHARACTERISTIC_INDEX = 0;
           GattCharacteristic myCharacteristic = myService.GetCharacteristics(selectedCharacteristic)[CHARACTERISTIC_INDEX];
           myCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;     // Set security level to "No encryption" 

           /*** Reading 1000 data samples ***/
           //GattReadResult bleData;
           //byte[] bleDataArray = new byte[6];

//           for (int i = 0; i < 1000; i++)
//           {
//              /* Read data from a buffer */
//              bleData = await myCharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached);
//              DataReader.FromBuffer(bleData.Value).ReadBytes(bleDataArray);

//              /* Display each element of raw data */
//              count++;
//              displayTextBox.Text = string.Format("\r\nValue {0}: {1} {2} {3} {4} {5} {6}", count, bleDataArray[0], bleDataArray[1], bleDataArray[2], bleDataArray[3], bleDataArray[4], bleDataArray[5]);

//              /* Pause thread execution */
////              await Task.Delay(TimeSpan.FromMilliseconds(100));
//           }

           /*** Create an event handler when the characteristic value changes ***/
           myCharacteristic.ValueChanged += myCharacteristic_ValueChanged;
           await myCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);        
        }

       // New characteristic received
       void myCharacteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
       {
          //this.SetText(string.Format("count: {0}", count));
          //count++;
          /* Create an array to hold sensor data */
          byte[] bleDataArray = new byte[args.CharacteristicValue.Length];

          DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(bleDataArray);
          displayTextBox.Text = string.Format("\r\nValue");

       }//end event myCharacteristic_ValueChanged

    }
}
