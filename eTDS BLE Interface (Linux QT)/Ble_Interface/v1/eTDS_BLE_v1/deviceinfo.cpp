#include "deviceinfo.h"

deviceInfo::deviceInfo(QBluetoothDeviceInfo _device)
{
    device = _device;
}


QString deviceInfo::getCoreConfiguration()
{
    if(device.coreConfigurations() & QBluetoothDeviceInfo::UnknownCoreConfiguration)
        return "Unknown Core Configuration";

    else if (device.coreConfigurations()& QBluetoothDeviceInfo::LowEnergyCoreConfiguration)
        return "BLE";

    else if (device.coreConfigurations() & QBluetoothDeviceInfo::BaseRateCoreConfiguration)
        return "BT Standard";

    else if (device.coreConfigurations() & QBluetoothDeviceInfo::BaseRateAndLowEnergyCoreConfiguration)
        return "BT Smart Ready";

     else
        return "Error: No core config found";
}


QString deviceInfo::getDataCompleteness()
{
    if (device.serviceUuidsCompleteness() & QBluetoothDeviceInfo::DataComplete)
        return "Data Complete";
    else if(device.serviceUuidsCompleteness() & QBluetoothDeviceInfo::DataIncomplete)
        return "Data Incomplete";
    else if(device.serviceUuidsCompleteness() & QBluetoothDeviceInfo::DataUnavailable)
        return "Data Unavailable";
    else
        return "Error: Data Completeness not found";

}


QString deviceInfo::getMajorDeviceClass()
{
    if (device.majorDeviceClass() & QBluetoothDeviceInfo::MiscellaneousDevice)
        return "Miscellaneous Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::ComputerDevice)
        return "Computer Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::PhoneDevice)
        return "Phone Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::LANAccessDevice)
        return "LAN Access Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::AudioVideoDevice)
        return "Audio Video Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::PeripheralDevice)
        return "Peripheral Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::ImagingDevice)
        return "Imaging Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::WearableDevice)
        return "Wearable Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::ToyDevice)
        return "Toy Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::HealthDevice)
        return "Health Device";
    else if(device.majorDeviceClass() & QBluetoothDeviceInfo::UncategorizedDevice)
        return "Uncategorized Device";
    else
        return "Error: Major Class not found";
}


QString deviceInfo::getMinorComputerClass()
{
    if (device.minorDeviceClass() & QBluetoothDeviceInfo::UncategorizedComputer)
        return "Uncategorized Computer";
    else if(device.minorDeviceClass() & QBluetoothDeviceInfo::DesktopComputer)
        return "Desktop Computer";
    else if(device.minorDeviceClass() & QBluetoothDeviceInfo::ServerComputer)
        return "Server Computer";
    else if(device.minorDeviceClass() & QBluetoothDeviceInfo::LaptopComputer)
        return "Laptop Computer";
    else if(device.minorDeviceClass() & QBluetoothDeviceInfo::HandheldClamShellComputer)
        return "Handheld PDA";
    else if(device.minorDeviceClass() & QBluetoothDeviceInfo::HandheldComputer)
        return "Handheld Computer";
    else if(device.minorDeviceClass() & QBluetoothDeviceInfo::WearableComputer)
        return "Wearable Computer";
    else
        return "Error: Minor Class not found";
}
