using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Data;
using SimpleCRUD.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployesContext _context;

        public HomeController(EmployesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, string nom)
        {
            List<Employe> employes;
            ViewBag.Recherche = false;
            if (id == null && nom == null)
            {
                employes = await _context.Employes.ToListAsync();
            }
            else
            {
                ViewBag.Recherche = true;
                employes = await _context.Employes.FromSql("EXEC RechercherEMP {0},{1}", id, nom).ToListAsync();
            }

            return View(employes);
        }

        public IActionResult Peupler()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Peupler(int x, int y)
        {
            try
            {
                _context.Database.ExecuteSqlCommand("EXEC PeuplerEmp {0},{1}", x, y);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Erreur lors du peuplement");
                ViewBag.x = x;
                ViewBag.y = y;
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employe employe)
        {
            if (!ModelState.IsValid)
                return View(employe);

            try
            {
                string sql = "EXEC AjouterEMP id,prenom,nom,fonction,salaire,date_em,date_na,pays";
                _context.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("id", employe.Id),
                    new SqlParameter("prenom", employe.Prenom),
                    new SqlParameter("nom", employe.Nom),
                    new SqlParameter("fonction", employe.Fonction),
                    new SqlParameter("salaire", employe.Salaire),
                    new SqlParameter("data_em", employe.DateEmbauche),
                    new SqlParameter("date_na", employe.DateNaissance),
                    new SqlParameter("pays", employe.Pays));
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult Details(int id)
        {
            Employe employe = _context.Employes.FromSql($"EXEC RechercherEMP {id}").SingleOrDefault();

            if (employe == null)
                return NotFound();

            return View(employe);
        }


        public IActionResult Edit(int id)
        {
            Employe employe = _context.Employes.FromSql($"EXEC RechercherEMP {id}").SingleOrDefault();

            if (employe == null)
                return NotFound();

            return View(employe);
        }

        [HttpPost]
        public IActionResult Edit(Employe employe)
        {
            if (!ModelState.IsValid)
                return View(employe);

            try
            {
                _context.Database.ExecuteSqlCommand("EXEC ModifierEMP {0},{1},{2},{3}", employe.Id, employe.Fonction,
                    employe.Salaire, employe.Pays);
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        public IActionResult Delete(int id)
        {
            Employe employe = _context.Employes.FromSql($"EXEC RechercherEMP {id}").SingleOrDefault();

            if (employe == null)
                return NotFound();

            return View(employe);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _context.Database.ExecuteSqlCommand($"EXEC SupprimerEMP {id}");

            return RedirectToAction("Index");
        }
    }
}
