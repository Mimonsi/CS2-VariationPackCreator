import os
from PIL import Image

# Pfade für Eingabe- und Ausgabeordner
input_folder = "thumbnailsDynamic"
output_folder = "thumbnailsDynamicScaled"

# Ausgabeordner erstellen, falls er nicht existiert
os.makedirs(output_folder, exist_ok=True)

# Größe, auf die Bilder skaliert werden sollen
output_size = (256, 256)

# Alle Dateien im Eingabeordner durchgehen
for filename in os.listdir(input_folder):
    if filename.lower().endswith(".png"):  # Nur PNG-Dateien verarbeiten
        input_path = os.path.join(input_folder, filename)
        output_path = os.path.join(output_folder, filename)
        try:
            # Bild öffnen und skalieren
            with Image.open(input_path) as img:
                img = img.resize(output_size, Image.Resampling.LANCZOS)
                img.save(output_path, format="PNG")
            print(f"Erfolgreich verarbeitet: {filename}")
        except Exception as e:
            print(f"Fehler beim Verarbeiten von {filename}: {e}")
