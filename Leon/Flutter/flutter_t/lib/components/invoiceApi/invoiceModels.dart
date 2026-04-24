import 'dart:convert';


class Address {
  final int id;
  final String countryCode;
  final String line1;
  final String line2;
  final String gln;

  Address({
    required this.id,
    required this.countryCode,
    required this.line1,
    required this.line2,
    required this.gln,
  });

  factory Address.fromJson(Map<String, dynamic> json) {
    return Address(
      id: json['id'] ?? 0,
      countryCode: json['countryCode'] ?? '',
      line1: json['line1'] ?? '',
      line2: json['line2'] ?? '',
      gln: json['gln'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'countryCode': countryCode,
      'line1': line1,
      'line2': line2,
      'gln': gln,
    };
  }
}


class BankAccount {
  final int id;
  final String fullNumber;
  final String swift;
  final String bankName;
  final String description;
  final int isBankOwnAccount;

  BankAccount({
    required this.id,
    required this.fullNumber,
    required this.swift,
    required this.bankName,
    required this.description,
    required this.isBankOwnAccount,
  });

  factory BankAccount.fromJson(Map<String, dynamic> json) {
    return BankAccount(
      id: json['id'] ?? 0,
      fullNumber: json['fullNumber'] ?? '',
      swift: json['swift'] ?? '',
      bankName: json['bankName'] ?? '',
      description: json['description'] ?? '',
      isBankOwnAccount: json['isBankOwnAccount'] ?? 0,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'fullNumber': fullNumber,
      'swift': swift,
      'bankName': bankName,
      'description': description,
      'isBankOwnAccount': isBankOwnAccount,
    };
  }
}


class ContactInfo {
  final int id;
  final String email;
  final String phone;

  ContactInfo({
    required this.id,
    required this.email,
    required this.phone,
  });

  factory ContactInfo.fromJson(Map<String, dynamic> json) {
    return ContactInfo(
      id: json['id'] ?? 0,
      email: json['email'] ?? '',
      phone: json['phone'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'email': email,
      'phone': phone,
    };
  }
}


class Party {
  final int id;
  final String role;
  final String eori;
  final String nip;
  final String name;

  final int? mainAddressId;
  final Address? mainAddress;

  final int? correspondenceAddressID;
  final Address? correspondenceAddress;

  final int? contactInfoId;
  final ContactInfo? contact;

  final String customerNumber;

  Party({
    required this.id,
    required this.role,
    required this.eori,
    required this.nip,
    required this.name,
    required this.mainAddressId,
    required this.mainAddress,
    required this.correspondenceAddressID,
    required this.correspondenceAddress,
    required this.contactInfoId,
    required this.contact,
    required this.customerNumber,
  });

  factory Party.fromJson(Map<String, dynamic> json) {
    return Party(
      id: json['id'] ?? 0,
      role: json['role'] ?? '',
      eori: json['eori'] ?? '',
      nip: json['nip'] ?? '',
      name: json['name'] ?? '',
      mainAddressId: json['mainAddressId'],
      mainAddress: json['mainAddress'] != null
          ? Address.fromJson(json['mainAddress'])
          : null,
      correspondenceAddressID: json['correspondenceAddressID'],
      correspondenceAddress: json['correspondenceAddress'] != null
          ? Address.fromJson(json['correspondenceAddress'])
          : null,
      contactInfoId: json['contactInfoId'],
      contact:
      json['contact'] != null ? ContactInfo.fromJson(json['contact']) : null,
      customerNumber: json['customerNumber'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'role': role,
      'eori': eori,
      'nip': nip,
      'name': name,
      'mainAddressId': mainAddressId,
      'mainAddress': mainAddress?.toJson(),
      'correspondenceAddressID': correspondenceAddressID,
      'correspondenceAddress': correspondenceAddress?.toJson(),
      'contactInfoId': contactInfoId,
      'contact': contact?.toJson(),
      'customerNumber': customerNumber,
    };
  }
}


class Carrier {
  final int id;
  final String countryCode;
  final String taxId;
  final String name;

