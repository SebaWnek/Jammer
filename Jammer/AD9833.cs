using Meadow.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer
{
    class AD9833
    {
        private SpiPeripheral Ad9833;

        public AD9833(ISpiBus bus, IDigitalOutputPort port)
        {
            Ad9833 = new SpiPeripheral(bus, port, 16, 16, ChipSelectMode.ActiveLow);
        }
    }
}
