import 'package:http/http.dart' as http;
import 'invoiceModels.dart';
import 'dart:convert';


Future<List<InvoiceDTOs>> apiInvoiceGetAllInvoicesDTOs() async {
  try {
    final response = await http.get(Uri.parse('http://192.168.101.25/api/Invoice/GetAllInvoicesDTOs'));

    if (response.statusCode != 200) {
      throw Exception('Network response was not ok: ${response.statusCode}');
    }

    final List<dynamic> jsonList = json.decode(response.body);
    return jsonList.map((json) => InvoiceDTOs.fromJson(json)).toList();
  } catch (error) {
    print('Error calling apiInvoiceGetAllInvoicesDTOs: $error');
    rethrow;
  }
}
Future<List<Invoice>> apiInvoiceGetAllInvoices() async {
  try {
    final response = await http.get(Uri.parse('http://192.168.101.25/api/Invoice/GetAllInvoices'));

    if (response.statusCode != 200) {
      throw Exception('Network response was not ok: ${response.statusCode}');
    }

    final List<dynamic> jsonList = json.decode(response.body);
    return jsonList.map((json) => Invoice.fromJson(json)).toList();
  } catch (error) {
    print('Error calling apiInvoiceGetAllInvoices: $error');
    rethrow;
  }
}


