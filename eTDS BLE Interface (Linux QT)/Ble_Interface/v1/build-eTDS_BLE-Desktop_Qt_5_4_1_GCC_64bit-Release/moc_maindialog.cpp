/****************************************************************************
** Meta object code from reading C++ file 'maindialog.h'
**
** Created by: The Qt Meta Object Compiler version 67 (Qt 5.4.1)
**
** WARNING! All changes made in this file will be lost!
*****************************************************************************/

#include "../eTDS_BLE_v1/maindialog.h"
#include <QtCore/qbytearray.h>
#include <QtCore/qmetatype.h>
#if !defined(Q_MOC_OUTPUT_REVISION)
#error "The header file 'maindialog.h' doesn't include <QObject>."
#elif Q_MOC_OUTPUT_REVISION != 67
#error "This file was generated using the moc from 5.4.1. It"
#error "cannot be used with the include files from this version of Qt."
#error "(The moc has changed too much.)"
#endif

QT_BEGIN_MOC_NAMESPACE
struct qt_meta_stringdata_mainDialog_t {
    QByteArrayData data[24];
    char stringdata[348];
};
#define QT_MOC_LITERAL(idx, ofs, len) \
    Q_STATIC_BYTE_ARRAY_DATA_HEADER_INITIALIZER_WITH_OFFSET(len, \
    qptrdiff(offsetof(qt_meta_stringdata_mainDialog_t, stringdata) + ofs \
        - idx * sizeof(QByteArrayData)) \
    )
static const qt_meta_stringdata_mainDialog_t qt_meta_stringdata_mainDialog = {
    {
QT_MOC_LITERAL(0, 0, 10), // "mainDialog"
QT_MOC_LITERAL(1, 11, 9), // "addDevice"
QT_MOC_LITERAL(2, 21, 0), // ""
QT_MOC_LITERAL(3, 22, 20), // "QBluetoothDeviceInfo"
QT_MOC_LITERAL(4, 43, 6), // "device"
QT_MOC_LITERAL(5, 50, 11), // "disableScan"
QT_MOC_LITERAL(6, 62, 10), // "enableScan"
QT_MOC_LITERAL(7, 73, 14), // "deviceSelected"
QT_MOC_LITERAL(8, 88, 16), // "QTreeWidgetItem*"
QT_MOC_LITERAL(9, 105, 14), // "selectedDevice"
QT_MOC_LITERAL(10, 120, 15), // "deviceConnected"
QT_MOC_LITERAL(11, 136, 17), // "serviceDiscovered"
QT_MOC_LITERAL(12, 154, 14), // "QBluetoothUuid"
QT_MOC_LITERAL(13, 169, 10), // "newService"
QT_MOC_LITERAL(14, 180, 24), // "serviceDiscoveryFinished"
QT_MOC_LITERAL(15, 205, 15), // "serviceSelected"
QT_MOC_LITERAL(16, 221, 15), // "selectedService"
QT_MOC_LITERAL(17, 237, 19), // "serviceStateChanged"
QT_MOC_LITERAL(18, 257, 31), // "QLowEnergyService::ServiceState"
QT_MOC_LITERAL(19, 289, 5), // "state"
QT_MOC_LITERAL(20, 295, 13), // "newValueNotif"
QT_MOC_LITERAL(21, 309, 24), // "QLowEnergyCharacteristic"
QT_MOC_LITERAL(22, 334, 7), // "charact"
QT_MOC_LITERAL(23, 342, 5) // "value"

    },
    "mainDialog\0addDevice\0\0QBluetoothDeviceInfo\0"
    "device\0disableScan\0enableScan\0"
    "deviceSelected\0QTreeWidgetItem*\0"
    "selectedDevice\0deviceConnected\0"
    "serviceDiscovered\0QBluetoothUuid\0"
    "newService\0serviceDiscoveryFinished\0"
    "serviceSelected\0selectedService\0"
    "serviceStateChanged\0QLowEnergyService::ServiceState\0"
    "state\0newValueNotif\0QLowEnergyCharacteristic\0"
    "charact\0value"
};
#undef QT_MOC_LITERAL

