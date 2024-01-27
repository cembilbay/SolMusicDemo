using Bitirme_projesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Python.Runtime;

namespace Bitirme_projesi.Controllers
{
    [AllowAnonymous]
    public class RecommendController : Controller
    {
        private const string CsvFilePath = "C:\\Users\\ASUS\\source\\repos\\Bitirme_projesi\\Bitirme_projesi\\wwwroot\\python\\turkish_song_lyrics.csv";
        private const string PythonScriptPath = "C:\\Users\\ASUS\\source\\repos\\Bitirme_projesi\\Bitirme_projesi\\wwwroot\\python\\recommendation.py";
        private static bool pythonEngineInitialized = false;
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult PartialRecommend(string selectedSong)
        {
            

                dynamic recommendations = RunPythonScript(selectedSong);
                Console.WriteLine(recommendations);

                string jsonText = System.IO.File.ReadAllText("recommended_songs.json");

                
                List<RecommendedSongModel> recommendedSongs = JsonConvert.DeserializeObject<List<RecommendedSongModel>>(jsonText);
               

                return Json(recommendedSongs);
           
        }



    
        private dynamic RunPythonScript(string selectedSong)
        {
            if (!pythonEngineInitialized)
            {
                Runtime.PythonDLL = @"C:\Users\ASUS\AppData\Local\Programs\Python\Python312\python312.dll";
                PythonEngine.Initialize();
                PythonEngine.BeginAllowThreads();
            }

            using (Py.GIL())
            {
                if (!pythonEngineInitialized)
                {
                    dynamic sys = Py.Import("sys");
                    sys.path.append(@"C:\Users\ASUS\source\repos\Bitirme_projesi\Bitirme_projesi\wwwroot\python");
                    pythonEngineInitialized = true;
                }
                
                dynamic pythonScript = Py.Import("recommendation");
                
                dynamic recommendations = pythonScript.recommend(selectedSong);
                return recommendations;
            }
        }



        private List<MusicModel> ReadCsv(string filePath)
        {
            List<MusicModel> musicItems = new List<MusicModel>();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 4) 
                    {
                        var musicItem = new MusicModel
                        {

                            SongName = values[1],
                            Artist = values[2],
                            SongContent = values[3]
                        };

                        musicItems.Add(musicItem);
                    }
                }
            }

            return musicItems;
        }

    }
}
