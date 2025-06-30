using Microsoft.EntityFrameworkCore;
using TotemPWA.Models;

namespace TotemPWA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Additional> Additionals { get; set; }

        public static void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var lanches = new Category { Name = "Lanches", Slug = "lanches" };
                var combos = new Category { Name = "Combos", Slug = "combos" };
                var sobremesas = new Category { Name = "Sobremesas", Slug = "sobremesas" };
                var molhos = new Category { Name = "Molhos", Slug = "molhos" };
                var bebidas = new Category { Name = "Bebidas", Slug = "bebidas" };
                context.Categories.AddRange(lanches, combos, sobremesas, molhos, bebidas);
                context.SaveChanges();

                var ingredientes = new[] {
                    new Ingredient { Name = "Pão de gergelim", Price = 0.50M, Limit = 2 },
                    new Ingredient { Name = "Queijo cheddar", Price = 1.00M, Limit = 3 },
                    new Ingredient { Name = "Hambúrguer de costela", Price = 5.00M, Limit = 3 },
                    new Ingredient { Name = "Alface", Price = 0.30M, Limit = 2 },
                    new Ingredient { Name = "Cebola", Price = 0.20M, Limit = 2 },
                    new Ingredient { Name = "Bacon", Price = 2.00M, Limit = 2 },
                    new Ingredient { Name = "Cebola caramelizada", Price = 0.50M, Limit = 2 },
                    new Ingredient { Name = "Picles", Price = 0.40M, Limit = 2 },
                    new Ingredient { Name = "Queijo prato", Price = 1.50M, Limit = 2 },
                    new Ingredient { Name = "Molho especial", Price = 0.70M, Limit = 2 },
                    new Ingredient { Name = "Queijo coalho", Price = 1.50M, Limit = 2 },
                    new Ingredient { Name = "Molho de pimenta", Price = 0.50M, Limit = 2 },
                    new Ingredient { Name = "Tomate", Price = 0.40M, Limit = 2 },
                    new Ingredient { Name = "Hambúrguer de fraldinha", Price = 5.50M, Limit = 2 },
                    new Ingredient { Name = "Pão brioche", Price = 1.00M, Limit = 2 },
                    new Ingredient { Name = "Cebola crispy", Price = 0.50M, Limit = 2 },
                    new Ingredient { Name = "Molho barbecue", Price = 0.70M, Limit = 2 },
                    new Ingredient { Name = "Pão australiano", Price = 1.20M, Limit = 2 },
                    new Ingredient { Name = "Pão integral", Price = 0.80M, Limit = 2 },
                    new Ingredient { Name = "Hambúrguer de grão-de-bico", Price = 4.00M, Limit = 2 },
                    new Ingredient { Name = "Abobrinha", Price = 0.50M, Limit = 2 },
                    new Ingredient { Name = "Berinjela", Price = 0.50M, Limit = 2 },
                    new Ingredient { Name = "Molho de iogurte", Price = 0.70M, Limit = 2 },
                };
                context.Ingredients.AddRange(ingredientes);
                context.SaveChanges();

                var produtos = new[] {
                    new Product { Name = "Burguer 404", Slug = "burguer-404", Price = 24.90M, CategoryId = lanches.Id, Image = "img/menuburguerimage.png", Description = "Hambúrguer de costela, cheddar, pão de gergelim e salada." },
                    new Product { Name = "Full Stack", Slug = "full-stack", Price = 39.90M, CategoryId = lanches.Id, Image = "img/full.png", Description = "Duplo hambúrguer, queijo prato, bacon e cebola caramelizada." },
                    new Product { Name = "Looping Triplo", Slug = "looping-triplo", Price = 32.90M, CategoryId = lanches.Id, Image = "img/loopingduplo.png", Description = "Três hambúrgueres, cheddar, crispy de cebola e molho especial." },
                    new Product { Name = "Hello Word", Slug = "hello-word", Price = 24.90M, CategoryId = lanches.Id, Image = "img/helloword.png", Description = "Hambúrguer de fraldinha, queijo, picles e pão australiano." },
                    new Product { Name = "VS Veggie", Slug = "vs-veggie", Price = 21.90M, CategoryId = lanches.Id, Image = "img/vegan.png", Description = "Hambúrguer de grão-de-bico, abobrinha, berinjela e pão integral." },
                    new Product { Name = "Backend", Slug = "backend", Price = 22.90M, CategoryId = lanches.Id, Image = "img/backend.jpg", Description = "Hambúrguer de costela, queijo prato, alface e tomate." },
                    new Product { Name = "Frontend", Slug = "frontend", Price = 44.90M, CategoryId = lanches.Id, Image = "img/front.png", Description = "Duplo hambúrguer, cheddar, bacon e molho barbecue." },
                    new Product { Name = "DevOps Bacon", Slug = "devops-bacon", Price = 34.90M, CategoryId = lanches.Id, Image = "img/devopsbacon.png", Description = "Hambúrguer de costela, queijo, bacon e cebola crispy." },
                    new Product { Name = "Byte Burguer", Slug = "byte-burguer", Price = 24.90M, CategoryId = lanches.Id, Image = "img/byte.png", Description = "Hambúrguer de costela, queijo coalho e molho de pimenta." },
                };
                context.Products.AddRange(produtos);
                context.SaveChanges();

                // Mapeamento de ingredientes por produto (exceto combos)
                var produtoPorIngredientes = new Dictionary<string, string[]>
                {
                    ["Burguer 404"] = new[] { "Pão de gergelim", "Queijo cheddar", "Hambúrguer de costela", "Alface", "Cebola" },
                    ["Full Stack"] = new[] { "Pão brioche", "Queijo prato", "Hambúrguer de costela", "Bacon", "Cebola caramelizada" },
                    ["Looping Triplo"] = new[] { "Pão de gergelim", "Queijo cheddar", "Hambúrguer de costela", "Cebola crispy", "Molho especial" },
                    ["Hello Word"] = new[] { "Pão australiano", "Hambúrguer de fraldinha", "Queijo prato", "Picles", "Cebola" },
                    ["VS Veggie"] = new[] { "Pão integral", "Hambúrguer de grão-de-bico", "Abobrinha", "Berinjela", "Molho de iogurte" },
                    ["Backend"] = new[] { "Pão de hambúrguer", "Hambúrguer de costela", "Queijo prato", "Alface", "Tomate" },
                    ["Frontend"] = new[] { "Pão brioche", "Hambúrguer de costela", "Queijo cheddar", "Bacon", "Molho barbecue" },
                    ["DevOps Bacon"] = new[] { "Pão de gergelim", "Hambúrguer de costela", "Queijo prato", "Bacon", "Cebola crispy" },
                    ["Byte Burguer"] = new[] { "Pão de batata", "Hambúrguer de costela", "Queijo coalho", "Molho de pimenta" },
                    // Exemplo para sobremesas, bebidas, molhos, extras (ajuste conforme necessário)
                    // ["Nome do Produto"] = new[] { "Ingrediente 1", "Ingrediente 2" },
                };
                foreach (var produto in produtos)
                {
                    // Não vincular ingredientes aos combos
                    var categoria = context.Categories.FirstOrDefault(c => c.Id == produto.CategoryId);
                    if (categoria != null && categoria.Slug == "combos") continue;
                    if (produtoPorIngredientes.TryGetValue(produto.Name, out var nomesIngredientes))
                    {
                        foreach (var nomeIng in nomesIngredientes)
                        {
                            var ing = ingredientes.FirstOrDefault(i => i.Name == nomeIng);
                            if (ing != null)
                                context.Additionals.Add(new Additional { ProductId = produto.Id, IngredientId = ing.Id });
                        }
                    }
                }
                context.SaveChanges();

                // Combos criativos e funcionais
                var comboCategoria = context.Categories.First(c => c.Slug == "combos");
                var produtosCombos = new[] {
                    new Product {
                        Name = "Combo Full Stack",
                        Slug = "combo-full-stack",
                        Price = 49.90M,
                        CategoryId = comboCategoria.Id,
                        Image = "img/combos.png",
                        Description = "1x Burguer 404, 1x Batata Frita, 1x Coca-Cola, 1x Molho Barbecue"
                    },
                    new Product {
                        Name = "DevOps Duplo",
                        Slug = "combo-devops-duplo",
                        Price = 89.90M,
                        CategoryId = comboCategoria.Id,
                        Image = "img/combos.png",
                        Description = "2x DevOps Bacon, 2x Batata Cheddar com Bacon, 2x Sprite, 2x Molho Rose"
                    },
                    new Product {
                        Name = "Frontend Family",
                        Slug = "combo-frontend-family",
                        Price = 129.90M,
                        CategoryId = comboCategoria.Id,
                        Image = "img/combos.png",
                        Description = "3x Frontend Burguer, 2x Batata Rústica, 3x Fanta Laranja, 3x Molho Verde, 2x Brownie"
                    },
                    new Product {
                        Name = "Byte Solo",
                        Slug = "combo-byte-solo",
                        Price = 39.90M,
                        CategoryId = comboCategoria.Id,
                        Image = "img/combos.png",
                        Description = "1x Byte Burguer, 1x Chips de Batata Doce, 1x Suco de Uva, 1x Molho de Queijos"
                    },
                    new Product {
                        Name = "Stack Overflow",
                        Slug = "combo-stack-overflow",
                        Price = 99.90M,
                        CategoryId = comboCategoria.Id,
                        Image = "img/combos.png",
                        Description = "2x Full Stack, 2x Batata Frita, 2x Coca Zero, 2x Molho Secreto do Chef, 1x Sorvete de Morango"
                    }
                };
                context.Products.AddRange(produtosCombos);
                context.SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Additional>()
                .HasKey(a => new { a.ProductId, a.IngredientId });
            base.OnModelCreating(modelBuilder);
        }
    }
} 