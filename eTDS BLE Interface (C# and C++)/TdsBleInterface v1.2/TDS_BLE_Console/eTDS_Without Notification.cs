using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace TDS_BLE_Console_WithoutNotification
{
   class TdsBleMain
   {
      static void Main()
      {
         eTdsDevice eTds = new eTdsDevice();

         eTds.InitializeDevice();

         Console.ReadLine();

      }//end Main

   }//end class TdsBleMain

   public class eTdsDevice
   {
      /* Instance variable */
      public int count = 0;

      /* Method */
      public async void InitializeDevice()
      {
         /*** Get a list of devices that match desired service UUID  ***/
         Guid selectedService = new Guid("0000AA10-0000-1000-8000-00805F9B34FB");
         var devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(selectedService));
         
         /*** Create an instance of the eTDS device ***/
         DeviceInformation eTdsDevice = devices[0];         // Only one device should be matching the eTDS-specific service UUID
         Console.WriteLine("Device Name: {0}", eTdsDevice.Name);  // Display the name of the device

         /*** Create an instance of the specified eTDS service ***/
         GattDeviceService myService = await GattDeviceService.FromIdAsync(eTdsDevice.Id);
         Console.WriteLine("Service UUID: {0}", myService.Uuid.ToString());

         /*** Create an instance of the characteristic of the specified service ***/
         Guid selectedCharacteristic = new Guid("0000AA11-0000-1000-8000-00805F9B34FB");
         const int CHARACTERISTIC_INDEX = 0;
         GattCharacteristic myCharacteristic = myService.GetCharacteristics(selectedCharacteristic)[CHARACTERISTIC_INDEX];
         myCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;     // Set security level to "No encryption" 

         /*** Reading 1000 data samples ***/
         GattReadResult bleData;
         byte[] bleDataArray = new byte[6];

         for (int i = 0; i < 10000; i++)
         {
            /* Read data from a buffer */
            bleData = null;
            bleData = await myCharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached);

            DataReader.FromBuffer(bleData.Value).ReadBytes(bleDataArray);

            /* Display each element of raw data */
            count++;
            Console.WriteLine("Value {0}: {1} {2} {3} {4} {5} {6}", count, bleDataArray[0], bleDataArray[1], bleDataArray[2], bleDataArray[3], bleDataArray[4], bleDataArray[5]);

            /* Pause thread execution */
            Thread.Sleep(500);

         }//end for

      }//end method InitializeDevice

   }//end class eTdsDevice
}
