CONVERTIR PNG, BMP, GIF Y PDF A JPG
# Aplicación de Windows Forms que convierte archivos PNG, BMP, GIF y PDF a JPG.
# Cada botón (PDF, PNG, BMP, GIF) abre un diálogo para elegir el archivo y guarda el JPG en la misma carpeta, con el mismo nombre.
# Toda la conversión la hace un solo método: procesarImagen, en Form1.cs.



ESTRUCTURA DEL CÓDIGO (Form1.cs)
#   convertirPDF, convertirPngAJpg,
#   convertirBmpAJpg, convertirGifAJpg				Eventos de los botones. Solo llaman a convertirImagenAJpg con el filtro de su formato.
#   convertirImagenAJpg(filtro)						Abre el diálogo, lee el archivo, llama a procesarImagen, guarda el JPG y muestra los mensajes.
#   procesarImagen(bitsImagen, formato)				Recibe el archivo en Base64 y su extensión, y devuelve el JPG en Base64.
#
# Flujo:
#   Botón -> convertirImagenAJpg(filtro) -> procesarImagen(base64, extension) -> se guarda el JPG en disco



MÉTODO PROCESARIMAGEN
# Firma:
#   public string procesarImagen(string bitsImagen, string formato)
#
# Parámetros:
#   bitsImagen										El contenido del archivo codificado en Base64.
#   formato											La extensión del archivo: .jpg, .png, .bmp, .gif o .pdf.
#													Acepta mayúsculas y minúsculas, con o sin punto (".PNG", "png", etc.).
#
# Devuelve:
#   El JPG codificado en Base64.
#   Si el formato es .jpg, devuelve el mismo Base64 que recibió, sin convertirlo.
#
# Pasos:
#   1. Normaliza la extensión: quita espacios y el punto, y la pasa a minúsculas.
#   2. Si es "jpg", devuelve bitsImagen tal cual.
#   3. Decodifica el Base64 a bytes con Convert.FromBase64String.
#   4. Convierte los bytes en un SKBitmap según el formato:
#        png, bmp, gif		SKBitmap.Decode(datos)						(en GIF solo el primer cuadro)
#        pdf				Conversion.ToImage(stream, page: 0, ...)	(solo la primera página, a 300 DPI)
#        otro				Lanza ArgumentException("Formato no válido...")
#   5. Si la imagen es null (los bytes no son una imagen válida), lanza ArgumentException.
#   6. Pinta un fondo blanco, dibuja la imagen encima y la codifica como JPG con calidad 90.
#   7. Devuelve el JPG en Base64 con Convert.ToBase64String.
#
# Errores:
#   procesarImagen no muestra mensajes; lanza excepciones (ArgumentException, FormatException si el Base64 no es válido).
#   convertirImagenAJpg las atrapa con try/catch y muestra el mensaje en un MessageBox.
#
# Ejemplo de uso:
#   string base64 = Convert.ToBase64String(File.ReadAllBytes(@"C:\fotos\logo.png"));
#   string jpgBase64 = procesarImagen(base64, ".png");
#   File.WriteAllBytes(@"C:\fotos\logo.jpg", Convert.FromBase64String(jpgBase64));



PAQUETES NUGET
# SkiaSharp
#   Librería de gráficos 2D mantenida por Microsoft (proyecto Mono). Lee y escribe PNG, JPG, BMP, GIF, WEBP, entre otros.
#   Se usa para leer las imágenes y codificarlas como JPG.
#   Para agregar al proyecto: "dotnet add package SkiaSharp".
#
# PDFtoImage
#   Convierte páginas de un PDF en imágenes. Usa PDFium (el motor de PDF de Google Chrome) y SkiaSharp por dentro.
#   Se usa para convertir la primera página del PDF en un SKBitmap.
#   Para agregar al proyecto: "dotnet add package PDFtoImage".



CONVERTIR GIF EN FORMATO APNG [ImageSharp] (opcional, no se usa en el proyecto)
# Permite convertir un GIF a un formato PNG animado (APNG). Muchos programas solo muestran el primer cuadro de este formato y no la animación completa.
# Para agregar al proyecto: "dotnet add package SixLabors.ImageSharp".



CALIDAD DE JPG
# La conversión del JPG con calidad de 90 es una opción bastante eficiente, casi no se nota la diferencia y el archivo pesa bastante menos que con 100.



