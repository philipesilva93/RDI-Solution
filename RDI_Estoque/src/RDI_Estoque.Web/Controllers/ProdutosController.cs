using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Aplicacao.ViewModel;
using RDI_Estoque.Dados.Contexto;
using RDI_Estoque.Dominio.Entidades;

namespace RDI_Estoque.Web.Controllers
{

    public class ProdutosController : Controller
    {

        private readonly IAppServicoProduto _IAppServicoProduto;

        public ProdutosController(IAppServicoProduto context)
        {
            _IAppServicoProduto = context;
        }

        // GET: Produtos
        public IActionResult Index()
        {
            return View(_IAppServicoProduto.RecuperarTodos());
        }

        // GET: Produtos/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = _IAppServicoProduto.BuscarPorID(int.Parse(id));
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IDUsuarioCadastro,Marca,DataCadastro,QtdeEstoque,Id,Nome")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _IAppServicoProduto.Adicionar(produto);
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = _IAppServicoProduto.BuscarPorID(int.Parse(id));
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("IDUsuarioCadastro,Marca,DataCadastro,QtdeEstoque,Id,Nome")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _IAppServicoProduto.Atualizar(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = _IAppServicoProduto.BuscarPorID(int.Parse(id));
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var produto = _IAppServicoProduto.BuscarPorID(int.Parse(id));
            _IAppServicoProduto.Excluir(produto);
            return RedirectToAction(nameof(Index));
        }

    }
}
