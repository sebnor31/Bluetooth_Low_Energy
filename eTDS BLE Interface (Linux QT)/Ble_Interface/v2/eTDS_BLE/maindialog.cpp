#include "maindialog.h"

QCPGraph *mainGraph1;
QCPGraph *mainGraph2;
QCPGraph *mainGraph3;
QCPGraph *mainGraph4;
QCPGraph *mainGraph5;
QCPGraph *mainGraph6;
QCPGraph *mainGraph7;
QCPGraph *mainGraph8;
QCPGraph *mainGraph9;
QCPGraph *mainGraph10;
QCPGraph *mainGraph11;
QCPGraph *mainGraph12;


QCPAxisRect   *wideAxisRect;
QCPAxisRect   *wideAxisRect1;
QCPAxisRect   *wideAxisRect2;
QCPAxisRect   *wideAxisRect3;
QCPAxisRect   *wideAxisRect4;
QCPAxisRect   *wideAxisRect5;
QCPAxisRect   *wideAxisRect6;
QCPAxisRect   *wideAxisRect7;
QCPAxisRect   *wideAxisRect8;
QCPAxisRect   *wideAxisRect9;
QCPAxisRect   *wideAxisRect10;
QCPAxisRect   *wideAxisRect11;

QCPMarginGroup *marginGroup;
QCPMarginGroup *marginGroup1;
QCPMarginGroup *marginGroup2;
QCPMarginGroup *marginGroup3;
QCPMarginGroup *marginGroup4;
QCPMarginGroup *marginGroup5;
QCPMarginGroup *marginGroup6;
QCPMarginGroup *marginGroup7;
QCPMarginGroup *marginGroup8;
QCPMarginGroup *marginGroup9;
QCPMarginGroup *marginGroup10;
QCPMarginGroup *marginGroup11;

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

    /* Set Graph */

    /* Set BTDeviceDiscoveryAgent */    
    connect(scanButton,SIGNAL(clicked()), this, SLOT(deviceDiscoveryStarted()));

    /* Set connections related to double clicking a tree item */
    connect(devicesTree, SIGNAL(itemDoubleClicked(QTreeWidgetItem*,int)), this, SLOT(deviceSelected(QTreeWidgetItem*)));
    connect(servicesTree, SIGNAL(itemDoubleClicked(QTreeWidgetItem*,int)), this, SLOT(serviceSelected(QTreeWidgetItem*)));

    /* Set connections related to clicking on Close button*/
    connect(closeButton,SIGNAL(clicked()),this,SLOT(close()));

}

/* ==========================================================================
 *                              Slots
 * ==========================================================================
 */

void mainDialog::deviceDiscoveryStarted()
{
    /* Cleaning procedures before starting a new scan */

    // Clean trees and lists from any previous scan
    characteristicTree->clear();
    charactList.clear();
    servicesTree->clear();
    servicesQList.clear();
    devicesTree->clear();
    devicesQList.erase(devicesQList.begin() , devicesQList.end());

    /* Start new scan */
    connectionLabel->setText("Device discovery in progress...");

    // Create a new agent or stop discovery of exisiting one
    if (deviceDiscoverAgent == NULL)
        deviceDiscoverAgent = new QBluetoothDeviceDiscoveryAgent(this);


    // Start new discovery
    deviceDiscoverAgent->start();
    connect(deviceDiscoverAgent, SIGNAL(finished()), this, SLOT(deviceDiscoveryFinished()));

    // Disable Scan button
    scanButton->setEnabled(false);

}//end slot deviceDiscoveryStarted