static const uint qt_meta_data_mainDialog[] = {

 // content:
       7,       // revision
       0,       // classname
       0,    0, // classinfo
      10,   14, // methods
       0,    0, // properties
       0,    0, // enums/sets
       0,    0, // constructors
       0,       // flags
       0,       // signalCount

 // slots: name, argc, parameters, tag, flags
       1,    1,   64,    2, 0x08 /* Private */,
       5,    0,   67,    2, 0x08 /* Private */,
       6,    0,   68,    2, 0x08 /* Private */,
       7,    1,   69,    2, 0x08 /* Private */,
      10,    0,   72,    2, 0x08 /* Private */,
      11,    1,   73,    2, 0x08 /* Private */,
      14,    0,   76,    2, 0x08 /* Private */,
      15,    1,   77,    2, 0x08 /* Private */,
      17,    1,   80,    2, 0x08 /* Private */,
      20,    2,   83,    2, 0x08 /* Private */,

 // slots: parameters
    QMetaType::Void, 0x80000000 | 3,    4,
    QMetaType::Void,
    QMetaType::Void,
    QMetaType::Void, 0x80000000 | 8,    9,
    QMetaType::Void,
    QMetaType::Void, 0x80000000 | 12,   13,
    QMetaType::Void,
    QMetaType::Void, 0x80000000 | 8,   16,
    QMetaType::Void, 0x80000000 | 18,   19,
    QMetaType::Void, 0x80000000 | 21, QMetaType::QByteArray,   22,   23,

       0        // eod
};

void mainDialog::qt_static_metacall(QObject *_o, QMetaObject::Call _c, int _id, void **_a)
{
    if (_c == QMetaObject::InvokeMetaMethod) {
        mainDialog *_t = static_cast<mainDialog *>(_o);
        switch (_id) {
        case 0: _t->addDevice((*reinterpret_cast< const QBluetoothDeviceInfo(*)>(_a[1]))); break;
        case 1: _t->disableScan(); break;
        case 2: _t->enableScan(); break;
        case 3: _t->deviceSelected((*reinterpret_cast< QTreeWidgetItem*(*)>(_a[1]))); break;
        case 4: _t->deviceConnected(); break;
        case 5: _t->serviceDiscovered((*reinterpret_cast< const QBluetoothUuid(*)>(_a[1]))); break;
        case 6: _t->serviceDiscoveryFinished(); break;
        case 7: _t->serviceSelected((*reinterpret_cast< QTreeWidgetItem*(*)>(_a[1]))); break;
        case 8: _t->serviceStateChanged((*reinterpret_cast< QLowEnergyService::ServiceState(*)>(_a[1]))); break;
        case 9: _t->newValueNotif((*reinterpret_cast< QLowEnergyCharacteristic(*)>(_a[1])),(*reinterpret_cast< QByteArray(*)>(_a[2]))); break;
        default: ;
        }
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        switch (_id) {
        default: *reinterpret_cast<int*>(_a[0]) = -1; break;
        case 5:
            switch (*reinterpret_cast<int*>(_a[1])) {
            default: *reinterpret_cast<int*>(_a[0]) = -1; break;
            case 0:
                *reinterpret_cast<int*>(_a[0]) = qRegisterMetaType< QBluetoothUuid >(); break;
            }
            break;
        }
    }
}

const QMetaObject mainDialog::staticMetaObject = {
    { &QDialog::staticMetaObject, qt_meta_stringdata_mainDialog.data,
      qt_meta_data_mainDialog,  qt_static_metacall, Q_NULLPTR, Q_NULLPTR}
};


const QMetaObject *mainDialog::metaObject() const
{
    return QObject::d_ptr->metaObject ? QObject::d_ptr->dynamicMetaObject() : &staticMetaObject;
}

void *mainDialog::qt_metacast(const char *_clname)
{
    if (!_clname) return Q_NULLPTR;
    if (!strcmp(_clname, qt_meta_stringdata_mainDialog.stringdata))
        return static_cast<void*>(const_cast< mainDialog*>(this));
    if (!strcmp(_clname, "Ui::mainDialog"))
        return static_cast< Ui::mainDialog*>(const_cast< mainDialog*>(this));
    return QDialog::qt_metacast(_clname);
}

int mainDialog::qt_metacall(QMetaObject::Call _c, int _id, void **_a)
{
    _id = QDialog::qt_metacall(_c, _id, _a);
    if (_id < 0)
        return _id;
    if (_c == QMetaObject::InvokeMetaMethod) {
        if (_id < 10)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 10;
    } else if (_c == QMetaObject::RegisterMethodArgumentMetaType) {
        if (_id < 10)
            qt_static_metacall(this, _c, _id, _a);
        _id -= 10;
    }
    return _id;
}
QT_END_MOC_NAMESPACE
