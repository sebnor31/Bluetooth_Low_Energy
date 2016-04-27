using System;
using System.Threading.Tasks;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace TDS_BLE_Form
{
   public class eTdsDevice
   {
      /* Instance variable */
      public Guid gattService;
      public Guid gattCharacteristic;
      public GattCharacteristic myCharacteristic;
      public DeviceInformation TdsDevice;
      public GattDeviceService myService;

      /* Constructor */
      public eTdsDevice(Guid service, Guid characterictic)
      {
         gattService = service;
         gattCharacteristic = characterictic;
      }//end constructor

      /* Methods */
      public async Task InitializationAsync()
      {
         /*** Get a list of devices that match desired service UUID  ***/
         var devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(gattService));

         /*** Create an instance of the eTDS device ***/
         TdsDevice = devices[0];         // Only one device should be matching the eTDS-specific service UUID

         /*** Create an instance of the specified eTDS service ***/
         myService = await GattDeviceService.FromIdAsync(TdsDevice.Id);

         /*** Create an instance of the characteristic of the specified service ***/
         const int CHARACTERISTIC_INDEX = 0;
         myCharacteristic = myService.GetCharacteristics(gattCharacteristic)[CHARACTERISTIC_INDEX];
         myCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;     // Set security level to "No encryption" 
      }

   }
}