  final int? addressId;
  final Address? address;

  Carrier({
    required this.id,
    required this.countryCode,
    required this.taxId,
    required this.name,
    required this.addressId,
    required this.address,
  });

  factory Carrier.fromJson(Map<String, dynamic> json) {
    return Carrier(
      id: json['id'] ?? 0,
      countryCode: json['countryCode'] ?? '',
      taxId: json['taxId'] ?? '',
      name: json['name'] ?? '',
      addressId: json['addressId'],
      address:
      json['address'] != null ? Address.fromJson(json['address']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'countryCode': countryCode,
      'taxId': taxId,
      'name': name,
      'addressId': addressId,
      'address': address?.toJson(),
    };
  }
}


class Contract {
  final int id;
  final DateTime? contractDate;
  final String contractNumber;
  final int termsId;

  Contract({
    required this.id,
    required this.contractDate,
    required this.contractNumber,
    required this.termsId,
  });

  factory Contract.fromJson(Map<String, dynamic> json) {
    return Contract(
      id: json['id'] ?? 0,
      contractDate: json['contractDate'] != null
          ? DateTime.tryParse(json['contractDate'])
          : null,
      contractNumber: json['contractNumber'] ?? '',
      termsId: json['termsId'] ?? 0,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'contractDate': contractDate?.toIso8601String(),
      'contractNumber': contractNumber,
      'termsId': termsId,
    };
  }
}


class OrderInfo {
  final int id;
  final DateTime? orderDate;
  final String orderNumber;
  final int termsId;

  OrderInfo({
    required this.id,
    required this.orderDate,
    required this.orderNumber,
    required this.termsId,
  });

  factory OrderInfo.fromJson(Map<String, dynamic> json) {
    return OrderInfo(
      id: json['id'] ?? 0,
      orderDate: json['orderDate'] != null
          ? DateTime.tryParse(json['orderDate'])
          : null,
      orderNumber: json['orderNumber'] ?? '',
      termsId: json['termsId'] ?? 0,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'orderDate': orderDate?.toIso8601String(),
      'orderNumber': orderNumber,
      'termsId': termsId,
    };
  }
}


class Terms {
  final int id;
  final int invoiceId;

  final Contract? contract;
  final OrderInfo? order;
  final String deliveryTerms;
  final TransportInfo? transport;

  Terms({
    required this.id,
    required this.invoiceId,
    required this.contract,
    required this.order,
    required this.deliveryTerms,
    required this.transport,
  });

  factory Terms.fromJson(Map<String, dynamic> json) {
    return Terms(
      id: json['id'] ?? 0,
      invoiceId: json['invoiceId'] ?? 0,
      contract:
      json['contract'] != null ? Contract.fromJson(json['contract']) : null,
      order:
      json['order'] != null ? OrderInfo.fromJson(json['order']) : null,
      deliveryTerms: json['deliveryTerms'] ?? '',
      transport: json['transport'] != null
          ? TransportInfo.fromJson(json['transport'])
          : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'invoiceId': invoiceId,
      'contract': contract?.toJson(),
      'order': order?.toJson(),
      'deliveryTerms': deliveryTerms,
      'transport': transport?.toJson(),
    };
  }
}


class TransportInfo {
  final int id;
  final int? transportType;
  final Carrier? carrier;
  final String transportOrderNumber;
  final int? cargoDescription;
  final String packagingUnit;
  final DateTime startDate;
  final DateTime endDate;

  final int? shipFromId;
  final Address? shipFrom;

  final int? shipViaID;
  final Address? shipVia;

  final int? shipToID;
  final Address? shipTo;

  final int termsId;

