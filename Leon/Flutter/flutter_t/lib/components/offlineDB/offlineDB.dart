import 'package:flutter_t/components/invoiceApi/invoiceModels.dart';
import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';
import 'db.dart';

class OfflineDbHelper {
  static final OfflineDbHelper instance = OfflineDbHelper._internal();
  OfflineDbHelper._internal();

  Database? _db;

  Future<Database> get database async {
    if (_db != null) return _db!;
    _db = await _initDb();
    return _db!;
  }

  Future<Database> _initDb() async {
    final dbPath = await getDatabasesPath();
    final path = join(dbPath, 'offline.db');

    return await openDatabase(
      path,
      version: 1,
      onConfigure: (db) async {
        await db.execute("PRAGMA foreign_keys = ON;");
      },
      onCreate: (db, version) async {
        await _runMigration(db);
      },
    );
  }

  Future<void> _runMigration(Database db) async {
    final statements = migrationScript.split(";");

    for (final stmt in statements) {
      final sql = stmt.trim();
      if (sql.isNotEmpty) {
        await db.execute(sql);
      }
    }
  }
}

extension InvoiceDao on OfflineDbHelper {
  Future<int> insertInvoice(Map<String, Invoice> data) async {
    final db = await database;
    return await db.insert("Invoice", data);
  }

  Future<List<Map<String, dynamic>>> getInvoices() async {
    final db = await database;
    return await db.query("Invoice", orderBy: "Id DESC");
  }
}
