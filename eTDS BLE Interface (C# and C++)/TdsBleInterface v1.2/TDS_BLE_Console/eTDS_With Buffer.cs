using System;
using System.Threading.Tasks;

namespace TDS_BLE_Console_WithBuffer
{
   class TdsBleMain
   {
      static void Main()
      {
         TDS_BLE_Console.CircularBuffer buffer = new TDS_BLE_Console.CircularBuffer(5);

         TDS_BLE_Console.eTdsDevice eTds = new TDS_BLE_Console.eTdsDevice(buffer);

         eTds.InitializeDevice();

         Console.ReadLine();
      }
   }
}
