CONVERTIR JPG, BMP, GIF, ICO, WEBP (SkiaSharp)
# Para la conversión de JPG, BMP y GIF a PNG se utiliza el paquete NuGET "SkiaSharp" desarrollado por Sungalia en GitHub.
# Se eligió SkiaSharp porque es una libreria que permite hacer conversiones de formatos JPG, PDF, BMP y GIF.
# Para agregar al proyecto: "dotnet add package SkiaSharp".



CONVERTIR GIF EN FORMATO APNG (ImageSharp)
# Permite un GIF a un formato animado PNG (APNG). Muchos programas solo muestran el primer cuadro de este formato y no la animación completa.
# Se agrega el paquete con "dotnet add package SixLabors.ImageSharp" desde la terminal.



CALIDAD DE JPG
# 90 es una opción bastante eficiente, casi no se nota la diferencia y el archivo pesa bastante menos que con 100.



ELEMENTOS DE SKIASHARP USADOS EN EL PROYECTO
# Todos se usan en Form1.cs, dentro del método convertirImagenAJpg.
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
#   SKBitmap.Decode(ruta)							Lee la imagen desde el disco.
#   SKCanvas.Clear(color)							Pinta el fondo de un color.
#   SKCanvas.DrawBitmap(imagen, x, y, opciones)		Dibuja la imagen original sobre el lienzo.
#   SKBitmap.Encode(formato, calidad)				Codifica el bitmap como JPG con calidad 90.
#   SKData.ToArray()								Convierte el resultado en byte[] para guardarlo.
#   Dispose()										Lo llaman los "using" para liberar la memoria.
#
# Propiedades y valores:
#   SKBitmap.Width, SKBitmap.Height					Ancho y alto de la imagen.
#   SKColors.White									Color blanco para el fondo (JPG no admite transparencia).
#   SKSamplingOptions.Default						Opciones de muestreo por defecto.
#   SKEncodedImageFormat.Jpeg						Formato de salida JPG.




CÓMO SE USA SKIASHARP EN EL PROYECTO
# SkiaSharp se usa para convertir imágenes PNG, BMP y GIF a JPG.
# Todo está en Form1.cs, en el método convertirImagenAJpg.
# Los botones convertirPngAJpg, convertirBmpAJpg y convertirGifAJpg llaman a ese mismo método; solo cambia el filtro del diálogo para abrir archivos.
#
# 1. Leer la imagen:
#      SKBitmap.Decode(origen)
#    SkiaSharp detecta el formato solo y carga la imagen en memoria.
#    Si el archivo no es una imagen válida devuelve null y se muestra un error.
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
# 4. Codificar como JPG y guardar:
#      fondo.Encode(SKEncodedImageFormat.Jpeg, 90)
#      File.WriteAllBytes(destino, jpg.ToArray())
#    La imagen se guarda en la misma carpeta que el original, con el mismo nombre y extensión .jpg.
#
# Todos los objetos de SkiaSharp van dentro de "using" porque guardan memoria nativa (fuera de .NET) que se tiene que liberar al terminar.
#
# La conversión de PDF a JPG (método convertirPDF) no usa SkiaSharp directamente, usa el paquete PDFtoImage.
# Pero PDFtoImage usa SkiaSharp por dentro para crear la imagen de la página.





ELEMENTOS DE PDFTOIMAGE USADOS EN EL PROYECTO
# Todos se usan en Form1.cs, en una sola línea del método convertirPDF:
#   Conversion.SaveJpeg(destino, pdf, page: 0, options: new(Dpi: 300));
#
# Clases y tipos:
#   Conversion										Clase estática con todos los métodos para convertir PDF a imagen.
#   RenderOptions									Record struct con las opciones de la conversión. Se crea con new(...) sin escribir el nombre.
#
# Métodos:
#   Conversion.SaveJpeg(...)						Convierte una página del PDF y la guarda directamente como archivo JPG.
#
# Parámetros de SaveJpeg:
#   imageFilename = destino							Ruta del JPG que se va a crear.
#   pdfStream = pdf									El PDF de entrada (FileStream).
#   page = 0										La página que se convierte (empieza en 0).
#   options = new(Dpi: 300)							Las opciones de conversión.
#   leaveOpen (no se pasa)							Vale false por defecto: cierra el stream al terminar.
#   password (no se pasa)							Vale null por defecto: por eso los PDF protegidos fallan.
#
# Propiedades:
#   RenderOptions.Dpi = 300							Resolución de la imagen. A más DPI, más píxeles y más calidad.
#   Las demás propiedades de RenderOptions (Width, Height, Rotation, BackgroundColor, WithAnnotations, etc.) no se usan.




CÓMO SE USA PDFTOIMAGE EN EL PROYECTO
# PDFtoImage se usa para convertir la primera página de un PDF a JPG.
# Todo está en Form1.cs, en el método convertirPDF.
# Para agregar al proyecto: "dotnet add package PDFtoImage".
#
# 1. Elegir el PDF:
#    Se abre un OpenFileDialog filtrado a *.pdf y la ruta se muestra en txtContenido.
#    Si el usuario cancela, el método termina.
#
# 2. Elegir la carpeta de destino:
#    A diferencia de las imágenes, que se guardan junto al original, aquí se pide la carpeta con un FolderBrowserDialog.
#
# 3. Abrir el PDF como stream:
#      File.OpenRead(origen)
#    El archivo se pasa como Stream, no como byte[], así que no se carga entero en un arreglo antes de convertirlo.
#
# 4. Armar la ruta de salida:
#    El JPG se llama <nombre del PDF>_pagina_1.jpg y se guarda en la carpeta elegida.
#
# 5. Convertir:
#      Conversion.SaveJpeg(destino, pdf, page: 0, options: new(Dpi: 300))
#    Es la única llamada a PDFtoImage en todo el proyecto.
#      Conversion           Clase estática de PDFtoImage con los métodos de conversión.
#      SaveJpeg(...)        Convierte una página y la guarda directamente como archivo JPG.
#      destino              Ruta del JPG que se va a crear.
#      pdf                  El stream del PDF.
#      page: 0              La página que se convierte. Empieza en 0, así que 0 es la primera.
#      new(Dpi: 300)        Crea un RenderOptions con resolución de 300 DPI. Una hoja carta da unos 2550x3300 píxeles.
#
# 6. Avisar o mostrar el error:
#    Si sale bien se muestra la ruta del JPG.
#    Si falla (PDF dañado, protegido con contraseña, sin permisos, etc.), el catch muestra el mensaje de error.
#
# Cómo funciona por dentro:
#    PDFium (el motor de PDF de Google Chrome) lee el PDF y dibuja la página.
#    SkiaSharp guarda esa página como imagen y la codifica como JPG.
#
# Detalles a tener en cuenta:
#    Solo se convierte la primera página. Para convertir todas habría que repetir la conversión con un ciclo o usar Conversion.ToImages(...).
#    La ventana se congela mientras convierte, porque la conversión se ejecuta en el hilo de la interfaz. Con PDFs normales casi no se nota; con páginas muy grandes sí.
#    SaveJpeg cierra el stream al terminar (su parámetro leaveOpen vale false por defecto) y luego el "using" intenta cerrarlo otra vez. Cerrar dos veces no da error.