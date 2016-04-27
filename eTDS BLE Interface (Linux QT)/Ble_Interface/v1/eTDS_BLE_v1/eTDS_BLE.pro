#-------------------------------------------------
#
# Project created by QtCreator 2015-04-29T12:33:12
#
#-------------------------------------------------

QT       += core gui
QT       += quick bluetooth

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = eTDS_BLE_v1
TEMPLATE = app


SOURCES += main.cpp\
        maindialog.cpp \
    deviceinfo.cpp

HEADERS  += maindialog.h \
    deviceinfo.h

FORMS    += maindialog.ui