  TransportInfo({
    required this.id,
    required this.transportType,
    required this.carrier,
    required this.transportOrderNumber,
    required this.cargoDescription,
    required this.packagingUnit,
    required this.startDate,
    required this.endDate,
    required this.shipFromId,
    required this.shipFrom,
    required this.shipViaID,
    required this.shipVia,
    required this.shipToID,
    required this.shipTo,
    required this.termsId,
  });

  factory TransportInfo.fromJson(Map<String, dynamic> json) {
    return TransportInfo(
      id: json['id'] ?? 0,
      transportType: json['transportType'],
      carrier:
      json['carrier'] != null ? Carrier.fromJson(json['carrier']) : null,
      transportOrderNumber: json['transportOrderNumber'] ?? '',
      cargoDescription: json['cargoDescription'],
      packagingUnit: json['packagingUnit'] ?? '',
      startDate: DateTime.tryParse(json['startDate'] ?? '') ?? DateTime(1970),
      endDate: DateTime.tryParse(json['endDate'] ?? '') ?? DateTime(1970),
      shipFromId: json['shipFromId'],
      shipFrom:
      json['shipFrom'] != null ? Address.fromJson(json['shipFrom']) : null,
      shipViaID: json['shipViaID'],
      shipVia:
      json['shipVia'] != null ? Address.fromJson(json['shipVia']) : null,
      shipToID: json['shipToID'],
      shipTo:
      json['shipTo'] != null ? Address.fromJson(json['shipTo']) : null,
      termsId: json['termsId'] ?? 0,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'transportType': transportType,
      'carrier': carrier?.toJson(),
      'transportOrderNumber': transportOrderNumber,
      'cargoDescription': cargoDescription,
      'packagingUnit': packagingUnit,
      'startDate': startDate.toIso8601String(),
      'endDate': endDate.toIso8601String(),
      'shipFromId': shipFromId,
      'shipFrom': shipFrom?.toJson(),
      'shipViaID': shipViaID,
      'shipVia': shipVia?.toJson(),
      'shipToID': shipToID,
      'shipTo': shipTo?.toJson(),
      'termsId': termsId,
    };
  }
}

class InvoiceLine {
  final int id;
  final int invoiceId;
  final String name;
  final double pricePerPiceNetto;
  final int quantity;
  final String unit;
  final int taxRate;
  final String priceTotalNetto;
  final double taxValue;

  InvoiceLine({
    required this.id,
    required this.invoiceId,
    required this.name,
    required this.pricePerPiceNetto,
    required this.quantity,
    required this.unit,
    required this.taxRate,
    required this.priceTotalNetto,
    required this.taxValue,
  });

  factory InvoiceLine.fromJson(Map<String, dynamic> json) {
    return InvoiceLine(
      id: json['id'] ?? 0,
      invoiceId: json['invoiceId'] ?? 0,
      name: json['name'] ?? '',
      pricePerPiceNetto: (json['pricePerPiceNetto'] ?? 0).toDouble(),
      quantity: json['quantity'] ?? 0,
      unit: json['unit'] ?? '',
      taxRate: json['taxRate'] ?? 0,
      priceTotalNetto: json['priceTotalNetto'] ?? '',
      taxValue: (json['taxValue'] ?? 0).toDouble(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'invoiceId': invoiceId,
      'name': name,
      'pricePerPiceNetto': pricePerPiceNetto,
      'quantity': quantity,
      'unit': unit,
      'taxRate': taxRate,
      'priceTotalNetto': priceTotalNetto,
      'taxValue': taxValue,
    };
  }
}


class TaxSummary {
  final int id;
  final int invoiceId;
  final String taxRate;
  final double netto;
  final double taxAmount;
  final double brutto;
  final double plnAmount;

  TaxSummary({
    required this.id,
    required this.invoiceId,
    required this.taxRate,
    required this.netto,
    required this.taxAmount,
    required this.brutto,
    required this.plnAmount,
  });

