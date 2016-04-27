#include "maindialog.h"


mainDialog::mainDialog(QWidget *parent) : QDialog(parent)

{
    setupUi(this);

    /* Set Buttons */
    scanButton = buttonBox->button(QDialogButtonBox::Ok);
    closeButton = buttonBox->button(QDialogButtonBox::Close);
    scanButton->setText("&Scan");

    /* Set Label */
    connectionLabel->setText("Click on Scan button to begin...");

    /* Set Characteristic Tree Widget */
    QStringList charactTreeHeader;
    charactTreeHeader << "Name" << "Value";
    characteristicTree->setHeaderLabels(charactTreeHeader);

    /* Set BTDeviceDiscoveryAgent */
    QBluetoothDeviceDiscoveryAgent *deviceDiscoverAgent = new QBluetoothDeviceDiscoveryAgent(this);
    connect(deviceDiscoverAgent,SIGNAL(deviceDiscovered(QBluetoothDeviceInfo)), this, SLOT(addDevice(QBluetoothDeviceInfo)));

    /* Set all connections */
    connect(scanButton,SIGNAL(clicked()),deviceDiscoverAgent,SLOT(start()));
    connect(scanButton,SIGNAL(clicked()),this,SLOT(disableScan()));
    connect(deviceDiscoverAgent, SIGNAL(finished()), this, SLOT(enableScan()));

    connect(devicesTree, SIGNAL(itemDoubleClicked(QTreeWidgetItem*,int)), this, SLOT(deviceSelected(QTreeWidgetItem*)));
    connect(servicesTree, SIGNAL(itemDoubleClicked(QTreeWidgetItem*,int)), this, SLOT(serviceSelected(QTreeWidgetItem*)));

    connect(closeButton,SIGNAL(clicked()),this,SLOT(close()));

}

/* ==========================================================================
 *                              Slots
 * ==========================================================================
 */

void mainDialog::addDevice(const QBluetoothDeviceInfo &device)
{
    deviceInfo d(device);

//    Show only BLE device
//    if (device.coreConfigurations() & QBluetoothDeviceInfo::LowEnergyCoreConfiguration)
//    { }

        /* Add device to top level of TreeList */
        QTreeWidgetItem *parentItem = new QTreeWidgetItem(devicesTree);
        parentItem->setText(0, device.address().toString());
        parentItem->setText(1, QString::number(device.rssi()) );
        parentItem->setText(2, device.name());

        QTreeWidgetItem *childItemType = new QTreeWidgetItem(parentItem);
        childItemType->setText(2, "Type: " + d.getCoreConfiguration());

        QTreeWidgetItem *childItemData = new QTreeWidgetItem(parentItem);
        childItemData->setText(2, d.getDataCompleteness());

        QTreeWidgetItem *childItemMajor = new QTreeWidgetItem(parentItem);
        childItemMajor->setText(2, d.getMajorDeviceClass());

        QTreeWidgetItem *childItemMinor = new QTreeWidgetItem(parentItem);
        childItemMinor->setText(2, d.getMinorComputerClass());

        parentItem->addChild(childItemType);
        parentItem->addChild(childItemData);
        parentItem->addChild(childItemMajor);
        parentItem->addChild(childItemMinor);
        devicesTree->addTopLevelItem(parentItem);

        /* Add device to devicesQList */
        devicesQList.append(device);

}//end slot addDevice


void mainDialog::deviceSelected(QTreeWidgetItem* selectedDevice)
{
    // Clear services and characteristics lists and tree widgets
    servicesTree->clear();
    servicesQList.clear();
    characteristicTree->clear();
    charactList.clear();

    // Create a LE Controller that handles LE communication
    QBluetoothDeviceInfo deviceSelected = devicesQList[devicesTree->indexOfTopLevelItem(selectedDevice)];
    deviceControl = new QLowEnergyController(deviceSelected.address(), this);

    connect(deviceControl, SIGNAL(connected()), this, SLOT(deviceConnected()) );

    if(deviceControl->state() == QLowEnergyController::UnconnectedState)
    {
        connectionLabel->setText("Device Unconnected");
        deviceControl->connectToDevice();
    }
    else
        connectionLabel->setText("Device Connected");
}//end slot deviceSelected


void mainDialog::deviceConnected()
{
    connectionLabel->setText("Discovering Services...");
    connect(deviceControl, SIGNAL(serviceDiscovered(QBluetoothUuid)), this, SLOT(serviceDiscovered(QBluetoothUuid))) ;
    connect(deviceControl, SIGNAL(discoveryFinished()), this, SLOT(serviceDiscoveryFinished()));
    deviceControl->discoverServices();
}//end slot deviceConnected


