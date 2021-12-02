using Meadow.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer
{
    class AD9833
    {
        private SpiPeripheral Ad9833;
        private ushort initialise = 0x2100; //default settings with sin wave and writing all registers
        private ushort start = 0x2000;
        private double mclk = 25e6;
        private double multiplier = Math.Pow(2, 28);


        public AD9833(ISpiBus bus, IDigitalOutputPort port)
        {
            Ad9833 = new SpiPeripheral(bus, port, 16, 16, ChipSelectMode.ActiveLow);
        }

        private int CalculateFrequency(double frequency)
        {
            int result = 0;
            int mask = 0x0fffffff;
            if (frequency > mclk/2)
            {
                Console.WriteLine("Too high frequency!");
                return 0;
            }
            result = (int)Math.Round(frequency * multiplier / mclk);
            return result;
        }

        public void SetSinFrequency(double frequency)
        {
            ushort mask = 0b0011111111111111;
            ushort phase = 0;
            ushort[] data = new ushort[5];
            data[0] = initialise;
            data[3] = phase;
            data[4] = start;

            int freqReg = CalculateFrequency(frequency);

            data[1] = (ushort)(freqReg & mask);
            data[2] = (ushort)(freqReg >> 14 & mask);

            Ad9833.WriteUShorts(0, data);
        }
    }
}
