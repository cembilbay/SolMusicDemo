using Bitirme_projesi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bitirme_projesi.Controllers
{
    [AllowAnonymous]
    public class HomePageController : Controller
    {
        private const string CsvFilePath = "C:\\Users\\ASUS\\source\\repos\\Bitirme_projesi\\Bitirme_projesi\\wwwroot\\python\\turkish_song_lyrics.csv";
        public IActionResult Index()
        {
            var musicItems = ReadCsv(CsvFilePath);
            var songList = musicItems.Select(item => item.SongName).ToArray();

            ViewBag.SongList = songList;
            return View();
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

                    if (values.Length == 4) // assuming 4 columns: ID, SongName, Artist, SongContent
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
