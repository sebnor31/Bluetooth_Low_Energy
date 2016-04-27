using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.Diagnostics;

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
   private Stopwatch stopWatch = new Stopwatch();
   private long elapsedTimeMilliseconds = 0;

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

      /*** Reset Stop Watch ***/
      stopWatch.Reset();

   }//end method InitializationAsync

   public async void GetDataFromNotification()
   {
      /*** Create an event handler when the characteristic value changes ***/
      myCharacteristic.ValueChanged += myCharacteristic_ValueChanged;
      await myCharacteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

      /*** Start Stop Watch ***/
      stopWatch.Start();

   }//end method GetDataGetDataFromNotification

   private void myCharacteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
   {
      /* Save the elapsed time before manipulating data */
      long prevElpasedTime = elapsedTimeMilliseconds;
      elapsedTimeMilliseconds = stopWatch.ElapsedMilliseconds;
      
      /* Create an Array to Hold Raw Sensor Data and Read from the Characteristics Value */
      byte[] sensorData = new byte[args.CharacteristicValue.Length];
      DataReader.FromBuffer(args.CharacteristicValue).ReadBytes(sensorData);

      /* Convert Raw Data (6 bytes) into 3-Axis Values (X,Y,Z) */
      short[] convertedData = DataConversion.convertSensorData(sensorData);

      /* Write to Circular Buffer the datapoint (Time, X, Y, Z) */
      sensorValue.Buffer = new long[] { elapsedTimeMilliseconds, convertedData[0], convertedData[1], convertedData[2], (elapsedTimeMilliseconds - prevElpasedTime) };

   }//end method myCharacteristic_ValueChanged

}//end class eTdsDevice

