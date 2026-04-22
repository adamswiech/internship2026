import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

// Shell z NavigationBar oparty na przykładzie go_router/StatefulNavigationShell. [web:18][web:36]
class ScaffoldWithNavBar extends StatelessWidget {
  const ScaffoldWithNavBar({
    super.key,
    required this.navigationShell,
  });

  final StatefulNavigationShell navigationShell;

  void _onDestinationSelected(int index) {
    navigationShell.goBranch(
      index,
      // Jeśli klikniesz aktywną zakładkę, wrócisz do jej initialLocation. [web:18][web:36]
      initialLocation: index == navigationShell.currentIndex,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // navigationShell renderuje aktualny branch jako body. [web:18][web:36]
      body: navigationShell,
      bottomNavigationBar: NavigationBar(
        selectedIndex: navigationShell.currentIndex,
        onDestinationSelected: _onDestinationSelected,
        destinations: const [
          NavigationDestination(
            icon: Icon(Icons.home_outlined),
            selectedIcon: Icon(Icons.home),
            label: 'Home',
          ),
          NavigationDestination(
            icon: Icon(Icons.search),
            selectedIcon: Icon(Icons.search_rounded),
            label: 'Search',
          )
        ],
      ),
    );
  }}
