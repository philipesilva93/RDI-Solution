using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Dados.Contexto;
using RDI_Estoque.Dominio.Entidades;

namespace RDI_Estoque.Web.Controllers
{
    public class NotaFiscalsController : Controller
    {
        private readonly IAppServicoNotaFiscal _IAppServicoNotaFiscal;
        private readonly IAppServicoProduto _IAppServicoProduto;
        public NotaFiscalsController(IAppServicoNotaFiscal context, IAppServicoProduto IAppServicoProduto)
        {
            _IAppServicoNotaFiscal = context;
            _IAppServicoProduto = IAppServicoProduto;
        }

        // GET: NotaFiscals
        public IActionResult Index()
        {
            var appContexto = _IAppServicoNotaFiscal.RecuperarTodos();
            return View(appContexto);
        }

        // GET: NotaFiscals/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = _IAppServicoNotaFiscal.BuscarPorID(int.Parse(id));
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // GET: NotaFiscals/Create
        public IActionResult Create()
        {
            ViewData["IdProduto"] = new SelectList(_IAppServicoProduto.RecuperarTodos(), "Id", "Nome");
            return View();
        }

        // POST: NotaFiscals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Qtde,Numero,Serie,Valor,ChaveAcessoNfe,DataCadastro,DataEntrega,DataEmissao,DataSaida,IdUsuarioCadastro,IdProduto,Id,Nome")] NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                _IAppServicoNotaFiscal.Adicionar(notaFiscal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_IAppServicoProduto.RecuperarTodos(), "Id", "Nome", notaFiscal.IdProduto);
            return View(notaFiscal);
        }

        // GET: NotaFiscals/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = _IAppServicoNotaFiscal.BuscarPorID(int.Parse(id));
            if (notaFiscal == null)
            {
                return NotFound();
            }
            ViewData["IdProduto"] = new SelectList(_IAppServicoProduto.RecuperarTodos(), "Id", "Nome", notaFiscal.IdProduto);
            return View(notaFiscal);
        }

        // POST: NotaFiscals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Qtde,Numero,Serie,Valor,ChaveAcessoNfe,DataCadastro,DataEntrega,DataEmissao,DataSaida,IdUsuarioCadastro,IdProduto,Id,Nome")] NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _IAppServicoNotaFiscal.Atualizar(notaFiscal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProduto"] = new SelectList(_IAppServicoProduto.RecuperarTodos(), "Id", "Nome", notaFiscal.IdProduto);
            return View(notaFiscal);
        }

        // GET: NotaFiscals/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notaFiscal = _IAppServicoNotaFiscal.BuscarPorID(int.Parse(id));
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // POST: NotaFiscals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var notaFiscal = _IAppServicoNotaFiscal.BuscarPorID(int.Parse(id));
            _IAppServicoNotaFiscal.Excluir(notaFiscal);
            return RedirectToAction(nameof(Index));
        }

    }
}
