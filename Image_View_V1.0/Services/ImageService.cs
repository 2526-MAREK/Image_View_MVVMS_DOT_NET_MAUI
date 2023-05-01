using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.Controls;
using System.Reflection;
using Image_View_V1._0.Helpers;

namespace Image_View_V1._0.Services
{
    public class ImageService
    {
        ImageToProcessHelper imageHelper;
        public ImageService(ImageToProcessHelper imageHelper)
        {
            this.imageHelper = imageHelper;
        }

        private async Task<ImageSource> GetImageSource(System.IO.Stream stream)
        {
            // Tworzenie MemoryStream z oryginalnego strumienia
            MemoryStream memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0; // Ustawienie pozycji na początek strumienia

            ImageSource imageSourceTemp;
            // Użycie MemoryStream zamiast oryginalnego strumienia
            imageSourceTemp = new StreamImageSource { Stream = cancellationToken => Task.FromResult((Stream)memoryStream) };

            return imageSourceTemp;

        }
        public async Task<ImageToProcess> GetImage()
        {
            ImageToProcess imageToProcess = new();

            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
              {
                  {DevicePlatform.WinUI, new[] {".png"} }
                    //{DevicePlatform.MacCatalyst, new[] { "public.image"} }
              })
            });

            if(result == null)
                return imageToProcess;

            using var stream1 = await result.OpenReadAsync();
            using var stream2 = await result.OpenReadAsync();
            using var stream3 = await result.OpenReadAsync();

            SaveImage(stream1);

            imageToProcess.ImageSrcMain = await GetImageSource(stream2);
            imageToProcess.ImageSrcMainBytes = await imageHelper.ImageSourceToByteArrayAsync(await GetImageSource(stream3));

            //imageToProcess.ImageSrcThumbnail = await GetImageSource(stream3);

            return imageToProcess;
        }

        private async void SaveImage(System.IO.Stream stream)
        {
            using var memory_stream = new MemoryStream();

            stream.CopyTo(memory_stream);

            stream.Position = 0;
            memory_stream.Position = 0;

            await System.IO.File.WriteAllBytesAsync(
                @"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Resources\Images\photo_processed.png", memory_stream.ToArray());
        }

        public async Task<T> GetChunkData<T>(string filePath)
        {
            T chunkData = default(T);

            var contents = await File.ReadAllTextAsync(filePath);
            chunkData = JsonConvert.DeserializeObject<T>(contents);

            return chunkData;
        }

        public async Task<ChunktIME> GetChunktIME()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\tIME.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunktIME>(filePath);
            }
            else
            {
                return null;
            }
        }

        public async Task<ChunktEXt> GetChunktEXt()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\tEXt.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunktEXt>(filePath);
            }
            else
            {
                return null;
            }
        }

        //można i utworzyć generyczną metodę 
        public async Task<T> GetChunkDataByName<T>(string fileName)
        {
            string filePath = $@"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Resources\Raw\{fileName}.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<T>(filePath);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<ChunksTER> GetChunksTER()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\sTER.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunksTER>(filePath);
            }
            else
            {
                return null;
            }
        }

        public async Task<ChunksRGB> GetChunksRGB()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\sRGB.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunksRGB>(filePath);
            }
            else
            {
                return null;
            }
        }

        public async Task<ChunksPLT> GetChunksPLT()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\sPLT.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunksPLT>(filePath);
            }
            else
            {
                return null;
            }
        }

        public async Task<ChunksBIT> GetChunksBIT()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\sBIT.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunksBIT>(filePath);
            }
            else
            {
                return null;
            }
        }

        public async Task<ChunkpHYs> GetChunkpHYs()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\pHYs.json";

            if (File.Exists(filePath))
            {
                return await GetChunkData<ChunkpHYs>(filePath);
            }
            else
            {
                return null;
            }

        }


        //Parsowanie Chunków na dwa sposoby:

        public async Task<ChunkoFFs> GetChunkoFFs()
        {

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\oFFs.json";

            if (File.Exists(filePath))
            {
                //1 sposob
                return await GetChunkData<ChunkoFFs>(filePath);
            }
            else
            {
                return null;
            }
            }

        public async Task<ChunkiTXt> GetChunkiTXt()
        {
            ChunkiTXt ChTemp = new();

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\iTXt.json";

            if (File.Exists(filePath))
            {
                var contents = await File.ReadAllTextAsync(filePath);

                //2 sposob
                JObject jObject = JObject.Parse(contents);
            //{"Keyword": "Author", "CompressionFlag": 0, "CompressionMethod": 0,
            //"LanguageTag": "ja", "TranslatedKeyword": "\u4f5c\u8005", "Text":
            //"\u30d7\u30ed\u30b8\u30a7\u30af\u30c8\u540d\u96ea"}
            ChTemp.Keyword = (string)jObject["Keyword"];
            ChTemp.CompressionFlag = (int)jObject["CompressionFlag"];
            ChTemp.CompressionMethod = (int)jObject["CompressionMethod"];
            ChTemp.LanguageTag = (string)jObject["LanguageTag"];
            ChTemp.TranslatedKeyword = (string)jObject["TranslatedKeyword"];
            ChTemp.Text = (string)jObject["Text"];

            //Debug.WriteLine($"Przetworzone dane: Szerokość = {ChTemp.Width}, Wysokość = {ChTemp.Height}, Głębia bitów = {ChTemp.BitDepth}");

            return ChTemp;
            }
            else
            {
                return null;
            }

        }

        public async Task<ChunkgAMA> GetChunkgAMA()
        {
            ChunkgAMA ChTemp = new();

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\gAMA.json";

            if (File.Exists(filePath))
            {
                var contents = await File.ReadAllTextAsync(filePath);

                JObject jObject = JObject.Parse(contents);

                ChTemp.Gamma = (int)jObject["Gamma"];

                return ChTemp;
            }
            else
            {
                return null;
            }
        }

        public async Task<ChunkhIST> GetChunkhIST()
        {
            ChunkhIST ChTemp = new();

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\hIST.json";

            if (File.Exists(filePath))
            {
                var contents = await File.ReadAllTextAsync(filePath);

            JObject jObject = JObject.Parse(contents);

            ChTemp.Histogram = jObject["Histogram"].ToObject<List<int>>();

            return ChTemp;
            }
            else
            {
                return null;
            }
        }
            public async Task<ChunkIHDR> GetChunkIHDR()
        {
            ChunkIHDR ChTemp = new();

            /*FileSystem.OpenAppPackageFileAsync() - Ta metoda jest częścią klasy FileSystem, która jest 
             * przeznaczona do odczytu plików wewnątrz 
             * pakietu aplikacji. Pakiet aplikacji to zestaw plików, z których składa się aplikacja, 
             * takich jak zasoby, pliki konfiguracyjne, itp. Zwykle pliki w pakiecie aplikacji
             * są tylko do odczytu i nie są przeznaczone do częstego aktualizowania.*/

            /* Ta metoda jest częścią klasy File, która służy do zarządzania plikami 
             * na dysku. Metoda File.ReadAllTextAsync() 
             * jest przeznaczona do odczytu plików, które mogą być często aktualizowane.*/

            /*using var stream = await FileSystem.OpenAppPackageFileAsync("IHDR.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();*/

            string filePath = @"C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\IHDR.json";

            if (File.Exists(filePath))
            {
                var contents = await File.ReadAllTextAsync(filePath);

            
            //Debug.WriteLine("Zawartość pliku IHDR.json przed przetworzeniem: " + contents);
            JObject jObject = JObject.Parse(contents);
            //"bit_depth": 8, "color_type": 6, "compression_method": 0, "filter_method": 0, "interlace_method": 0}
            ChTemp.Width = (int)jObject["width"];
            ChTemp.Height = (int)jObject["height"];
            ChTemp.BitDepth = (int)jObject["bit_depth"];
            ChTemp.ColorType = (int)jObject["color_type"];
            ChTemp.CompressionMethod = (int)jObject["compression_method"];
            ChTemp.FilterMethod = (int)jObject["filter_method"];
            ChTemp.InterlaceMethod = (int)jObject["interlace_method"];


            //Debug.WriteLine($"Przetworzone dane: Szerokość = {ChTemp.Width}, Wysokość = {ChTemp.Height}, Głębia bitów = {ChTemp.BitDepth}");

            return ChTemp;
            }
            else
            {
                return null;
            }

        }

        private ImageSource LoadImageFromResource(string imagePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //ImageSource source = new FileImageSource { File = $"resource://{assembly.GetName().Name}.{imagePath}" };
            ImageSource source = new FileImageSource { File = imagePath };

            return source;
        }

        public ImageSource GetImageFFTFromProcess()
        {
             string imagePath = @"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Resources\Images\fft.png"; // Ścieżka do zdjęcia w folderze projektu
            ImageSource source;

            if (File.Exists(imagePath))
            {
               source = LoadImageFromResource(imagePath); //new FileImageSource { File = @"C:\Users\marek\source\repos\Image_View_V1.0\Image_View_V1.0\Resources\Images\fft.png" };
            }
            else
            {
                imagePath = @"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Resources\Images\fftdontexists.png";
                source = LoadImageFromResource(imagePath);
            }
            
            return source;
        }

        public ImageSource GetImageHistFromProcess()
        {
            string imagePath = @"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Resources\Images\hist.png"; // Ścieżka do zdjęcia w folderze projektu

            ImageSource source = LoadImageFromResource(imagePath); //new FileImageSource { File = @"C:\Users\marek\source\repos\Image_View_V1.0\Image_View_V1.0\Resources\Images\fft.png" };

            return source;
        }

        public ImageSource GetImageMiniatureFromProcess()
        {
            string imagePath = @"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Resources\Images\miniature.png"; // Ścieżka do zdjęcia w folderze projektu

            ImageSource source = LoadImageFromResource(imagePath); //new FileImageSource { File = @"C:\Users\marek\source\repos\Image_View_V1.0\Image_View_V1.0\Resources\Images\fft.png" };

            return source;
        }

        public async Task RunPythonToImageProcess()
        {
            // 1) Create Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\marek\AppData\Local\Programs\Python\Python311\python.exe";

            // 2) Provide script and arguments
            var script = @"C:\Users\marek\OneDrive\Dokumenty\GitHub\Image_Viewer\Image_View_MVVC\Image_View_V1.0\Model\PythonScripts\ImageProcess.py";
            var start = "2019-1-1";
            var end = "2019-1-22";

            psi.Arguments = $"\"{script}\" \"{start}\" \"{end}\"";

            // 3) Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            // 5) Display output
            Debug.WriteLine("ERRORS:");
            Debug.WriteLine(errors);
            Debug.WriteLine("\n");
            Debug.WriteLine("Results:");
            Debug.WriteLine(results);

        }
    }

    

    }
