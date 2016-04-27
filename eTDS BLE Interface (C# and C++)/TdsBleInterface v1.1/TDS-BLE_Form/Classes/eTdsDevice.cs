using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;

/*** Specific Namespaces for BLE Interface ***/
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

/*** Specific Namespace for this project ***/
using TDS_BLE_Form;

public class eTdsDevice
{
   /* Instance variable */
   public Guid gattService;
   public Guid gattCharacteristic;
   public GattCharacteristic myCharacteristic;
   public DeviceInformation TdsDevice;
   public GattDeviceService myService;
   public CircularBuffer sensorValue;

   /* Constructor */
   public eTdsDevice(Guid service, Guid characterictic, CircularBuffer sensorBuffer)
   {
      gattService = service;
      gattCharacteristic = characterictic;
      sensorValue = sensorBuffer;
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

   }//end method InitializationAsync

   public async void GetData()
   {
      /*** Read Data ***/
      GattReadResult dataRaw;
      byte[] dataArray = new byte[6];

      for (int i = 0; i < 10000; i++)
      {
         /* Read data from buffer */
         dataRaw = null;
         dataRaw = await myCharacteristic.ReadValueAsync(BluetoothCacheMode.Cached);
         DataReader.FromBuffer(dataRaw.Value).ReadBytes(dataArray);

         /* Convert raw data into meaningful values (3-D axis) */
         sensorValue.Buffer = DataConversion.convertSensorData(dataArray);
         
         /* Pause thread execution */
         Thread.Sleep(10);

      }//end for

   }//end method GetData

   public async void GetDataFromNotification()
   {
      /*** Create an event handler when the characteristic value changes ***/
      myCharacteristic.ValueChanged += myCharacteristic_ValueChanged;
      await myCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
   }//end method GetDataGetDataFromNotification

   private void myCharacteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
   {
      /* Create an array to hold sensor data */
      byte[] sensorData = new byte[args.CharacteristicValue.Length];

      DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(sensorData);

      sensorValue.Buffer = DataConversion.convertSensorData(sensorData);

   }//end method myCharacteristic_ValueChanged

}//end class eTdsDevice

