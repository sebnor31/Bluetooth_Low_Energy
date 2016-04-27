#ifndef MAINDIALOG_H
#define MAINDIALOG_H

#include <QDialog>
#include <QPushButton>
#include <QTreeWidget>
#include <QList>

#include "ui_maindialog.h"
#include "deviceinfo.h"

#include <QtBluetooth/QBluetoothDeviceDiscoveryAgent>
#include <QLowEnergyController>



class mainDialog : public QDialog , public Ui::mainDialog
{
    Q_OBJECT

public:
    explicit mainDialog(QWidget *parent = 0);
    void displayCharact();
    QString getCharactValue(QByteArray value);

private slots:
    void addDevice(const QBluetoothDeviceInfo &device);
    void disableScan();
    void enableScan();
    void deviceSelected(QTreeWidgetItem* selectedDevice);
    void deviceConnected();
    void serviceDiscovered(const QBluetoothUuid & newService);
    void serviceDiscoveryFinished();
    void serviceSelected(QTreeWidgetItem* selectedService);
    void serviceStateChanged(QLowEnergyService::ServiceState state);
    void newValueNotif(QLowEnergyCharacteristic charact, QByteArray value);

private:
    QPushButton *scanButton;
    QPushButton *closeButton;
    QList<QBluetoothDeviceInfo> devicesQList;   // List of BLE devices
    QList<QLowEnergyService*> servicesQList;   // List of services for a selected device

    QLowEnergyController *deviceControl;
    QLowEnergyService *g_service;
    QList<QLowEnergyCharacteristic> charactList;

};

#endif // MAINDIALOG_H