  factory TaxSummary.fromJson(Map<String, dynamic> json) {
    return TaxSummary(
      id: json['id'] ?? 0,
      invoiceId: json['invoiceId'] ?? 0,
      taxRate: json['taxRate'] ?? '',
      netto: (json['netto'] ?? 0).toDouble(),
      taxAmount: (json['taxAmount'] ?? 0).toDouble(),
      brutto: (json['brutto'] ?? 0).toDouble(),
      plnAmount: (json['plnAmount'] ?? 0).toDouble(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'invoiceId': invoiceId,
      'taxRate': taxRate,
      'netto': netto,
      'taxAmount': taxAmount,
      'brutto': brutto,
      'plnAmount': plnAmount,
    };
  }
}


class Charge {
  final int id;
  final String reason;
  final double? amount;
  final int? settlementId;

  final Settlement? settlement;

  Charge({
    required this.id,
    required this.reason,
    required this.amount,
    required this.settlementId,
    required this.settlement,
  });

  factory Charge.fromJson(Map<String, dynamic> json) {
    return Charge(
      id: json['id'] ?? 0,
      reason: json['reason'] ?? '',
      amount: json['amount'] != null ? (json['amount'] as num).toDouble() : null,
      settlementId: json['settlementId'],
      settlement: json['settlement'] != null
          ? Settlement.fromJson(json['settlement'])
          : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'reason': reason,
      'amount': amount,
      'settlementId': settlementId,
    };
  }
}


class Deduction {
  final int id;
  final String reason;
  final double? amount;
  final int? settlementId;

  final Settlement? settlement;

  Deduction({
    required this.id,
    required this.reason,
    required this.amount,
    required this.settlementId,
    required this.settlement,
  });

  factory Deduction.fromJson(Map<String, dynamic> json) {
    return Deduction(
      id: json['id'] ?? 0,
      reason: json['reason'] ?? '',
      amount: json['amount'] != null ? (json['amount'] as num).toDouble() : null,
      settlementId: json['settlementId'],
      settlement: json['settlement'] != null
          ? Settlement.fromJson(json['settlement'])
          : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'reason': reason,
      'amount': amount,
      'settlementId': settlementId,
      // settlement intentionally omitted
    };
  }
}


class Settlement {
  final int id;
  final int invoiceId;
  final List<Charge> charges;
  final List<Deduction> deductions;
  final double totalToPay;

  Settlement({
    required this.id,
    required this.invoiceId,
    required this.charges,
    required this.deductions,
    required this.totalToPay,
  });

  factory Settlement.fromJson(Map<String, dynamic> json) {
    return Settlement(
      id: json['id'] ?? 0,
      invoiceId: json['invoiceId'] ?? 0,
      charges: (json['charges'] as List?)
          ?.map((e) => Charge.fromJson(e as Map<String, dynamic>))
          .toList() ??
          [],
      deductions: (json['deductions'] as List?)
          ?.map((e) => Deduction.fromJson(e as Map<String, dynamic>))
          .toList() ??
          [],
      totalToPay: (json['totalToPay'] ?? 0).toDouble(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'invoiceId': invoiceId,
      'charges': charges.map((e) => e.toJson()).toList(),
      'deductions': deductions.map((e) => e.toJson()).toList(),
      'totalToPay': totalToPay,
    };
  }
}


class PaymentInfo {
  final int id;
  final int invoiceId;
  final bool isPartial;
  final List<PartialPayment> partialPayments;
  final DateTime? paymentDueDate;
  final String paymentTermsDescription;
  final String paymentMethod;

  PaymentInfo({
    required this.id,
    required this.invoiceId,
    required this.isPartial,
    required this.partialPayments,
    required this.paymentDueDate,
    required this.paymentTermsDescription,
    required this.paymentMethod,
  });

