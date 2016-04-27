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
    void deviceDiscoveryStarted();
    void deviceDiscoveryFinished();
    void deviceSelected(QTreeWidgetItem* selectedDevice);
    void deviceConnected();
    void serviceDiscovered(const QBluetoothUuid & newService);
    void serviceDiscoveryFinished();
    void serviceSelected(QTreeWidgetItem* selectedService);
    void serviceStateChanged(QLowEnergyService::ServiceState state);
    void newValueNotif(QLowEnergyCharacteristic charact, QByteArray value);

private:
    // Buttons
    QPushButton *scanButton;
    QPushButton *closeButton;

    // BLE Device
    QBluetoothDeviceDiscoveryAgent *deviceDiscoverAgent = NULL;
    QLowEnergyController *deviceControl = NULL;
    QList<QBluetoothDeviceInfo> devicesQList;   // List of BLE devices

    // BLE Services
    QList<QLowEnergyService*> servicesQList;   // List of services for a selected device
    QLowEnergyService *g_service = NULL;

    // BLE Characteristics
    QList<QLowEnergyCharacteristic> charactList;

    // Graph

public :
    int count,firstrun;
    int forplottingmagenticdata;
    int sensor;


//nischal


};

#endif // MAINDIALOG_H
