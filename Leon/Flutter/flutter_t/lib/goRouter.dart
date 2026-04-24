import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';
import 'components/initialApp.dart';
import 'components/invoiceApi.dart';
import 'components/Home.dart';



class AppRoutes {
  AppRoutes._();

  static const String home = '/';
  static const String initialApp = '/initialApp';
  static const String invoiceApi = '/invoiceApi';
}

final routerKey = GlobalKey<NavigatorState>();

final router = GoRouter(
  navigatorKey: routerKey,
  initialLocation: AppRoutes.home,
  routes: [
    StatefulShellRoute.indexedStack(
      builder: (context, state, navigationShell) {
        return Scaffold(
          body: navigationShell,
          bottomNavigationBar: BottomNavigationBar(
            items: const [
              BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
              BottomNavigationBarItem(icon: Icon(Icons.looks_one), label: 'Initial App'),
              BottomNavigationBarItem(icon: Icon(Icons.api), label: 'Invoice API'),
            ],
            currentIndex: navigationShell.currentIndex,
            onTap: (index) {
              navigationShell.goBranch(
                index,
                initialLocation: index == navigationShell.currentIndex,
              );
            },
          ),
        );
      },
        branches: [
          StatefulShellBranch(
            routes: [
              GoRoute(
                path: AppRoutes.home,
                builder: (context, state) => const Home(),
              ),
            ],
          ),
          StatefulShellBranch(
            routes: [
              GoRoute(
                path: AppRoutes.initialApp,
                builder: (context, state) => const initialApp(),
              ),
            ],
          ),
          StatefulShellBranch(
            routes: [
              GoRoute(
                path: AppRoutes.invoiceApi,
                builder: (context, state) => const InvoiceApi(),
              ),
            ],
          ),
        ],
    ),
  ],
);
