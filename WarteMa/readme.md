# Warte ma!

## Warte ma! Was isn das?

_WarteMa!_ nimmt eine Liste von Dateien entgegen, lauscht auf Änderungen in
diesen Dateien und terminiert sobald es Änderungen in einer dieser Dateien
feststellt.

## Warte ma! Wozu brauch ich das?

_WarteMa!_ kann verwendet werden, um automatisch auf Dateiänderungen zu
reagieren und bspw. Build-Prozesse anzustoßen:

```shell
#!/usr/bin/env fish
while true
    wartema **.cshtml
    npx tailwind --input=tailwind.css --output=wwwroot/app.css
end
```

Warte ma! Wenn man das in einer Konsole nebenher laufen lässt, kann man sich
auf das Schreiben von Code und Markup konzentrieren. Die Artefakte werden
nach jedem Speichern einfach automatisch neu gebaut.

## Warte ma! Das geht doch schon mit inotifywait!?

Genau, aber warte ma!  Leider ist die inotify-API nicht immer verfügbar, zum
Beispiel wenn man in einer Windows-Bude arbeitet, oder wenn man in einem
Docker-Container entwickelt. Für diese Situationen habe ich _WarteMa!_
geschrieben.

_A.C.D., September 2024_
