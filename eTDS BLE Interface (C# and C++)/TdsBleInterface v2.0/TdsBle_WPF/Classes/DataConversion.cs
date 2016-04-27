using System;

static class DataConversion
{
   public static short[] convertSensorData(byte[] sensorData)
   {
      /* Re-construct axis values by concatenating MSB and LSB parts */
      short axisX = (short)((UInt16)(sensorData[1] << 8) | sensorData[0]);
      short axisY = (short)((UInt16)(sensorData[3] << 8) | sensorData[2]);
      short axisZ = (short)((UInt16)(sensorData[5] << 8) | sensorData[4]);

      /* Return an a ushort array composed of each axis (X, Y, Z) */
      return new short[] { axisX, axisY, axisZ };

   }//end method convertSensorData


}

