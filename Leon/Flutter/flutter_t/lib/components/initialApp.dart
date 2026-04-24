import 'package:flutter/material.dart';



class initialApp extends StatefulWidget {
  const initialApp({super.key});

  @override
  State<initialApp> createState() => _initialAppState();
}

class _initialAppState extends State<initialApp> {
  final FocusNode focusNode = FocusNode();
  String s = "";
  String T = "";

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(T),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Focus(
              child: KeyboardListener(
                focusNode: focusNode,
                autofocus: true,
                onKeyEvent: (event) {
                  setState(() {
                    s = event.logicalKey.keyLabel;
                  });
                },
                child: FloatingActionButton.extended(
                  onPressed: () => callback(s),
                  label: Text(s.isEmpty ? "Press a key" : s),
                ),
              ),
            ),
            const SizedBox(height: 20),
            textF(),
          ],
        ),
      ),
    );
  }

  Widget textF() {
    return SizedBox(
      height: 60,
      width: 200,
      child: TextField(
        decoration: const InputDecoration(
          border: OutlineInputBorder(),
          labelText: "Textfield",
        ),
        onChanged: (text) {
          setState(() {
            T = text;
          });
        },
        onEditingComplete: () {
          focusNode.requestFocus();
        },
        onTapOutside: (_) {
          focusNode.requestFocus();
        },
      ),
    );
  }

  void callback(String s) {
    print(s);
  }
}

