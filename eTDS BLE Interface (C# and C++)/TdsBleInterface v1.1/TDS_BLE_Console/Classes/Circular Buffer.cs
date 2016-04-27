using System;

namespace TDS_BLE_Console
{
   public class CircularBuffer
   {
      /* Instance variable */
      private int bufferLength;
      public short[][] buffer;
//      public short[][] buffer = new short[5][];  // Buffer length is 5 and composed of short arrays
      private int occupiedBufferCount = 0;   // maintains count of occupied buffers
      private int readLocation = 0;          // location of the next read
      private int writeLocation = 0;          // location of the next write

      /* Constructor */
      public CircularBuffer(int buffLength)
      {
         bufferLength = buffLength;

         buffer = new short[bufferLength][];

         for (int i = 0; i < this.buffer.Length; i++)
            buffer[i] = new short[] { 0, 0, 0 }; // Initialize each buffer element to [0,0,0]
      }

   }
}//end namespace


