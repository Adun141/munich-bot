# Munich Navigator Telegram Bot

Ein interaktiver asynchroner Telegram-Bot für Expats und Neuankömmlinge in München, entwickelt mit der offiziellen `Telegram.Bot`-Bibliothek.

## Technische Merkmale
* **Asynchrone Programmierung:** Verwendung von `async/await` und `Task`, um blockierungsfreie Netzwerkanfragen zu gewährleisten.
* **Null-Safety & Stabilität:** Einsatz von Pattern Matching (`is not { } message`), um unvorhersehbare API-Updates (wie Sticker oder Medien) abzufangen und `NullReferenceException`-Abstürze zu verhindern.
* **UI/UX Design:** Integration von interaktiven Inline-Tastaturen (`InlineKeyboardMarkup`) für eine intuitive Menüführung.
* **Architektur (Clean Code):** Trennung von Zuständigkeiten durch Auslagerung aller Textantworten in eine separate Klasse mit statischen Methoden (`BotResponses`).
* **Robustes Logging:** Erweiterte Fehlerbehandlung im `HandleErrorAsync` zur Differenzierung zwischen Telegram-API-Fehlern (`ApiRequestException`) und Systemstörungen.
