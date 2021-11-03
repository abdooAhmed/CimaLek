using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CimaLek.Data;
using Entertment.Models;
using Microsoft.AspNetCore.Http;
using CimaLek.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CimaLek.Controllers
{
    public class seriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment WebEnvironment;

        public seriesController(ApplicationDbContext context, IWebHostEnvironment e)
        {
            _context = context;
            WebEnvironment = e;
        }

        // GET: series
        public async Task<IActionResult> Index()
        {
            List<SeriesData> seriesDatas = new List<SeriesData>();
            var series = await _context.serie.ToListAsync();

            int x = 0;
            foreach (var serie in series)
            {
                
                var s = _context.serie.Where(x=>x.seriesId==serie.seriesId).Select(team => new {
                    
                    PlayersOlder20 = team.serieTypes.Where(x => x.serieId == serie.seriesId).Select(x => x.type)
                }).ToList();
                
                var n = s[0].PlayersOlder20.ToList();
                var date = n.Count();
                string termsList = "";
                for (var count = 0; count < date; count++)
                {
                    var l = n[count].type;
                    termsList = n[count].type + "," + termsList;
                    var c = count;
                }
                seriesDatas.Add(new SeriesData
                {
                    seriesId = serie.seriesId,
                    name = serie.name,
                    Describtion = serie.Describtion,
                    country = serie.country,
                    CreateDate = serie.CreateDate,

                    rate = serie.rate,
                    time = serie.time,
                    type = termsList
                });
            }
            var si = seriesDatas;

            
            
            
            return View(seriesDatas);
        }

        public async Task<IActionResult> SeriesHome()
        {

            List<SeriesData> seriesDatas = new List<SeriesData>();
            var series = await _context.serie.ToListAsync();

            int x = 0;
            foreach (var serie in series)
            {

                var s = _context.serie.Where(x => x.seriesId == serie.seriesId).Select(team => new {

                    PlayersOlder20 = team.serieTypes.Where(x => x.serieId == serie.seriesId).Select(x => x.type)
                }).ToList();

                var n = s[0].PlayersOlder20.ToList();
                var date = n.Count();
                string termsList = "";
                for (var count = 0; count < date; count++)
                {
                    var l = n[count].type;
                    termsList = n[count].type + "," + termsList;
                    var c = count;
                }
                seriesDatas.Add(new SeriesData
                {
                    seriesId = serie.seriesId,
                    name = serie.name,
                    Describtion = serie.Describtion,
                    country = serie.country,
                    CreateDate = serie.CreateDate,
                    imageUrl = serie.image,
                    rate = serie.rate,
                    time = serie.time,
                    type = termsList
                });
            }
            var si = seriesDatas;




            return View(seriesDatas);
        }
        // GET: series/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.serie
                .FirstOrDefaultAsync(m => m.seriesId == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // GET: series/Create
        public IActionResult AddSeries()
        {
            ViewData["typeId"] = new SelectList(_context.filmSeriesTypes, "typeID", "type");
            return View();
        }

        // POST: series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSeries(SeriesData seriesData)
        {
            if (ModelState.IsValid)
            {

                var file = UploadFile(seriesData.image, seriesData.name);
                string[] subs = seriesData.type.Split(',');
                var series = new serie
                {
                    seriesId = Guid.NewGuid().ToString(),
                    name = seriesData.name,
                    Describtion = seriesData.Describtion,
                    country = seriesData.country,
                    CreateDate = seriesData.CreateDate,
                    image = file,
                    rate = seriesData.rate,
                    time = seriesData.time
                };
                _context.Add(series);
                await _context.SaveChangesAsync();
                foreach (var s in subs)
                {
                    if(s == "")
                    {
                        continue;
                    }
                    var id = _context.filmSeriesTypes.Where(x => x.type == s).FirstOrDefault();
                    var seriesfilt = new serieType { serieId = series.seriesId,
                    typeId=id.typeID};
                    _context.Add(seriesfilt);
                    await _context.SaveChangesAsync();
                }
                
                
                return RedirectToAction(nameof(Index));
            }
            return View(seriesData);
        }


        private string UploadFile(IFormFile Image, string id)
        {
            string fileName = null;
            string file = null;
            int indexExp = Image.FileName.LastIndexOf(".");
            string fileExp = Image.FileName.Substring(indexExp);
            if (Image != null)
            {
                string uploadDir = Path.Combine(WebEnvironment.WebRootPath, "images/series/");
                fileName = id + fileExp;
                string filePath = Path.Combine(uploadDir, fileName);
                file = filePath;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
                file = "~/images/series/"  + "/" + fileName;
            }
            //string fileName = Path.GetFileName(data.Image.FileName);
            //string imagPath = Path.Combine(Microsoft.AspNetCore.Server.MapPath(WebEnvironment.WebRootPath, fileName);
            //file.SaveAs(imagPath);
            //com.Parameters.Add("@i", "~/AgencyImage/" + file.FileName);
            //com.ExecuteNonQuery();
            return file;
        }


        // GET: series/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            ViewData["typeId"] = new SelectList(_context.filmSeriesTypes, "typeID", "type");
            SeriesData seriesDatas = new SeriesData();
            var series =  _context.serie.Where(x=>x.seriesId==id);
            foreach (var serie in series)
            {
                var s = _context.serie.Where(x=>x.seriesId==serie.seriesId).Select(team => new {
                    TeamName = team.name,
                    PlayersOlder20 = team.serieTypes.Where(x => x.serieId == serie.seriesId).Select(x => x.type)
                }).ToList();
                var n = s[0].PlayersOlder20.ToList();
                var date = n.Count();
                string termsList = "";
                for (var count = 0; count < date; count++)
                {
                    var l = n[count].type;
                    termsList = n[count].type + "," + termsList;
                    var c = count;
                }
                seriesDatas=new SeriesData
                {
                    seriesId = serie.seriesId,
                    name = serie.name,
                    Describtion = serie.Describtion,
                    country = serie.country,
                    CreateDate = serie.CreateDate,

                    rate = serie.rate,
                    time = serie.time,
                    type = termsList
                };
            }



            return View(seriesDatas);
        }

        // POST: series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("seriesId,name,country,time,CreateDate,Describtion,rate,image,type")] SeriesData seriesData)
        {
            if (id != seriesData.seriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = UploadFile(seriesData.image, seriesData.name);
                    string[] subs = seriesData.type.Split(',');
                    var serie = new serie
                    {
                        seriesId = seriesData.seriesId,
                        name = seriesData.name,
                        Describtion = seriesData.Describtion,
                        country = seriesData.country,
                        CreateDate = seriesData.CreateDate,
                        image = file,
                        rate = seriesData.rate,
                        time = seriesData.time
                    };

                    _context.Update(serie);
                    await _context.SaveChangesAsync();

                    foreach (var s in subs)
                    {
                        if (s == "")
                        {
                            continue;
                        }
                        var id1 = _context.filmSeriesTypes.Where(x => x.type == s).FirstOrDefault();
                        var seriesfilt = new serieType
                        {
                            serieId = serie.seriesId,
                            typeId = id1.typeID
                        };
                        _context.serieTypes.Update(seriesfilt);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!serieExists(seriesData.seriesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(seriesData);
        }

        // GET: series/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.serie
                .FirstOrDefaultAsync(m => m.seriesId == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // POST: series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var serie = await _context.serie.FindAsync(id);
            _context.serie.Remove(serie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool serieExists(string id)
        {
            return _context.serie.Any(e => e.seriesId == id);
        }
    }
}