  factory PaymentInfo.fromJson(Map<String, dynamic> json) {
    return PaymentInfo(
      id: json['id'] ?? 0,
      invoiceId: json['invoiceId'] ?? 0,
      isPartial: json['isPartial'] ?? false,
      partialPayments: (json['partialPayments'] as List?)
          ?.map((e) => PartialPayment.fromJson(e as Map<String, dynamic>))
          .toList() ??
          [],
      paymentDueDate: json['paymentDueDate'] != null
          ? DateTime.tryParse(json['paymentDueDate'])
          : null,
      paymentTermsDescription: json['paymentTermsDescription'] ?? '',
      paymentMethod: json['paymentMethod'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'invoiceId': invoiceId,
      'isPartial': isPartial,
      'partialPayments': partialPayments.map((e) => e.toJson()).toList(),
      'paymentDueDate': paymentDueDate?.toIso8601String(),
      'paymentTermsDescription': paymentTermsDescription,
      'paymentMethod': paymentMethod,
    };
  }
}


class PartialPayment {
  final int id;
  final DateTime date;
  final double amount;
  final String method;
  final int paymentInfoId;

  // Circular reference to PaymentInfo is kept but excluded from toJson
  final PaymentInfo? paymentInfo;

  PartialPayment({
    required this.id,
    required this.date,
    required this.amount,
    required this.method,
    required this.paymentInfoId,
    required this.paymentInfo,
  });

  factory PartialPayment.fromJson(Map<String, dynamic> json) {
    return PartialPayment(
      id: json['id'] ?? 0,
      date: DateTime.tryParse(json['date'] ?? '') ?? DateTime(1970),
      amount: (json['amount'] ?? 0).toDouble(),
      method: json['method'] ?? '',
      paymentInfoId: json['paymentInfoId'] ?? 0,
      paymentInfo: json['paymentInfo'] != null
          ? PaymentInfo.fromJson(json['paymentInfo'])
          : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'date': date.toIso8601String(),
      'amount': amount,
      'method': method,
      'paymentInfoId': paymentInfoId,
      // paymentInfo intentionally omitted
    };
  }
}


class Invoice {
  final int id;
  final String invoiceNumber;
  final String ksefNumber;
  final DateTime issueDate;
  final DateTime deliveryDate;
  final String issuePlace;
  final String currencyCode;
  final double? currencyRate;

  final int sellerId;
  final int buyerId;
  final Party seller;
  final Party buyer;

  final List<Party> otherParties;
  final List<InvoiceLine> lines;
  final List<TaxSummary> taxSummaries;

  final PaymentInfo? payment;
  final Settlement? settlement;

  final int? factorBankAccountId;
  final BankAccount? factorBankAccount;

  final int? sellerBankAccountId;
  final BankAccount? sellerBankAccount;

  final Terms? transactionTerms;
  final String footerNote;

  Invoice({
    required this.id,
    required this.invoiceNumber,
    required this.ksefNumber,
    required this.issueDate,
    required this.deliveryDate,
    required this.issuePlace,
    required this.currencyCode,
    required this.currencyRate,
    required this.sellerId,
    required this.buyerId,
    required this.seller,
    required this.buyer,
    required this.otherParties,
    required this.lines,
    required this.taxSummaries,
    required this.payment,
    required this.settlement,
    required this.factorBankAccountId,
    required this.factorBankAccount,
    required this.sellerBankAccountId,
    required this.sellerBankAccount,
    required this.transactionTerms,
    required this.footerNote,
  });

