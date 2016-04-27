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
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QTreeWidget>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_mainDialog
{
public:
    QWidget *verticalLayoutWidget;
    QVBoxLayout *verticalLayout;
    QLabel *connectionLabel;
    QTreeWidget *devicesTree;
    QTreeWidget *servicesTree;
    QTreeWidget *characteristicTree;
    QHBoxLayout *horizontalLayout_2;
    QDialogButtonBox *buttonBox;

    void setupUi(QDialog *mainDialog)
    {
        if (mainDialog->objectName().isEmpty())
            mainDialog->setObjectName(QStringLiteral("mainDialog"));
        mainDialog->resize(1131, 606);
        QSizePolicy sizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(mainDialog->sizePolicy().hasHeightForWidth());
        mainDialog->setSizePolicy(sizePolicy);
        verticalLayoutWidget = new QWidget(mainDialog);
        verticalLayoutWidget->setObjectName(QStringLiteral("verticalLayoutWidget"));
        verticalLayoutWidget->setGeometry(QRect(0, 10, 1121, 591));
        verticalLayout = new QVBoxLayout(verticalLayoutWidget);
        verticalLayout->setSpacing(6);
        verticalLayout->setContentsMargins(11, 11, 11, 11);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        verticalLayout->setSizeConstraint(QLayout::SetNoConstraint);
        verticalLayout->setContentsMargins(0, 0, 0, 0);
        connectionLabel = new QLabel(verticalLayoutWidget);
        connectionLabel->setObjectName(QStringLiteral("connectionLabel"));
        connectionLabel->setScaledContents(true);

        verticalLayout->addWidget(connectionLabel);

        devicesTree = new QTreeWidget(verticalLayoutWidget);
        QTreeWidgetItem *__qtreewidgetitem = new QTreeWidgetItem();
        __qtreewidgetitem->setText(0, QStringLiteral("Address"));
        devicesTree->setHeaderItem(__qtreewidgetitem);
        devicesTree->setObjectName(QStringLiteral("devicesTree"));
        devicesTree->setAlternatingRowColors(true);

        verticalLayout->addWidget(devicesTree);

        servicesTree = new QTreeWidget(verticalLayoutWidget);
        QTreeWidgetItem *__qtreewidgetitem1 = new QTreeWidgetItem();
        __qtreewidgetitem1->setText(0, QStringLiteral("Service"));
        servicesTree->setHeaderItem(__qtreewidgetitem1);
        servicesTree->setObjectName(QStringLiteral("servicesTree"));
        servicesTree->setSizeAdjustPolicy(QAbstractScrollArea::AdjustToContentsOnFirstShow);
        servicesTree->setAlternatingRowColors(true);

        verticalLayout->addWidget(servicesTree);

        characteristicTree = new QTreeWidget(verticalLayoutWidget);
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

        verticalLayout->addWidget(characteristicTree);

        horizontalLayout_2 = new QHBoxLayout();
        horizontalLayout_2->setSpacing(6);
        horizontalLayout_2->setObjectName(QStringLiteral("horizontalLayout_2"));
        buttonBox = new QDialogButtonBox(verticalLayoutWidget);
        buttonBox->setObjectName(QStringLiteral("buttonBox"));
        buttonBox->setStandardButtons(QDialogButtonBox::Close|QDialogButtonBox::Ok);

        horizontalLayout_2->addWidget(buttonBox);


        verticalLayout->addLayout(horizontalLayout_2);


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
