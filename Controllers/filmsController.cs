using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CimaLek.Data;
using Entertment.Models;
using Microsoft.AspNetCore.Hosting;
using CimaLek.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CimaLek.Controllers
{
    public class filmsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment WebEnvironment;

        public filmsController(ApplicationDbContext context, IWebHostEnvironment e)
        {
            _context = context;
            WebEnvironment = e;
        }

        public async Task<IActionResult> FilmsHome()
        {

            List<SeriesData> seriesDatas = new List<SeriesData>();
            var films = await _context.films.ToListAsync();

            int x = 0;
            foreach (var film in films)
            {

                var s = _context.films.Where(x => x.filmId == film.filmId).Select(team => new {

                    PlayersOlder20 = team.filmtypes.Where(x => x.filmId == film.filmId).Select(x => x.type)
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
                    seriesId = film.filmId,
                    name = film.name,
                    Describtion = film.Describtion,
                    country = film.country,
                    CreateDate = film.CreateDate,
                    imageUrl = film.image,
                    rate = film.rate,
                    time = film.time,
                    type = termsList
                });
            }
            var si = seriesDatas;




            return View(seriesDatas);
        }

        public IActionResult AddFilm()
        {
            ViewData["typeId"] = new SelectList(_context.filmSeriesTypes, "typeID", "type");
            return View();
        }

        // POST: series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFilm(SeriesData seriesData)
        {
            if (ModelState.IsValid)
            {

                var file = UploadFile(seriesData.image, seriesData.name,"films");
                string[] subs = seriesData.type.Split(',');
                var film = new film
                {
                    filmId = Guid.NewGuid().ToString(),
                    name = seriesData.name,
                    Describtion = seriesData.Describtion,
                    country = seriesData.country,
                    CreateDate = seriesData.CreateDate,
                    image = file,
                    rate = seriesData.rate,
                    time = seriesData.time,
                    TrailerURl = seriesData.TrailerURl
                };
                _context.Add(film);
                await _context.SaveChangesAsync();
                foreach (var s in subs)
                {
                    if (s == "")
                    {
                        continue;
                    }
                    var id = _context.filmSeriesTypes.Where(x => x.type == s).FirstOrDefault();
                    var seriesfilt = new filmtype
                    {
                        filmId = film.filmId,
                        typeId = id.typeID
                    };
                    _context.Add(seriesfilt);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction("AddAuthor",new { id = film.filmId});
            }
            return View(seriesData);
        }
        [HttpGet]
        public async Task<IActionResult> AddAuthor(string? id)
        {
            if(id != null)
            {
                ViewBag.id = id;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author authorData,string? id)
        {
            ViewBag.id = id;
            var file = UploadFile(authorData.Image, authorData.author, "authors");
            if (ModelState.IsValid)
            {
               if(id != null)
                {

                   
                    var author1 = new Author
                    {
                        AuthorId = Guid.NewGuid().ToString(),
                        author = authorData.author,
                        ImageURl = file,
                        
                    };
                    _context.Add(author1);
                    await _context.SaveChangesAsync();
                    var authorTofilm = new AuthorToFilm
                    {
                        authorId = author1.AuthorId,
                        filmId = id
                    };
                    _context.Add(authorTofilm);
                    await _context.SaveChangesAsync();

                    if (authorData.Again == true)
                        return View();
                    return RedirectToAction("Index");
                }
               
                var author = new Author
                {
                    AuthorId = Guid.NewGuid().ToString(),
                    author = authorData.author,
                    ImageURl = file,

                };
                _context.Add(author);
                await _context.SaveChangesAsync();
                if (authorData.Again == true)
                    return View();
                return RedirectToAction("AuthorIndex");
            }
            return View(authorData); 
        }
        private string UploadFile(IFormFile Image, string id,string type)
        {
            string fileName = null;
            string file = null;
            int indexExp = Image.FileName.LastIndexOf(".");
            string fileExp = Image.FileName.Substring(indexExp);
            if (Image != null)
            {
                string uploadDir = Path.Combine(WebEnvironment.WebRootPath, "images/"+ type+"/");
                fileName = id + fileExp;
                string filePath = Path.Combine(uploadDir, fileName);
                file = filePath;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
                file = "~/images/" + type + "/" + "/" + fileName;
            }
            //string fileName = Path.GetFileName(data.Image.FileName);
            //string imagPath = Path.Combine(Microsoft.AspNetCore.Server.MapPath(WebEnvironment.WebRootPath, fileName);
            //file.SaveAs(imagPath);
            //com.Parameters.Add("@i", "~/AgencyImage/" + file.FileName);
            //com.ExecuteNonQuery();
            return file;
        }




        // GET: films
        public async Task<IActionResult> Index(string? id)
        {
            int x = 0;

            if (id != null)
            {
                List<SeriesData> seriesDatas = new List<SeriesData>();
                var films = _context.AuthorToFilms.Where(x => x.authorId == id).Select(x => x.film).ToList();
                

                
                foreach (var film in films)
                {

                    var s = _context.films.Where(x => x.filmId == film.filmId).Select(team => new {

                        PlayersOlder20 = team.filmtypes.Where(x => x.filmId == film.filmId).Select(x => x.type)
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
                        seriesId = film.filmId,
                        name = film.name,
                        Describtion = film.Describtion,
                        country = film.country,
                        CreateDate = film.CreateDate,

                        rate = film.rate,
                        time = film.time,
                        type = termsList
                    });
                }

                
                return View(seriesDatas);
            }

            List<SeriesData> seriesDatas1 = new List<SeriesData>();
            var films1 = await _context.films.ToListAsync();
            foreach (var film in films1)
            {

                var s = _context.films.Where(x => x.filmId == film.filmId).Select(team => new {

                    PlayersOlder20 = team.filmtypes.Where(x => x.filmId == film.filmId).Select(x => x.type)
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
                seriesDatas1.Add(new SeriesData
                {
                    seriesId = film.filmId,
                    name = film.name,
                    Describtion = film.Describtion,
                    country = film.country,
                    CreateDate = film.CreateDate,

                    rate = film.rate,
                    time = film.time,
                    type = termsList
                });
            }
            var si = seriesDatas1;




            return View(seriesDatas1);
        }

        // GET: films/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.films
                .FirstOrDefaultAsync(m => m.filmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        public async Task<IActionResult> AuthorIndex(string? id)
        {
            if(id != null)
            {
                var AuthortoFilm =await _context.AuthorToFilms.Where(x => x.filmId == id).Select(x => x.Author).ToListAsync();
                return View(AuthortoFilm);
            }
            var AuthorToFilms = await _context.Authors.ToListAsync();
            return View(AuthorToFilms);
        }

        public async Task<IActionResult> EditAuthor(string id)
        {
            return View(await _context.Authors.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuthor(string id, Author author)
        {
            if(ModelState.IsValid)
            {
                var file = UploadFile(author.Image, author.author, "films");
                var authors = new Author
                {
                    AuthorId = author.AuthorId,
                    author = author.author,

                };
                _context.Update(authors);
                await _context.SaveChangesAsync();
                return RedirectToAction("AuthorIndex");
            }
            return View(author);
        }


        // GET: films/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["typeId"] = new SelectList(_context.filmSeriesTypes, "typeID", "type");
            SeriesData seriesDatas = new SeriesData();
            var films = _context.films.Where(x => x.filmId == id);
            foreach (var film in films)
            {
                var s = _context.films.Where(x => x.filmId == film.filmId).Select(team => new {
                    TeamName = team.name,
                    PlayersOlder20 = team.filmtypes.Where(x => x.filmId == film.filmId).Select(x => x.type)
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
                seriesDatas = new SeriesData
                {
                    seriesId = film.filmId,
                    name = film.name,
                    Describtion = film.Describtion,
                    country = film.country,
                    CreateDate = film.CreateDate,

                    rate = film.rate,
                    time = film.time,
                    type = termsList
                };
            }



            return View(seriesDatas);
        }

        // POST: films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SeriesData seriesData)
        {
            ViewData["typeId"] = new SelectList(_context.filmSeriesTypes, "typeID", "type");

            if (id != seriesData.seriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = UploadFile(seriesData.image, seriesData.name,"films");
                    string[] subs = seriesData.type.Split(',');
                    var film = new film
                    {
                        filmId = seriesData.seriesId,
                        name = seriesData.name,
                        Describtion = seriesData.Describtion,
                        country = seriesData.country,
                        CreateDate = seriesData.CreateDate,
                        image = file,
                        rate = seriesData.rate,
                        time = seriesData.time,
                        TrailerURl = seriesData.TrailerURl

                    };

                    _context.Update(film);
                    await _context.SaveChangesAsync();

                    foreach (var s in subs)
                    {
                        if (s == "")
                        {
                            continue;
                        }
                        var id1 = _context.filmSeriesTypes.Where(x => x.type == s).FirstOrDefault();
                        var filmtype = new filmtype
                        {
                            filmId = film.filmId,
                            typeId = id1.typeID
                        };
                        _context.filmtypes.Update(filmtype);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!filmExists(seriesData.seriesId))
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

        // GET: films/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.films
                .FirstOrDefaultAsync(m => m.filmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var film = await _context.films.FindAsync(id);
            _context.films.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool filmExists(string id)
        {
            return _context.films.Any(e => e.filmId == id);
        }

        [HttpGet]
        public async Task<IActionResult> AddFilmServer()
        {
            ViewData["filmId"] = new SelectList(_context.films,  "filmId", "name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFilmServer(filmWatch filmWatch)
        {
            ViewData["filmId"] = new SelectList(_context.films, "filmId", "name");
            if (ModelState.IsValid)
            {
                
                _context.Add(filmWatch);
                await _context.SaveChangesAsync();
                if(filmWatch.Again)
                {
                    return View();
                }
                return RedirectToAction("ServerIndex");
            }

            return View(filmWatch);
        }
        public async Task<IActionResult> ServerIndex()
        {
            return View(await _context.filmWatches.ToListAsync());
        }

        public async Task<IActionResult> film(string Id)
        {
            var Type = new List<filmSeriesType>();
            var film =  _context.films.Where(x => x.filmId == Id).Include(x=>x.filmtypes).Include(x=>x.AuthorToFilms).FirstOrDefault();
            
            

            
            var types = film.filmtypes.ToList();
            foreach(var type in types)
            {
                Type.Add(_context.filmSeriesTypes.Where(x=>x.typeID==type.typeId).FirstOrDefault());
            }
            types[0].type = Type[0];
            types[1].type = Type[1];
            film.filmtypes = types;


            var Actors = new List<Author>();
            var actorsToFilm = film.AuthorToFilms.ToList();


            foreach (var actor in actorsToFilm)
            {
                Actors.Add(_context.Authors.Where(x => x.AuthorId == actor.authorId).FirstOrDefault());
            }


            return View(film);
        }





    }
}
