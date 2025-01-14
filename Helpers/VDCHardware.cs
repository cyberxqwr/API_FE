using Paslauga.Entities;

namespace Paslauga.Helpers
{

    public enum HardwareType
    {
        HDD = 0,
        CPU = 1,
        RAM = 2
    }
    public static class VDCHardware
    {

        public static int? LeftAvailable(VDC vDC, HardwareType hT)
        {
            if (vDC == null) { return -1; }

            switch (hT)
            {
                case HardwareType.CPU:
                    return vDC.VCPUMax - vDC.VCPUAllocated;
                case HardwareType.RAM:
                    return vDC.VMemoryMax - vDC.VMemoryAllocated;
                case HardwareType.HDD:
                    return vDC.VStorageMax - vDC.VStorageUsed;
                default: return -2;
            }
        }

    }
}
