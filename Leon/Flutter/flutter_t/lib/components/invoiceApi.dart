import 'package:flutter/material.dart';
import 'invoiceApi/invoiceRequest.dart';
import 'invoiceApi/invoiceModels.dart';
import 'offlineDB/offlineDB.dart';



class InvoiceApi extends StatefulWidget {
  const InvoiceApi({super.key});
  @override
  State<InvoiceApi> createState() => _InvoiceApiState();
}

class _InvoiceApiState extends State<InvoiceApi> {
  late Future<List<InvoiceDTOs>> invoicesFuture;

  @override
  void initState() {
    super.initState();


    final db = OfflineDbHelper.instance;



    invoicesFuture = apiInvoiceGetAllInvoicesDTOs();
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder<List<InvoiceDTOs>>(
      future: invoicesFuture,
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return const Center(child: CircularProgressIndicator());
        }
        if (snapshot.hasError) {
          return Center(child: Text("Error: ${snapshot.error}"));
        }
        final invoices = snapshot.data ?? [];
        if (invoices.isEmpty) {
          return const Center(child: Text("No invoices found"));
        }

        return ListView.builder(
          itemCount: invoices.length,
          shrinkWrap: true,
          scrollDirection: Axis.vertical,
          itemBuilder: (context, index) {
            final inv = invoices[index];

            return ListTile(
              title: Text("Invoice #${inv.InvoiceNumber}"),
              subtitle: Text(
                "ID: ${inv.id}\n"
                    "Issue Date: ${inv.IssueDate.toIso8601String()}\n"
                    "Seller: ${inv.SellerName}\n"
                    "Buyer: ${inv.BuyerName}\n"
                    "Total: ${inv.TotalAmount}",
              ),
            );
          },
        );
      },
    );
  }
}



