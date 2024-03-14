namespace Catalogar.Models.ViewModel
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public ProductIndexViewModel(List<Product> products)
        {
            Products = products;
        }
        public ProductIndexViewModel()
        {
            
        }
    }
}
