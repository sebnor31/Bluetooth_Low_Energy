using System;
using Windows.Storage.Streams;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

public class eTdsInterface
{
   /*----------------- 
    Instance Variables 
   ------------------*/
   DeviceInformation eTdsDevice;
   GattDeviceService myService;
   GattCharacteristic myCharacteristic;
   Guid selectedService;
   Guid selectedCharacteristic;
   GattReadResult bleData;
   byte[] bleDataArray;


   /*----------------- 
       Properties 
  ------------------*/
   public byte[] BleDataArray
   {
      get { return bleDataArray; }
      set { bleDataArray = value; }
   }

   public string DeviceName
   {
      get { return string.Format( "{0} - {1}", eTdsDevice.Name, myService.Uuid.ToString() ) ; }
   }


   /*----------------- 
       Constructor 
     ------------------*/
   public eTdsInterface(int NumEltDataArray, string serviceUUID, string characteristicUUID){
      bleDataArray = new byte[NumEltDataArray];    // Define the array size based on user input
      selectedService = new Guid(string.Format("0000{0}-0000-1000-8000-00805F9B34FB", serviceUUID));     
      selectedCharacteristic = new Guid(string.Format("0000{0}-0000-1000-8000-00805F9B34FB", characteristicUUID));
   }


   /*----------------- 
         Methods 
    ------------------*/
   public async void InitializeDevice()
   {
      /*** Get a list of devices that match desired service UUID  ***/
      var devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(selectedService));

      /*** Create an instance of the eTDS device ***/
      eTdsDevice = devices[0];         // Only one device should be matching the eTDS-specific service UUID, hence [0]

      /*** Create an instance of the specified eTDS service ***/
      myService = await GattDeviceService.FromIdAsync(eTdsDevice.Id);

      /*** Create an instance of the characteristic of the specified service ***/
      myCharacteristic = myService.GetCharacteristics(selectedCharacteristic)[0];
      myCharacteristic.ProtectionLevel = GattProtectionLevel.Plain;     // Set security level to "No encryption" 

   }// end method InitializeDevice


   public async void ReadData()
   {
      /*** Read data from selected characteristic ****/
      bleData = await myCharacteristic.ReadValueAsync(BluetoothCacheMode.Uncached); // Read using uncached mode
      DataReader.FromBuffer(bleData.Value).ReadBytes(bleDataArray);                 // save data into bleDataArray

   }//end method ReadData 
 

}//end class eTdsDevice

