using EvaluationWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EvaluationWeb.Controllers
{
    public class ProprietaireController : Controller
    {
        private Entities.EvaluationEntities db = new Entities.EvaluationEntities();
        // GET: Proprietaire
        [System.Web.Http.HttpGet]
        public IHttpActionResult ListeProprietaire(int index = 0, int taille = 10)
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;

                var categories = db.Proprietaire.Include(nameof(Proprietaire)).OrderByDescending(x => x.Nom).
                    Skip(index * taille + 1).Take(taille).ToList();
                return Ok(Proprietaire);
            }
            catch (DbUpdateException ex)
            {
                var exception = ex.InnerException?.InnerException as SqlException;
                return BadRequest(exception?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}