  factory Invoice.fromJson(Map<String, dynamic> json) {
    return Invoice(
      id: json['id'] ?? 0,
      invoiceNumber: json['invoiceNumber'] ?? '',
      ksefNumber: json['ksefNumber'] ?? '',
      issueDate: DateTime.tryParse(json['issueDate'] ?? '') ?? DateTime(1970),
      deliveryDate:
      DateTime.tryParse(json['deliveryDate'] ?? '') ?? DateTime(1970),
      issuePlace: json['issuePlace'] ?? '',
      currencyCode: json['currencyCode'] ?? '',
      currencyRate:
      json['currencyRate'] != null ? (json['currencyRate'] as num).toDouble() : null,
      sellerId: json['sellerId'] ?? 0,
      buyerId: json['buyerId'] ?? 0,
      seller: Party.fromJson(json['seller'] ?? {}),
      buyer: Party.fromJson(json['buyer'] ?? {}),
      otherParties: (json['otherParties'] as List?)
          ?.map((e) => Party.fromJson(e as Map<String, dynamic>))
          .toList() ??
          [],
      lines: (json['lines'] as List?)
          ?.map((e) => InvoiceLine.fromJson(e as Map<String, dynamic>))
          .toList() ??
          [],
      taxSummaries: (json['taxSummaries'] as List?)
          ?.map((e) => TaxSummary.fromJson(e as Map<String, dynamic>))
          .toList() ??
          [],
      payment: json['payment'] != null
          ? PaymentInfo.fromJson(json['payment'])
          : null,
      settlement: json['settlement'] != null
          ? Settlement.fromJson(json['settlement'])
          : null,
      factorBankAccountId: json['factorBankAccountId'],
      factorBankAccount: json['factorBankAccount'] != null
          ? BankAccount.fromJson(json['factorBankAccount'])
          : null,
      sellerBankAccountId: json['sellerBankAccountId'],
      sellerBankAccount: json['sellerBankAccount'] != null
          ? BankAccount.fromJson(json['sellerBankAccount'])
          : null,
      transactionTerms: json['transactionTerms'] != null
          ? Terms.fromJson(json['transactionTerms'])
          : null,
      footerNote: json['footerNote'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'invoiceNumber': invoiceNumber,
      'ksefNumber': ksefNumber,
      'issueDate': issueDate.toIso8601String(),
      'deliveryDate': deliveryDate.toIso8601String(),
      'issuePlace': issuePlace,
      'currencyCode': currencyCode,
      'currencyRate': currencyRate,
      'sellerId': sellerId,
      'buyerId': buyerId,
      'seller': seller.toJson(),
      'buyer': buyer.toJson(),
      'otherParties': otherParties.map((e) => e.toJson()).toList(),
      'lines': lines.map((e) => e.toJson()).toList(),
      'taxSummaries': taxSummaries.map((e) => e.toJson()).toList(),
      'payment': payment?.toJson(),
      'settlement': settlement?.toJson(),
      'factorBankAccountId': factorBankAccountId,
      'factorBankAccount': factorBankAccount?.toJson(),
      'sellerBankAccountId': sellerBankAccountId,
      'sellerBankAccount': sellerBankAccount?.toJson(),
      'transactionTerms': transactionTerms?.toJson(),
      'footerNote': footerNote,
    };
  }
}



class MyResponse {
  final Address? address;
  final BankAccount? bankAccount;
  final ContactInfo? contactInfo;
  final Invoice? invoice;
  final InvoiceLine? invoiceLine;
  final Party? party;
  final PaymentInfo? paymentInfo;
  final PartialPayment? partialPayment;
  final Settlement? settlement;
  final Charge? charge;
  final Deduction? deduction;
  final Terms? terms;
  final Contract? contract;
  final OrderInfo? orderInfo;
  final TransportInfo? transportInfo;
  final Carrier? carrier;

  MyResponse({
    required this.address,
    required this.bankAccount,
    required this.contactInfo,
    required this.invoice,
    required this.invoiceLine,
    required this.party,
    required this.paymentInfo,
    required this.partialPayment,
    required this.settlement,
    required this.charge,
    required this.deduction,
    required this.terms,
    required this.contract,
    required this.orderInfo,
    required this.transportInfo,
    required this.carrier,
  });

