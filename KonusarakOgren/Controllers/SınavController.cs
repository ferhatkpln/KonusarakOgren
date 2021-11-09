using HtmlAgilityPack;
using KonusarakOgren.Models;
using KonusarakOgren.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace KonusarakOgren.Controllers
{
    [Authorize]
    public class SınavController : Controller
    {
        [HttpGet]
        public IActionResult SınavListesi()
        {
            List<Sınav> sınavlist;

            using (var db = new Context())
            {
                db.Database.EnsureCreated();

                sınavlist = db.Sınavlar
                    .OrderByDescending(x => x.OlusturmaTarihi)
                    .OrderBy(x => x.SınavID)
                    .ToList();
            }

            return View(sınavlist);
        }

        [HttpGet]
        public IActionResult SınavEkle()
        {
            try
            {
                var posts = GetTextTitles(5);

                if (posts != null)
                {
                    SınavViewModel sınav = new SınavViewModel();

                    List<TextViewModel> textViewModels = new List<TextViewModel>();

                    foreach (var post in posts)
                    {
                        var textViewModel = new TextViewModel()
                        {
                            Id = post.Id,
                            Baslık = post.Title.Text
                        };

                        textViewModels.Add(textViewModel);
                    }

                    sınav.TextViewModels = textViewModels;

                    return View(sınav);
                }
                else
                {
                    return RedirectToAction("HataSayfası", "Sınav",
                           new { title = "Beklenmeyen Durum", content = "Yazılar listelemedi!" });
                }          
            }
            catch (Exception ee)
            {
                return RedirectToAction("HataSayfası", "Sınav",
                      new { title = ee.Message, content = "İstenmeyen bir durum oluştu!" });
            }
        }

        [HttpPost]
        public IActionResult SınavEkle(SınavViewModel sınavmodel)
        {
            if (ModelState.IsValid)
            {
                Sınav sınav = new Sınav()
                {
                    OlusturmaTarihi = DateTime.Now,
                    İcerik = sınavmodel.Sınav.İcerik,
                    Sorulars = sınavmodel.Sınav.Sorulars,
                    TextID = sınavmodel.Sınav.TextID,
                    Baslık = sınavmodel.Sınav.Baslık
                };

                List<Sorular> cevaplar = new List<Sorular>();

                foreach (var s in sınavmodel.Sınav.Sorulars)
                {
                    Sorular examQuestion = new Sorular()
                    {
                        CevapA = s.CevapA,
                        CevapB = s.CevapB,
                        CevapC = s.CevapC,
                        CevapD = s.CevapD,
                        Soru = s.Soru,
                        DoğruCevap = s.DoğruCevap,
                        SınavID = sınav.SınavID,
                        SoruNo = s.SoruNo
                    };

                    cevaplar.Add(examQuestion);
                }

                sınav.Sorulars = cevaplar;

                using (var db = new Context())
                {
                    db.Database.EnsureCreated();
                    db.Add(sınav);
                    int insertResult = db.SaveChanges();

                    if (insertResult > 0)
                    {
                        return RedirectToAction("SınavListesi", "Sınav");
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Lütfen boş alanları doldurunuz!";
                return View();
            }

            return RedirectToAction("SoruEkle", "Sınav");
        }

        [HttpGet]
        public IActionResult SınavSayfası(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("SınavListesi");
            }

            using (var db = new Context())
            {
                db.Database.EnsureCreated();

                Sınav sınav = db.Sınavlar.Where(x => x.SınavID == id).FirstOrDefault();
                List<Sorular> sorular = db.Sorular.Where(x => x.SınavID == id).ToList();
                sınav.Sorulars = sorular;

                if (sınav != null)
                {
                    return View(sınav);
                }
                else
                {
                    return RedirectToAction("SınavListesi");
                }
            }
        }

        public JsonResult SınavCevapControl(List<CevapViewModel> cevapmodel, int sınavıd)
        {
            if (cevapmodel != null)
            {
                using (var db = new Context())
                {
                    db.Database.EnsureCreated();
                    List<Sorular> sorular = db.Sorular.Where(x => x.SınavID == sınavıd).ToList();

                    foreach (var soru in sorular)
                    {
                        for (int i = 0; i < cevapmodel.Count; i++)
                        {
                            if (soru.SoruNo == cevapmodel[i].Id)
                            {
                                if (soru.DoğruCevap.Equals(cevapmodel[i].Cevap))
                                {
                                    cevapmodel[i].DoğruCevapmı = true;
                                }
                                else
                                {
                                    cevapmodel[i].DoğruCevapmı = false;
                                }
                                cevapmodel[i].DoğruCevap = soru.DoğruCevap;
                            }
                        }
                    }
                    return Json(cevapmodel);
                }
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult GetTextContent(string id)
        {
            var posts = GetTextTitles();

            var content = posts.Where(x => x.Id.Equals(id)).FirstOrDefault();

            return Json(content.Summary.Text);
        }

        public IEnumerable<SyndicationItem> GetTextTitles(int count = -1)
        {
            var url = "https://www.wired.com/feed/rss";
            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            var posts = feed.Items.OrderByDescending(x => x.PublishDate);

            if (count != -1)
            {
                var lastPosts = posts.Take(count);
                return lastPosts;
            }
            else
                return posts;
        }

        [HttpGet]
        public IActionResult SınavSil(int id)
        {
            using (var dbContext = new Context())
            {
                dbContext.Database.EnsureCreated();

                Sınav sınav = dbContext.Sınavlar.Where(x => x.SınavID == id).FirstOrDefault();
                if (sınav != null)
                {
                    dbContext.Remove(sınav);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("SınavListesi", "Sınav");
        }

        [HttpGet]
        public IActionResult HataSayfası(string title = "", string content = "")
        {
            if (String.IsNullOrEmpty(title))
            {
                title = "Bilinemyen Hata!";
            }

            if (String.IsNullOrEmpty(content))
            {
                content = "Beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }

            ViewBag.Title = title;
            ViewBag.Content = content;

            return View();
        }
    }
}