void mainDialog::deviceDiscoveryFinished()
{
    deviceDiscoverAgent->stop();

    scanButton->setEnabled(true);
    connectionLabel->setText("Device discovery completed!");

    devicesQList = deviceDiscoverAgent->discoveredDevices();

    for (int i = 0; i < devicesQList.size(); i++)
    {
        QBluetoothDeviceInfo device = devicesQList[i];
        deviceInfo d(device);

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
    }

}//end slot deviceDiscoveryFinished


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


    //nischal

    plot->plotLayout()->clear();  //clear layout for a new data plotting
    QBluetoothUuid UUID_mag = g_service->serviceUuid();
    QString forcomparion = UUID_mag.toString();
    QString magneticUUID = "{0000aa10-0000-1000-8000-00805f9b34fb}" ;


    if (forcomparion == magneticUUID)
    {
        forplottingmagenticdata = 1;
        sensor=0;
        wideAxisRect = new QCPAxisRect(plot);
        wideAxisRect1 = new QCPAxisRect(plot);
        wideAxisRect2 = new QCPAxisRect(plot);
        wideAxisRect3 = new QCPAxisRect(plot);
        wideAxisRect4 = new QCPAxisRect(plot);
        wideAxisRect5 = new QCPAxisRect(plot);
        wideAxisRect6= new QCPAxisRect(plot);
        wideAxisRect7 = new QCPAxisRect(plot);
        wideAxisRect8 = new QCPAxisRect(plot);
        wideAxisRect9 = new QCPAxisRect(plot);
        wideAxisRect10 = new QCPAxisRect(plot);
        wideAxisRect11 = new QCPAxisRect(plot);

        marginGroup = new QCPMarginGroup(plot);
        marginGroup1 = new QCPMarginGroup(plot);
        marginGroup2 = new QCPMarginGroup(plot);
        marginGroup3 = new QCPMarginGroup(plot);
        marginGroup4 = new QCPMarginGroup(plot);
        marginGroup5 = new QCPMarginGroup(plot);
        marginGroup6 = new QCPMarginGroup(plot);
        marginGroup7 = new QCPMarginGroup(plot);
        marginGroup8 = new QCPMarginGroup(plot);
        marginGroup9 = new QCPMarginGroup(plot);
        marginGroup10 = new QCPMarginGroup(plot);
        marginGroup11 = new QCPMarginGroup(plot);

        wideAxisRect ->setMaximumSize(1000, 500);
        wideAxisRect1->setMaximumSize(1000, 500);
        wideAxisRect2->setMaximumSize(1000, 500);
        wideAxisRect3->setMaximumSize(1000, 500);
        wideAxisRect4->setMaximumSize(1000, 500);
        wideAxisRect5->setMaximumSize(1000, 500);
        wideAxisRect6->setMaximumSize(1000, 500);
        wideAxisRect7->setMaximumSize(1000, 500);
        wideAxisRect8->setMaximumSize(1000, 500);
        wideAxisRect9->setMaximumSize(1000, 500);
        wideAxisRect10->setMaximumSize(1000, 500);
        wideAxisRect11->setMaximumSize(1000, 500);

        wideAxisRect->setMinimumSize(1000, 500);
        wideAxisRect1->setMinimumSize(1000, 500);
        wideAxisRect2->setMinimumSize(1000, 500);
        wideAxisRect3->setMinimumSize(1000, 500);
        wideAxisRect4->setMinimumSize(1000, 500);
        wideAxisRect5->setMinimumSize(1000, 500);
        wideAxisRect6->setMinimumSize(1000, 500);
        wideAxisRect7->setMinimumSize(1000, 500);
        wideAxisRect8->setMinimumSize(1000, 500);
        wideAxisRect9->setMinimumSize(1000, 500);
        wideAxisRect10->setMinimumSize(1000, 500);
        wideAxisRect11->setMinimumSize(1000, 500);

        wideAxisRect->addAxis(QCPAxis::atLeft)->setTickLabelColor(QColor("#6050F8")); // add an extra axis on the left and color its numbers
        plot->plotLayout()->addElement(0, 0, wideAxisRect); // insert axis rect in first row
        plot->plotLayout()->addElement(1, 1, wideAxisRect1); // sub layout in second row (grid layout will grow accordingly)
        plot->plotLayout()->addElement(2, 2, wideAxisRect2);
        plot->plotLayout()->addElement(0, 3, wideAxisRect3); // insert axis rect in first row
        plot->plotLayout()->addElement(1, 0, wideAxisRect4); // sub layout in second row (grid layout will grow accordingly)
        plot->plotLayout()->addElement(2, 1, wideAxisRect5);
        plot->plotLayout()->addElement(0, 2, wideAxisRect6); // insert axis rect in first row
        plot->plotLayout()->addElement(1, 3, wideAxisRect7); // sub layout in second row (grid layout will grow accordingly)
        plot->plotLayout()->addElement(2, 0, wideAxisRect8);
        plot->plotLayout()->addElement(0, 1, wideAxisRect9); // insert axis rect in first row
        plot->plotLayout()->addElement(1, 2, wideAxisRect10); // sub layout in second row (grid layout will grow accordingly)
        plot->plotLayout()->addElement(2, 3, wideAxisRect11);

        plot->plotLayout()->setRowStretchFactor(1, 1);


        wideAxisRect->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup);
        wideAxisRect1->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup1);
        wideAxisRect2->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup2);
        wideAxisRect3->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup3);
        wideAxisRect4->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup4);
        wideAxisRect5->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup5);
        wideAxisRect6->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup6);
        wideAxisRect7->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup7);
        wideAxisRect8->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup8);
        wideAxisRect9->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup9);
        wideAxisRect10->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup10);
        wideAxisRect11->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup11);

        // move newly created axes on "axes" layer and grids on "grid" layer:
        foreach (QCPAxisRect *rect, plot->axisRects())
        {
          foreach (QCPAxis *axis, rect->axes())
          {
            axis->setLayer("axes");
            axis->grid()->setLayer("grid");
          }
        }

        mainGraph1 = plot->addGraph(wideAxisRect->axis(QCPAxis::atTop), wideAxisRect->axis(QCPAxis::atLeft));
        mainGraph1->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph1->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph2 = plot->addGraph(wideAxisRect1->axis(QCPAxis::atTop), wideAxisRect1->axis(QCPAxis::atLeft));
        mainGraph2->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph2->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph3 = plot->addGraph(wideAxisRect2->axis(QCPAxis::atTop), wideAxisRect2->axis(QCPAxis::atLeft));
        mainGraph3->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph3->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph4 = plot->addGraph(wideAxisRect3->axis(QCPAxis::atTop), wideAxisRect3->axis(QCPAxis::atLeft));
        mainGraph4->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph4->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph5 = plot->addGraph(wideAxisRect4->axis(QCPAxis::atTop), wideAxisRect4->axis(QCPAxis::atLeft));
        mainGraph5->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph5->setPen(QPen(QColor(120, 120, 120), 2));


        mainGraph6 = plot->addGraph(wideAxisRect5->axis(QCPAxis::atTop), wideAxisRect5->axis(QCPAxis::atLeft));
        mainGraph6->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph6->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph7 = plot->addGraph(wideAxisRect6->axis(QCPAxis::atTop), wideAxisRect6->axis(QCPAxis::atLeft));
        mainGraph7->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph7->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph8 = plot->addGraph(wideAxisRect7->axis(QCPAxis::atTop), wideAxisRect7->axis(QCPAxis::atLeft));
        mainGraph8->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph8->setPen(QPen(QColor(120, 120, 120), 2));


        mainGraph9 = plot->addGraph(wideAxisRect8->axis(QCPAxis::atTop), wideAxisRect8->axis(QCPAxis::atLeft));
        mainGraph9->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph9->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph10 = plot->addGraph(wideAxisRect9->axis(QCPAxis::atTop), wideAxisRect9->axis(QCPAxis::atLeft));
        mainGraph10->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph10->setPen(QPen(QColor(120, 120, 120), 2));

        mainGraph11 = plot->addGraph(wideAxisRect10->axis(QCPAxis::atTop), wideAxisRect10->axis(QCPAxis::atLeft));
        mainGraph11->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph11->setPen(QPen(QColor(120, 120, 120), 2));


        mainGraph12 = plot->addGraph(wideAxisRect11->axis(QCPAxis::atTop), wideAxisRect11->axis(QCPAxis::atLeft));
        mainGraph12->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
        mainGraph12->setPen(QPen(QColor(120, 120, 120), 2));





    }
    else
    {
        forplottingmagenticdata = 0;

        wideAxisRect = new QCPAxisRect(plot);
        wideAxisRect1 = new QCPAxisRect(plot);
        wideAxisRect2 = new QCPAxisRect(plot);
        marginGroup = new QCPMarginGroup(plot);
        marginGroup1 = new QCPMarginGroup(plot);
        marginGroup2 = new QCPMarginGroup(plot);

        //wideAxisRect->addAxis(QCPAxis::atLeft)->setTickLabelColor(QColor("#6050F8"));

       plot->plotLayout()->addElement(0, 0, wideAxisRect); // insert axis rect in first row
       plot->plotLayout()->addElement(1, 0, wideAxisRect1); // sub layout in second row (grid layout will grow accordingly)
       plot->plotLayout()->addElement(2, 0, wideAxisRect2);
       plot->plotLayout()->setRowStretchFactor(1, 1);

       wideAxisRect->setMaximumSize(1500, 500);
       wideAxisRect1->setMaximumSize(1500, 500);
       wideAxisRect2->setMaximumSize(1500, 500);
       wideAxisRect->setMinimumSize(1500, 500);
       wideAxisRect1->setMinimumSize(1500, 500);
       wideAxisRect2->setMinimumSize(1500, 500);

       wideAxisRect->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup);
       wideAxisRect1->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup1);
       wideAxisRect2->setMarginGroup(QCP::msLeft | QCP::msRight, marginGroup2);
       // move newly created axes on "axes" layer and grids on "grid" layer:
       foreach (QCPAxisRect *rect, plot->axisRects())
       {
         foreach (QCPAxis *axis, rect->axes())
         {
           axis->setLayer("axes");
           axis->grid()->setLayer("grid");
         }
       }

       mainGraph1 = plot->addGraph(wideAxisRect->axis(QCPAxis::atTop), wideAxisRect->axis(QCPAxis::atLeft));
       mainGraph1->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
       mainGraph1->setPen(QPen(QColor(120, 120, 120), 2));

       mainGraph2 = plot->addGraph(wideAxisRect1->axis(QCPAxis::atTop), wideAxisRect1->axis(QCPAxis::atLeft));
       mainGraph2->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
       mainGraph2->setPen(QPen(QColor(120, 120, 120), 2));


       mainGraph3 = plot->addGraph(wideAxisRect2->axis(QCPAxis::atTop), wideAxisRect2->axis(QCPAxis::atLeft));
       mainGraph3->setScatterStyle(QCPScatterStyle(QCPScatterStyle::ssNone, QPen(Qt::black), QBrush(Qt::white), 6));
       mainGraph3->setPen(QPen(QColor(120, 120, 120), 2));

    }

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

    // Display on the tree list
    for(int i = 0; i < charactList.length(); i++)
    {
        if(charactList[i].uuid() == charact.uuid())
        {
            characteristicTree->topLevelItem(i)->setText(1, getCharactValue(value));
            break;
        }
    }



}//end slot newValueNotif



