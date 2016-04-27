using System;
using System.Threading;


public class CircularBuffer
{
   /* Instance variable */
   private long[][] buffers = new long[10][];  // Buffer length is 5 and composed of short arrays
   private UInt16 occupiedBufferCount = 0;   // maintains count of occupied buffers
   private int readLocation = 0;          // location of the next read
   private int writeLocation = 0;          // location of the next write

   /* Constructor */
   public CircularBuffer()
   {
      this.Reset();
   }//end constructor

   /* Property */
   public long[] Buffer
   {
      get
      {
         /* Lock this object while getting value from buffers array */
         lock (this)
         {
            Console.WriteLine("Occuppied Buffer: {0}", occupiedBufferCount);
            if(occupiedBufferCount == 0)
            {
               Monitor.Wait(this);  // Enter WaitSleepJoin state. Lock is also released
            }

            // Obtain value at current readLocation
            long[] readValue = buffers[readLocation];

            // Just consumed a value, so decrement number of occupied buffers
            --occupiedBufferCount;

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
               Monitor.Wait(this);
            }

            // Place value in writeLocation of buffers
            buffers[writeLocation] = value;

            // Just produced a value, so increment number of occupied buffers
            ++occupiedBufferCount;

            // Update writeLocation for future write operations, then add current state to output
            writeLocation = (writeLocation + 1) % buffers.Length;

            // Return waiting thread (if there is one) to Running state
            Monitor.Pulse(this);
              
         }//end lock

      }//end set

   }//end property Buffer

   /* Methods */
   public void Reset()
   {
      occupiedBufferCount = 0;
      readLocation = 0;
      writeLocation = 0;

      for (int i = 0; i < this.buffers.Length; i++)
         buffers[i] = new long[] { 0, 0, 0, 0, 0 }; ; // Initialize each buffer element to [0,0,0,0,0]

   }//end method reset

}//end class CircularBuffer


