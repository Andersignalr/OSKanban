using Microsoft.AspNetCore.Mvc;
using OSKanban.Data;
using OSKanban.Models;

namespace OSKanban.Controllers
{
    public class OSController : Controller
    {
        private readonly AppDbContext _context;

        private static List<string> tecnicos = new List<string>
        {
            "Anderson",
            "Marco Túlio",
            "Victor",
            "Daniel"
        };
        public OSController(AppDbContext context) {
            _context = context;
        }

        public IActionResult Index()
        {
            var lista = _context.OrdemServicos.ToList();

            ViewBag.Tecnicos = tecnicos;

            return View(lista);
        }

        [HttpPost]
        public IActionResult Mover([FromBody] MoverDto dto)
        {
            var os = _context.OrdemServicos.Find(dto.Id);
            if (os != null)
            {
                os.Tecnico = dto.NovoTecnico;
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Criar([FromBody] OrdemServico os)
        {
            os.Data = DateTime.Now;
            os.Status = "Em andamento";

            _context.OrdemServicos.Add(os);
            _context.SaveChanges();

            return Json(os);
        }

        [HttpGet]
        public IActionResult Obter(int id)
        {
            var os = _context.OrdemServicos.Find(id);
            if (os == null) return NotFound();

            return Json(os);
        }

        [HttpPost]
        public IActionResult Editar([FromBody] OrdemServico os)
        {
            var existente = _context.OrdemServicos.Find(os.Id);

            if (existente == null) return NotFound();

            existente.Unidade = os.Unidade;
            existente.Tecnico = os.Tecnico;
            existente.Patrimonio = os.Patrimonio;
            existente.Defeito = os.Defeito;
            existente.Observacoes = os.Observacoes;

            _context.SaveChanges();

            return Json(existente);
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var os = _context.OrdemServicos.Find(id);

            if (os == null)
                return NotFound();

            _context.OrdemServicos.Remove(os);
            _context.SaveChanges();

            return Ok();
        }
    }

    public class MoverDto
    {
        public int Id { get; set; }
        public string NovoTecnico { get; set; }
    }
}