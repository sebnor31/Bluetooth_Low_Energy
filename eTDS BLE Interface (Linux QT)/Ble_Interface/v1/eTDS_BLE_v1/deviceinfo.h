#ifndef DEVICEINFO_H
#define DEVICEINFO_H

#include <QBluetoothDeviceInfo>


class deviceInfo
{

public:
    deviceInfo(QBluetoothDeviceInfo _device);
    QString getCoreConfiguration();
    QString getDataCompleteness();
    QString getMajorDeviceClass();
    QString getMinorComputerClass();

private:
    QBluetoothDeviceInfo device;
};

#endif // DEVICEINFO_H
