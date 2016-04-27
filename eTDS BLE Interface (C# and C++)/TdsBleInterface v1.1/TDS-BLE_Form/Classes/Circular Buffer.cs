using System;
using System.Threading;

namespace TDS_BLE_Form
{
   public class CircularBuffer
   {
      /* Instance variable */
      private short[][] buffers = new short[10][];  // Buffer length is 5 and composed of short arrays
      private int occupiedBufferCount = 0;   // maintains count of occupied buffers
      private int readLocation = 0;          // location of the next read
      private int writeLocation = 0;          // location of the next write

      /* Constructor */
      public CircularBuffer()
      {
         for (int i = 0; i < this.buffers.Length; i++)
            buffers[i] = new short[] { 0, 0, 0 }; // Initialize each buffer element to [0,0,0]
      }//end constructor

      /* Property */
      public short[] Buffer
      {
         get
         {
            /* Lock this object while getting value from buffers array */
            lock (this)
            {
               if (occupiedBufferCount == 0)
               {
                  Console.WriteLine("Entered GET wait: {0}", occupiedBufferCount);
                  Monitor.Wait(this);  // Enter WaitSleepJoin state. Lock is also released
               }

               // Obtain value at current readLocation
               short[] readValue = buffers[readLocation];

               // Just consumed a value, so decrement number of occupied buffers
               --occupiedBufferCount;
               Console.WriteLine("Occupied Buffer (GET): {0}", occupiedBufferCount);

               // Update readLocation for future read operations, then add current state to output
               readLocation = (readLocation + 1) % buffers.Length;

               // Return waiting thread (if there is one) to Running state
               Monitor.Pulse(this);

               // Return the read value
               return readValue; 

            }//end lock

         }//end get

         set
         {
            // Lock this object while setting value in buffers array
            lock (this)
            {
               // If there are no empty locations, place invoking thread in WaitSleepJoin state
               if (occupiedBufferCount == buffers.Length)
               {
                  Console.WriteLine("Entered SET wait: {0}", occupiedBufferCount);
                  Monitor.Wait(this);
               }

               else
               {
                  // Place value in writeLocation of buffers
                  buffers[writeLocation] = value;

                  // Just produced a value, so increment number of occupied buffers
                  ++occupiedBufferCount;
                  Console.WriteLine("Occupied Buffer (SET): {0}", occupiedBufferCount);

                  // Update writeLocation for future write operations, then add current state to output
                  writeLocation = (writeLocation + 1) % buffers.Length;

                  // Return waiting thread (if there is one) to Running state
                  Monitor.Pulse(this);
               }
               
            }//end lock

         }//end set

      }//end property Buffer

   }//end class CircularBuffer

}//end namespace

