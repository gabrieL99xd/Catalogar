using Catalogar.Context;
using Catalogar.Models;
using Catalogar.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalogar.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? Nome , string? filtroDeQuantidade , decimal? valorQuantidade , string? filtroDePreco , decimal? valorPreco , int? itemPorPaginas , int? pagina)
        {
            var Products = _context.Products.AsQueryable();

            var itemPorPagina = itemPorPaginas.HasValue ? itemPorPaginas.Value : 5;
            var Pagina = pagina.HasValue ? pagina.Value : 1;
            Pagina = Pagina == 0 ? 1 : Pagina;

            Products = Products.Skip(Pagina - 1).Take(itemPorPagina);


            if (!string.IsNullOrEmpty(Nome))
            {
                Products = Products.Where(p => p.Nome.Contains(Nome) || Nome.Contains(p.Nome) || Nome == p.Nome);
            }

            if (!string.IsNullOrEmpty(filtroDeQuantidade) && valorQuantidade.HasValue)
            {
                if (filtroDeQuantidade.ToUpper() != "SEMFILTRO")
                {
                    var ehMaior = EhMaiorQue(filtroDeQuantidade);
                    if (ehMaior)
                    {
                        Products = Products.Where(p => p.Quantidade >= valorQuantidade);
                    }
                    else
                    {
                        Products = Products.Where(p => p.Quantidade <= valorQuantidade);
                    }
                }
            }

            if (!string.IsNullOrEmpty(filtroDePreco) && valorPreco.HasValue)
            {
                if (filtroDePreco.ToUpper() != "SEMFILTRO")
                {
                    var ehMaior = EhMaiorQue(filtroDePreco);
                    if (ehMaior)
                    {
                        Products = Products.Where(p => p.Preco >= valorPreco);
                    }
                    else
                    {
                        Products = Products.Where(p => p.Preco <= valorPreco);
                    }
                }
            }
            var totalProdutos = _context.Products.Count();

            var productsListViewModel = new ProductIndexViewModel
            {
                Products = Products.ToList(),
                TotalPaginas = totalProdutos/itemPorPagina != 0 ? (int)(totalProdutos/itemPorPagina) : 1,
                PaginaAtual = Pagina
            };

            return View(productsListViewModel);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RegisterProductViewModel product)
        {
            if(ModelState.IsValid)
            {
                var Product = new Product(product.Nome, product.Preco, product.Quantidade);
                _context.Products.Add(Product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var Product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (Product == null)
                return RedirectToAction("Index");
            var RegisterProduct = new RegisterProductViewModel(Product.Nome , Product.Preco, Product.Quantidade);
            return View(RegisterProduct);
        }

        [HttpPost]
        public IActionResult Edit(int id , RegisterProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                var Product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (Product == null)
                    return RedirectToAction("Index");

                Product.Nome = product.Nome;
                Product.Preco = product.Preco;
                Product.Quantidade = product.Quantidade;

                _context.Products.Update(Product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Produto = _context.Products.FirstOrDefault(p => p.Id == id);
            if (Produto == null)
            {
                return NotFound(id);
            }

            _context.Products.Remove(Produto);
            _context.SaveChanges();
            return RedirectToAction("Index" , "Product");
        }

        private bool EhMaiorQue(string filtro)
        {
            var FiltroUpper = filtro.ToUpper();
            return FiltroUpper == "ACIMA";
        }
    }
}
