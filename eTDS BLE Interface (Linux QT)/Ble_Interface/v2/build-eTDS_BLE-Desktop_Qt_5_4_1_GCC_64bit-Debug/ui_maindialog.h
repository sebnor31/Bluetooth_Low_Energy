/********************************************************************************
** Form generated from reading UI file 'maindialog.ui'
**
** Created by: Qt User Interface Compiler version 5.4.1
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINDIALOG_H
#define UI_MAINDIALOG_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QDialog>
#include <QtWidgets/QDialogButtonBox>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QSplitter>
#include <QtWidgets/QTreeWidget>
#include "qcustomplot.h"

QT_BEGIN_NAMESPACE

class Ui_mainDialog
{
public:
    QLabel *connectionLabel;
    QSplitter *splitter;
    QTreeWidget *devicesTree;
    QTreeWidget *servicesTree;
    QTreeWidget *characteristicTree;
    QCustomPlot *plot;
    QDialogButtonBox *buttonBox;

    void setupUi(QDialog *mainDialog)
    {
        if (mainDialog->objectName().isEmpty())
            mainDialog->setObjectName(QStringLiteral("mainDialog"));
        mainDialog->resize(1131, 715);
        QSizePolicy sizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(mainDialog->sizePolicy().hasHeightForWidth());
        mainDialog->setSizePolicy(sizePolicy);
        connectionLabel = new QLabel(mainDialog);
        connectionLabel->setObjectName(QStringLiteral("connectionLabel"));
        connectionLabel->setGeometry(QRect(1, 11, 611, 17));
        connectionLabel->setScaledContents(true);
        splitter = new QSplitter(mainDialog);
        splitter->setObjectName(QStringLiteral("splitter"));
        splitter->setGeometry(QRect(0, 40, 1121, 192));
        splitter->setOrientation(Qt::Horizontal);
        devicesTree = new QTreeWidget(splitter);
        QTreeWidgetItem *__qtreewidgetitem = new QTreeWidgetItem();
        __qtreewidgetitem->setText(0, QStringLiteral("Address"));
        devicesTree->setHeaderItem(__qtreewidgetitem);
        devicesTree->setObjectName(QStringLiteral("devicesTree"));
        devicesTree->setAlternatingRowColors(true);
        splitter->addWidget(devicesTree);
        servicesTree = new QTreeWidget(splitter);
        QTreeWidgetItem *__qtreewidgetitem1 = new QTreeWidgetItem();
        __qtreewidgetitem1->setText(0, QStringLiteral("Service"));
        servicesTree->setHeaderItem(__qtreewidgetitem1);
        servicesTree->setObjectName(QStringLiteral("servicesTree"));
        servicesTree->setSizeAdjustPolicy(QAbstractScrollArea::AdjustToContentsOnFirstShow);
        servicesTree->setAlternatingRowColors(true);
        splitter->addWidget(servicesTree);
        characteristicTree = new QTreeWidget(splitter);
        QFont font;
        font.setBold(true);
        font.setWeight(75);
        QTreeWidgetItem *__qtreewidgetitem2 = new QTreeWidgetItem();
        __qtreewidgetitem2->setFont(1, font);
        __qtreewidgetitem2->setText(0, QStringLiteral("Name"));
        __qtreewidgetitem2->setFont(0, font);
        characteristicTree->setHeaderItem(__qtreewidgetitem2);
        characteristicTree->setObjectName(QStringLiteral("characteristicTree"));
        characteristicTree->setAutoFillBackground(false);
        characteristicTree->setFrameShape(QFrame::StyledPanel);
        characteristicTree->setFrameShadow(QFrame::Sunken);
        characteristicTree->setSizeAdjustPolicy(QAbstractScrollArea::AdjustToContentsOnFirstShow);
        characteristicTree->setAlternatingRowColors(true);
        splitter->addWidget(characteristicTree);
        plot = new QCustomPlot(mainDialog);
        plot->setObjectName(QStringLiteral("plot"));
        plot->setGeometry(QRect(1, 240, 1119, 421));
        buttonBox = new QDialogButtonBox(mainDialog);
        buttonBox->setObjectName(QStringLiteral("buttonBox"));
        buttonBox->setGeometry(QRect(930, 670, 176, 27));
        buttonBox->setStandardButtons(QDialogButtonBox::Close|QDialogButtonBox::Ok);

        retranslateUi(mainDialog);

        QMetaObject::connectSlotsByName(mainDialog);
    } // setupUi

    void retranslateUi(QDialog *mainDialog)
    {
        mainDialog->setWindowTitle(QApplication::translate("mainDialog", "eTDS BLE Interface", 0));
        connectionLabel->setText(QApplication::translate("mainDialog", "Not Connected", 0));
        QTreeWidgetItem *___qtreewidgetitem = devicesTree->headerItem();
        ___qtreewidgetitem->setText(2, QApplication::translate("mainDialog", "Name", 0));
        ___qtreewidgetitem->setText(1, QApplication::translate("mainDialog", "Rssi", 0));
        QTreeWidgetItem *___qtreewidgetitem1 = characteristicTree->headerItem();
        ___qtreewidgetitem1->setText(1, QApplication::translate("mainDialog", "Value", 0));
    } // retranslateUi

};

namespace Ui {
    class mainDialog: public Ui_mainDialog {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINDIALOG_H