  factory MyResponse.fromJson(Map<String, dynamic> json) {
    return MyResponse(
      address:
      json['address'] != null ? Address.fromJson(json['address']) : null,
      bankAccount: json['bankAccount'] != null
          ? BankAccount.fromJson(json['bankAccount'])
          : null,
      contactInfo: json['contactInfo'] != null
          ? ContactInfo.fromJson(json['contactInfo'])
          : null,
      invoice:
      json['invoice'] != null ? Invoice.fromJson(json['invoice']) : null,
      invoiceLine: json['invoiceLine'] != null
          ? InvoiceLine.fromJson(json['invoiceLine'])
          : null,
      party: json['party'] != null ? Party.fromJson(json['party']) : null,
      paymentInfo: json['paymentInfo'] != null
          ? PaymentInfo.fromJson(json['paymentInfo'])
          : null,
      partialPayment: json['partialPayment'] != null
          ? PartialPayment.fromJson(json['partialPayment'])
          : null,
      settlement: json['settlement'] != null
          ? Settlement.fromJson(json['settlement'])
          : null,
      charge:
      json['charge'] != null ? Charge.fromJson(json['charge']) : null,
      deduction: json['deduction'] != null
          ? Deduction.fromJson(json['deduction'])
          : null,
      terms: json['terms'] != null ? Terms.fromJson(json['terms']) : null,
      contract:
      json['contract'] != null ? Contract.fromJson(json['contract']) : null,
      orderInfo: json['orderInfo'] != null
          ? OrderInfo.fromJson(json['orderInfo'])
          : null,
      transportInfo: json['transportInfo'] != null
          ? TransportInfo.fromJson(json['transportInfo'])
          : null,
      carrier:
      json['carrier'] != null ? Carrier.fromJson(json['carrier']) : null,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'address': address?.toJson(),
      'bankAccount': bankAccount?.toJson(),
      'contactInfo': contactInfo?.toJson(),
      'invoice': invoice?.toJson(),
      'invoiceLine': invoiceLine?.toJson(),
      'party': party?.toJson(),
      'paymentInfo': paymentInfo?.toJson(),
      'partialPayment': partialPayment?.toJson(),
      'settlement': settlement?.toJson(),
      'charge': charge?.toJson(),
      'deduction': deduction?.toJson(),
      'terms': terms?.toJson(),
      'contract': contract?.toJson(),
      'orderInfo': orderInfo?.toJson(),
      'transportInfo': transportInfo?.toJson(),
      'carrier': carrier?.toJson(),
    };
  }
}



List<Invoice> invoicesFromJson(String source) {
  final data = jsonDecode(source) as List<dynamic>;
  return data.map((e) => Invoice.fromJson(e as Map<String, dynamic>)).toList();
}



List<InvoiceDTOs> InvoiceDTOssFromJson(String source) {
  final data = jsonDecode(source) as List<dynamic>;
  return data.map((e) => InvoiceDTOs.fromJson(e as Map<String, dynamic>)).toList();
}
class InvoiceDTOs {
  final int id;
  final String InvoiceNumber;
  final DateTime IssueDate;
  final String SellerName;
  final String BuyerName;
  final double TotalAmount;

  InvoiceDTOs({
    required this.id,
    required this.InvoiceNumber,
    required this.IssueDate,
    required this.SellerName,
    required this.BuyerName,
    required this.TotalAmount,
  });

  factory InvoiceDTOs.fromJson(Map<String, dynamic> json) {
    return InvoiceDTOs(
      id: json['id'] ?? 0,
      InvoiceNumber: json['invoiceNumber'] ?? 0,
      IssueDate: DateTime.tryParse(json['issueDate'] ?? '') ?? DateTime(1970),
      SellerName: json['sellerName'] ?? "name",
      BuyerName: json['buyerName'] ?? "name",
      TotalAmount: json['totalAmount'] ?? 0,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'InvoiceNumber': InvoiceNumber,
      'IssueDate': IssueDate.toIso8601String(),
      'SellerName': SellerName,
      'BuyerName': BuyerName,
      'TotalAmount': TotalAmount,
    };
  }
}