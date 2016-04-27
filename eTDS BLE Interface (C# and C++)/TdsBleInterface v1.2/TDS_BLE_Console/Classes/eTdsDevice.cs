using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;


namespace TDS_BLE_Console
{
   class eTdsDevice
   {
      /* Instance variable */
      public int count = 0;
      public DeviceInformation TdsDevice;
      GattDeviceService myService;
      GattCharacteristic myCharacteristic;
      CircularBuffer buffer; 

      /* Constructor */
      public eTdsDevice(CircularBuffer _buffer)
      {
         buffer = _buffer;
      }//end constructor

      /* Method */
      public async void InitializeDevice()
      {
         /*** Get a list of devices that match desired service UUID  ***/
         Guid selectedService = new Guid("0000AA10-0000-1000-8000-00805F9B34FB");
         var devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(selectedService));

         /*** Create an instance of the eTDS device ***/
         TdsDevice = devices[0];         // Only one device should be matching the eTDS-specific service UUID
         Console.WriteLine("Device Name: {0}", TdsDevice.Name);  // Display the name of the device

         /*** Create an instance of the specified eTDS service ***/
         myService = await GattDeviceService.FromIdAsync(TdsDevice.Id);
         Console.WriteLine("Service UUID: {0}", myService.Uuid.ToString());

         /*** Create an instance of the characteristic of the specified service ***/
         Guid selectedCharacteristic = new Guid("0000AA11-0000-1000-8000-00805F9B34FB");
         const int CHARACTERISTIC_INDEX = 0;
         myCharacteristic = myService.GetCharacteristics(selectedCharacteristic)[CHARACTERISTIC_INDEX];
         myCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;     // Set security level to "No encryption" 

         /*** Create an event handler when the characteristic value changes ***/
         myCharacteristic.ValueChanged -= myCharacteristic_ValueChanged;
         GattCommunicationStatus disableNotifStatus = await myCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.None);
         Console.WriteLine("Disable Notification Status: {0}", disableNotifStatus);

         myCharacteristic.ValueChanged += this.myCharacteristic_ValueChanged;
         GattCommunicationStatus enableNotifStatus = await myCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
         Console.WriteLine("Enable Notification Status: {0}", disableNotifStatus);

      }//end method InitializeDevice

      // New characteristic received
      void myCharacteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
      {
         /* Create an array to hold sensor data */
         byte[] sensorData = new byte[args.CharacteristicValue.Length];

         DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(sensorData);

         /* Display each element of raw data */
         count++;
         Console.WriteLine("Value {0}: {1} {2} {3} {4} {5} {6}", count, sensorData[0], sensorData[1], sensorData[2], sensorData[3], sensorData[4], sensorData[5]);

      }//end event myCharacteristic_ValueChanged  

   }//end class eTdsDevice

}//end Namespace