QString mainDialog::getCharactValue(QByteArray value)
{

    QByteArray rawData = value.toHex();
    QString result = "";
    //Nischal
    QString resultx, resulty ,resultz = "";


    for (int i = 0; i < rawData.size(); i++)
    {
        result += rawData.at(i);

        if(i<=3)
        {
            resultx += rawData.at(i);
        }
        if(i > 3 && i <= 7)
        {
            resulty += rawData.at(i);
        }
        if( i>7 && i < rawData.size())
        {
            resultz += rawData.at(i);
        }
    }

    bool digi;
    int a,b,c;

    a = resultx.toInt(&digi,16);
    b = resulty.toInt(&digi,16);
    c = resultz.toInt(&digi,16);

    a=(a-30000);
    b=(b-30000);
    c=(c-30000);


    double key = QDateTime::currentDateTime().toMSecsSinceEpoch()/1000.0;
    static double lastPointKey = 0;
    if (key-lastPointKey > 0.001) // at most add point every 1 ms
    {
      // add data to lines:
      if(forplottingmagenticdata == 0)
      {
      mainGraph1->addData(key, a);
      mainGraph2->addData(key, b);
      mainGraph3->addData(key,c);

      mainGraph1->removeDataBefore(key-10);
      mainGraph2->removeDataBefore(key-10);
      mainGraph3->removeDataBefore(key-10);
      // rescale value (vertical) axis to fit the current data:
      mainGraph1->rescaleAxes();
      mainGraph2->rescaleAxes();
      mainGraph3->rescaleAxes();

     }


      else if (forplottingmagenticdata == 1)

      {
          switch(sensor)
          {
          case 0:
          {
              mainGraph1->addData(key, a);
              mainGraph2->addData(key, b);
              mainGraph3->addData(key,c);

              mainGraph1->removeDataBefore(key-10);
              mainGraph2->removeDataBefore(key-10);
              mainGraph3->removeDataBefore(key-10);
              // rescale value (vertical) axis to fit the current data:
              mainGraph1->rescaleAxes();
              mainGraph2->rescaleAxes();
              mainGraph3->rescaleAxes();
              sensor = 1;
              break;
          }


          case 1:
          {
              mainGraph4->addData(key, a);
              mainGraph5->addData(key, b);
              mainGraph6->addData(key,c);

              mainGraph4->removeDataBefore(key-8);
              mainGraph5->removeDataBefore(key-8);
              mainGraph6->removeDataBefore(key-8);
              // rescale value (vertical) axis to fit the current data:
              mainGraph4->rescaleAxes();
              mainGraph5->rescaleAxes();
              mainGraph6->rescaleAxes();
              sensor = 2;
               break;
          }


          case 2:
          {
              mainGraph7->addData(key, a);
              mainGraph8->addData(key, b);
              mainGraph9->addData(key,c);

              mainGraph7->removeDataBefore(key-8);
              mainGraph8->removeDataBefore(key-8);
              mainGraph9->removeDataBefore(key-8);
              // rescale value (vertical) axis to fit the current data:
              mainGraph7->rescaleAxes();
              mainGraph8->rescaleAxes();
              mainGraph9->rescaleAxes();
              sensor = 3;
              break;
          }


          case 3:
          {
              mainGraph10->addData(key, a);
              mainGraph11->addData(key, b);
              mainGraph12->addData(key,c);

              mainGraph10->removeDataBefore(key-8);
              mainGraph11->removeDataBefore(key-8);
              mainGraph12->removeDataBefore(key-8);
              // rescale value (vertical) axis to fit the current data:
              mainGraph10->rescaleAxes();
              mainGraph11->rescaleAxes();
              mainGraph12->rescaleAxes();
              sensor = 0;
              break;
          }

      }

      }
      lastPointKey = key;
    }
    // make key axis range scroll with the data (at a constant range size of 8):

    //plot->xAxis->setRange(key+0.25, 100, Qt::AlignRight);

    plot->replot();

    return result;

}//end method getCharactValue


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
