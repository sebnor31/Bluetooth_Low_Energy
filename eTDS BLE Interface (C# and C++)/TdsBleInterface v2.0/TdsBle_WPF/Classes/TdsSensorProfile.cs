/* These static classes enumerates all services and characteristics implemented on the eTDS BLE */
using System;

static public class eTdsServices
{  /*** eTDS BLE Service ***/
   static public Guid Accelerometer   = new Guid("0000AA00-0000-1000-8000-00805F9B34FB");
   static public Guid MagneticSensor = new Guid("0000AA10-0000-1000-8000-00805F9B34FB");
   static public Guid MpuGyroscope = new Guid("0000AA20-0000-1000-8000-00805F9B34FB");
   static public Guid MpuMagnetometer = new Guid("0000AA30-0000-1000-8000-00805F9B34FB");
}

static public class eTdsCharacteristics
{  /*** eTDS BLE Characteristics ***/

   // Accelerometer
   static public Guid Accelerometer = new Guid("0000AA01-0000-1000-8000-00805F9B34FB");

   // Magnetic Sensors
   static public Guid MagneticSensor1 = new Guid("0000AA11-0000-1000-8000-00805F9B34FB");
   static public Guid MagneticSensor2 = new Guid("0000AA12-0000-1000-8000-00805F9B34FB");
   static public Guid MagneticSensor3 = new Guid("0000AA13-0000-1000-8000-00805F9B34FB");
   static public Guid MagneticSensor4 = new Guid("0000AA13-0000-1000-8000-00805F9B34FB");

   // MPU
   static public Guid MpuGyroscope = new Guid("0000AA21-0000-1000-8000-00805F9B34FB");
   static public Guid MpuMagnetometer = new Guid("0000AA31-0000-1000-8000-00805F9B34FB");
}



