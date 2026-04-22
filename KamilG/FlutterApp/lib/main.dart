// lib/main.dart

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

import 'features/router/app_router.dart';

void main() {
  runApp(
    const ProviderScope(
      child: MyApp(),
    ),
  );
}

class MyApp extends ConsumerWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Pobieramy GoRouter z providera. [web:37][web:34]
    final router = ref.watch(goRouterProvider);

    return MaterialApp.router(
      title: 'GoRouter Bottom Nav',
      theme: ThemeData(
        useMaterial3: true, // potrzebne dla NavigationBar (Material 3). [web:1]
        colorSchemeSeed: Colors.indigo,
      ),
      routerConfig: router, // zamiast home: używamy routera. [web:26][web:29]
    );
  }
}