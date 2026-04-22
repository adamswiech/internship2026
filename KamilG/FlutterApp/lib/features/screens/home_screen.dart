// lib/features/navigation/screens/home_screen.dart

import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            // Przykładowa nawigacja wewnątrz brancha:
            // context.go('/home/details');
            // (musiałbyś dodać child-route w branchu home). [web:18][web:33]
          },
          child: const Text('Home screen'),
        ),
      ),
    );
  }
}