void mainDialog::serviceDiscovered(const QBluetoothUuid &newService)
{
    QTreeWidgetItem *parentItem = new QTreeWidgetItem(servicesTree);
    QTreeWidgetItem *childItem = new QTreeWidgetItem(parentItem);

    QLowEnergyService *service = deviceControl->createServiceObject(newService);

    parentItem->setText(0, service->serviceName());
    childItem->setText(0,service->serviceUuid().toString());

    parentItem->addChild(childItem);
    servicesTree->addTopLevelItem(parentItem);

    /* Add service to servicesQList */
   servicesQList.append(service);

}//end slot serviceDiscovered


void mainDialog::serviceDiscoveryFinished()
{
    connectionLabel->setText("Service Discovery Finished!");
}//end slot serviceDiscoveryFinished


void mainDialog::serviceSelected(QTreeWidgetItem* selectedService)
{
    // Remove previous items on characteristic tree and list
    characteristicTree->clear();
    charactList.clear();

    // Set global service to the selected (double-clicked) service
    g_service = servicesQList[servicesTree->indexOfTopLevelItem(selectedService)];
    connectionLabel->setText("Service selected: " + g_service->serviceName());

    // Get all the details (characteristics, descriptors, etc.) of the service
    connect(g_service, SIGNAL(stateChanged(QLowEnergyService::ServiceState)), this, SLOT(serviceStateChanged(QLowEnergyService::ServiceState)));
    connect(g_service, SIGNAL(characteristicChanged(QLowEnergyCharacteristic,QByteArray)), this, SLOT(newValueNotif(QLowEnergyCharacteristic,QByteArray)));
    g_service->discoverDetails();

}//end slot serviceSelected


void mainDialog::serviceStateChanged(QLowEnergyService::ServiceState state)
{
    switch(state){

        case QLowEnergyService::ServiceDiscovered:
        {
            connectionLabel->setText("Characteristic found !");
            charactList = g_service->characteristics();
            displayCharact();
            break;
        }

        case QLowEnergyService::DiscoveringServices:
        {         
            connectionLabel->setText("Discovering Services...");
            break;
        }

        case QLowEnergyService::InvalidService:
        {
            connectionLabel->setText("Invalid Service !!!");
            break;
        }

        case QLowEnergyService::DiscoveryRequired:
        {
            connectionLabel->setText("Discovery Required!!!");
            break;
        }

        default:
        {
            connectionLabel->setText("No Characteristic found...");
            break;
        }

    }//end switch

}//end slot serviceStateChanged


void mainDialog::newValueNotif(QLowEnergyCharacteristic charact, QByteArray value)
{
    for(int i = 0; i < charactList.length(); i++)
    {
        if(charactList[i].uuid() == charact.uuid())
        {
            characteristicTree->topLevelItem(i)->setText(1, getCharactValue(value));
            break;
        }
    }
}//end slot newValueNotif


void mainDialog::disableScan()
{
    scanButton->setEnabled(false);
}//end slot disableScan


void mainDialog::enableScan()
{
    scanButton->setEnabled(true);
}//end slot enableScan


/* ==========================================================================
 *                              Helper Methods
 * ==========================================================================
 */

void mainDialog::displayCharact()
{
    for(int i = 0; i < charactList.length(); i++)
    {
        QLowEnergyCharacteristic charact = charactList[i];
        QLowEnergyDescriptor charUserDescriptor = charact.descriptor(QBluetoothUuid::CharacteristicUserDescription);


        QTreeWidgetItem *parentItem = new QTreeWidgetItem(characteristicTree);
        parentItem->setText(0, QString::fromStdString(charUserDescriptor.value().toStdString()));
        parentItem->setText(1, getCharactValue(charact.value()));

        QTreeWidgetItem *childClientConfig = new QTreeWidgetItem(parentItem);
        childClientConfig->setText(0, "UUID: ");
        childClientConfig->setText(1, charact.uuid().toString());

        parentItem->addChild(childClientConfig);
        characteristicTree->addTopLevelItem(parentItem);

        // Enable Notification
        QLowEnergyDescriptor charClientConfig = charact.descriptor(QBluetoothUuid::ClientCharacteristicConfiguration);
        if(charClientConfig.isValid())
        {
            g_service->writeDescriptor(charClientConfig, QByteArray::fromHex("0100"));
        }

    }

}//end method displayCharact

QString mainDialog::getCharactValue(QByteArray value)
{
    QByteArray rawData = value.toHex();
    QString result = "";

    for (int i = 0; i < rawData.size(); i++)
    {
        result += rawData.at(i);

        if ( (i  == 3) || (i==7))
            result += " : ";
    }


    return result;

}//end method getCharactValue