ELEMENTOS DE SKIASHARP USADOS EN EL PROYECTO
# Todos se usan en Form1.cs, dentro del método procesarImagen.
#
# Clases y tipos:
#   SKBitmap										Clase que guarda la imagen en memoria.
#   SKCanvas										Clase que funciona como lienzo para dibujar sobre un SKBitmap.
#   SKData											Clase que guarda los bytes de la imagen codificada.
#   SKColors										Clase estática con colores predefinidos.
#   SKSamplingOptions								Struct con las opciones de muestreo al dibujar.
#   SKEncodedImageFormat							Enum con los formatos de salida (JPG, PNG, etc.).
#
# Constructores:
#   new SKBitmap(ancho, alto)						Crea una imagen vacía del mismo tamaño que la original.
#   new SKCanvas(bitmap)							Crea un lienzo para dibujar sobre el bitmap.
#
# Métodos:
#   SKBitmap.Decode(datos)							Convierte los bytes del archivo en una imagen. Devuelve null si no es una imagen válida.
#   SKCanvas.Clear(color)							Pinta el fondo de un color.
#   SKCanvas.DrawBitmap(imagen, x, y, opciones)		Dibuja la imagen original sobre el lienzo.
#   SKBitmap.Encode(formato, calidad)				Codifica el bitmap como JPG con calidad 90.
#   SKData.ToArray()								Convierte el resultado en byte[] para pasarlo a Base64.
#   Dispose()										Lo llaman los "using" para liberar la memoria.
#
# Propiedades y valores:
#   SKBitmap.Width, SKBitmap.Height					Ancho y alto de la imagen.
#   SKColors.White									Color blanco para el fondo (JPG no admite transparencia).
#   SKSamplingOptions.Default						Opciones de muestreo por defecto.
#   SKEncodedImageFormat.Jpeg						Formato de salida JPG.



CÓMO SE USA SKIASHARP EN EL PROYECTO
# 1. Leer la imagen:
#      SKBitmap.Decode(datos)
#    SkiaSharp detecta el formato por el contenido de los bytes y carga la imagen en memoria.
#    En un GIF animado solo lee el primer cuadro.
#
# 2. Crear una imagen vacía con fondo blanco:
#      new SKBitmap(imagen.Width, imagen.Height)
#      new SKCanvas(fondo)
#      lienzo.Clear(SKColors.White)
#    JPG no admite transparencia. Si se guardara directamente un PNG con partes transparentes, esas partes podrían salir negras.
#    Para solucionarlo, primero se pinta un fondo blanco.
#
# 3. Dibujar la imagen original encima:
#      lienzo.DrawBitmap(imagen, 0, 0, SKSamplingOptions.Default)
#    La imagen se dibuja en la esquina (0, 0), sobre el fondo blanco. Las partes transparentes quedan blancas.
#
# 4. Codificar como JPG:
#      fondo.Encode(SKEncodedImageFormat.Jpeg, 90)
#      Convert.ToBase64String(jpg.ToArray())
#    procesarImagen devuelve el JPG en Base64. convertirImagenAJpg lo decodifica y lo guarda con File.WriteAllBytes
#    en la misma carpeta que el original, con el mismo nombre y extensión .jpg.
#
# Todos los objetos de SkiaSharp van dentro de "using" porque guardan memoria nativa (fuera de .NET) que se tiene que liberar al terminar.



ELEMENTOS DE PDFTOIMAGE USADOS EN EL PROYECTO
# Se usa en Form1.cs, en una sola línea del método procesarImagen:
#   Conversion.ToImage(new MemoryStream(datos), page: 0, options: new(Dpi: 300))
#
# Clases y tipos:
#   Conversion										Clase estática con todos los métodos para convertir PDF a imagen.
#   RenderOptions									Record struct con las opciones de la conversión. Se crea con new(...) sin escribir el nombre.
#
# Métodos:
#   Conversion.ToImage(...)							Convierte una página del PDF en un SKBitmap.
#
# Parámetros de ToImage:
#   pdfStream = new MemoryStream(datos)				El PDF de entrada (los bytes decodificados del Base64).
#   page = 0										La página que se convierte (empieza en 0).
#   options = new(Dpi: 300)							Las opciones de conversión.
#   leaveOpen (no se pasa)							Vale false por defecto: cierra el stream al terminar.
#   password (no se pasa)							Vale null por defecto: por eso los PDF protegidos fallan.
#
# Propiedades:
#   RenderOptions.Dpi = 300							Resolución de la imagen. A más DPI, más píxeles y más calidad. Una hoja carta da unos 2550x3300 píxeles.
#   Las demás propiedades de RenderOptions (Width, Height, Rotation, BackgroundColor, WithAnnotations, etc.) no se usan.
#
# Como ToImage devuelve un SKBitmap, el PDF sigue el mismo camino que las imágenes (fondo blanco y codificación JPG).



DETALLES A TENER EN CUENTA
# El JPG se guarda junto al archivo original. Si ya existe un .jpg con el mismo nombre, se reemplaza sin preguntar.
# La extensión se toma del archivo elegido, no del botón. Con el filtro "Todos los archivos" se puede abrir cualquier formato soportado desde cualquier botón.
# Si el formato es .jpg, el Base64 se devuelve sin validar que el contenido sea realmente un JPG.
# Del PDF solo se convierte la primera página. Para convertir todas habría que usar un ciclo o Conversion.ToImages(...).
# El archivo se carga completo en memoria (byte[] y Base64), así que los archivos muy grandes usan bastante memoria.
# La ventana se congela mientras convierte, porque la conversión se ejecuta en el hilo de la interfaz. Con archivos normales casi no se nota; con páginas muy grandes sí.
