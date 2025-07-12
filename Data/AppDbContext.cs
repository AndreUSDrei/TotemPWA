using Microsoft.EntityFrameworkCore;
using TotemPWA.Models;

namespace TotemPWA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<Customize> Customizes { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Cupom> Cupoms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Exemplo de configuração de chaves compostas e relacionamentos muitos-para-muitos
            modelBuilder.Entity<Additional>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<ComboProduct>()
                .HasKey(cp => cp.Id);
            modelBuilder.Entity<Customize>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            // Relacionamento de categoria pai/filho
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId);

            base.OnModelCreating(modelBuilder);
        }

        public void SeedDatabase()
        {
            if (!Categories.Any())
            {
                // Categorias
                var catLanches = new Category { Name = "Lanches", Slug = "lanches" };
                var catCombos = new Category { Name = "Combos", Slug = "combos" };
                var catBebidas = new Category { Name = "Bebidas", Slug = "bebidas" };
                var catExtras = new Category { Name = "Extras", Slug = "extras" };
                var catMolhos = new Category { Name = "Molhos", Slug = "molhos" };
                var catSobremesas = new Category { Name = "Sobremesas", Slug = "sobremesas" };
                Categories.AddRange(catLanches, catCombos, catBebidas, catExtras, catMolhos, catSobremesas);
                SaveChanges();

                // Lanches (exemplo, adicione todos conforme views)
                Products.AddRange(new[] {
                    new Product { Name = "Burguer 404", Price = 24.90M, Image = "img/menuburguerimage.png", CategoryId = catLanches.Id },
                    new Product { Name = "Full Stack", Price = 39.90M, Image = "img/full.png", CategoryId = catLanches.Id },
                    new Product { Name = "Looping Triplo", Price = 32.90M, Image = "img/loopingduplo.png", CategoryId = catLanches.Id },
                    new Product { Name = "Hello Word", Price = 24.90M, Image = "img/helloword.png", CategoryId = catLanches.Id },
                    new Product { Name = "VS Veggie", Price = 21.90M, Image = "img/vegan.png", CategoryId = catLanches.Id },
                    new Product { Name = "Backend", Price = 22.90M, Image = "img/backend.jpg", CategoryId = catLanches.Id },
                    new Product { Name = "Frontend", Price = 44.90M, Image = "img/front.png", CategoryId = catLanches.Id },
                    new Product { Name = "DevOps Bacon", Price = 34.90M, Image = "img/devopsbacon.png", CategoryId = catLanches.Id },
                    new Product { Name = "Byte Burguer", Price = 24.90M, Image = "img/byte.png", CategoryId = catLanches.Id }
                });
                SaveChanges();

                // Bebidas (exemplo)
                Products.AddRange(new[] {
                    new Product { Name = "Coca Cola", Price = 8.90M, Image = "img/coca.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Coca Zero", Price = 8.90M, Image = "img/cocazero.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Sprite Zero", Price = 8.90M, Image = "img/spritezero.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Fanta Laranja", Price = 8.90M, Image = "img/fantalaranja.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Fanta Uva", Price = 8.90M, Image = "img/fantauva.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Sprite", Price = 8.90M, Image = "img/sprite.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Suco de Pessego", Price = 9.90M, Image = "img/sucodepessego.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Suco de Uva", Price = 9.90M, Image = "img/sucouva.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Água", Price = 7.90M, Image = "img/agua.png", CategoryId = catBebidas.Id },
                    new Product { Name = "Água com Gás", Price = 7.90M, Image = "img/aguacomgas.png", CategoryId = catBebidas.Id }
                });
                SaveChanges();

                // Extras (exemplo)
                Products.AddRange(new[] {
                    new Product { Name = "Batata Frita", Price = 12.90M, Image = "img/batata.png", CategoryId = catExtras.Id },
                    new Product { Name = "Batata com Cheddar", Price = 18.90M, Image = "img/batatacheddar.png", CategoryId = catExtras.Id },
                    new Product { Name = "Batata com Cheddar e Bacon", Price = 22.90M, Image = "img/batatacheddarcombacon.png", CategoryId = catExtras.Id },
                    new Product { Name = "Batata Rústica", Price = 15.90M, Image = "img/batatarustica.png", CategoryId = catExtras.Id },
                    new Product { Name = "Batata Chips", Price = 8.90M, Image = "img/batatachips.png", CategoryId = catExtras.Id },
                    new Product { Name = "Chips de Batata Doce", Price = 9.90M, Image = "img/chipsdebatatadoce.png", CategoryId = catExtras.Id },
                    new Product { Name = "Nuggets 5un", Price = 12.90M, Image = "img/nuggets5.png", CategoryId = catExtras.Id },
                    new Product { Name = "Nuggets 10un", Price = 19.90M, Image = "img/nuggets.png", CategoryId = catExtras.Id },
                    new Product { Name = "Nuggets 20un", Price = 34.90M, Image = "img/nuggets8.png", CategoryId = catExtras.Id }
                });
                SaveChanges();

                // Molhos (exemplo)
                Products.AddRange(new[] {
                    new Product { Name = "Molho de Ketchup", Price = 4.90M, Image = "img/molhodeketchup.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho de Pimenta", Price = 5.90M, Image = "img/molhodepimenta.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho de Barbecue", Price = 4.90M, Image = "img/molhodebarbecue.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho de Queijos", Price = 5.90M, Image = "img/molhodequeijos.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho de Maionese", Price = 4.90M, Image = "img/maionese.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho Verde", Price = 4.90M, Image = "img/molhoverdedacsa.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho Rose", Price = 7.90M, Image = "img/molhorose.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho de Mostarda", Price = 8.90M, Image = "img/molhodequeijos.png", CategoryId = catMolhos.Id },
                    new Product { Name = "Molho Secreto do Chef", Price = 10.90M, Image = "img/molhoultrasecretodacasa.png", CategoryId = catMolhos.Id }
                });
                SaveChanges();

                // Sobremesas (exemplo)
                Products.AddRange(new[] {
                    new Product { Name = "Sorvete de Morango", Price = 8.90M, Image = "img/sorvetemorango.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Sorvete de Cookie", Price = 8.90M, Image = "img/sorvetecookie.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Sorvete de Chocolate", Price = 8.90M, Image = "img/sorvetechocolate.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Milk-Shake de Morango", Price = 18.90M, Image = "img/milkmorango.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Milk-Shake de Pipoca Caramelizada", Price = 19.90M, Image = "img/milkpipoca.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Milk-Shake de Baunilha", Price = 20.90M, Image = "img/milkbaunilha.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Petit Gateau", Price = 22.90M, Image = "img/petit.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Waffle", Price = 28.90M, Image = "img/waffle.png", CategoryId = catSobremesas.Id },
                    new Product { Name = "Brownie", Price = 10.90M, Image = "img/brownie.png", CategoryId = catSobremesas.Id }
                });
                SaveChanges();

                // Ingredientes
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
                    new Ingredient { Name = "Molho de iogurte", Price = 0.70M, Limit = 2 }
                };
                Ingredients.AddRange(ingredientes);
                SaveChanges();

                // Relacionamento Produto-Ingrediente (exemplo para lanches)
                var produtos = Products.Where(p => p.CategoryId == catLanches.Id).ToList();
                var mapLancheIngredientes = new Dictionary<string, string[]>
                {
                    ["Burguer 404"] = new[] { "Pão de gergelim", "Queijo cheddar", "Hambúrguer de costela", "Alface", "Cebola" },
                    ["Full Stack"] = new[] { "Pão brioche", "Queijo prato", "Hambúrguer de costela", "Bacon", "Cebola caramelizada" },
                    ["Hello Word"] = new[] { "Pão australiano", "Hambúrguer de fraldinha", "Queijo prato", "Picles", "Cebola" },
                    ["VS Veggie"] = new[] { "Pão integral", "Hambúrguer de grão-de-bico", "Abobrinha", "Berinjela", "Molho de iogurte" },
                    ["Backend"] = new[] { "Pão de gergelim", "Hambúrguer de costela", "Queijo prato", "Alface", "Tomate" },
                    ["Frontend"] = new[] { "Pão brioche", "Hambúrguer de costela", "Queijo cheddar", "Bacon", "Molho barbecue" },
                    ["DevOps Bacon"] = new[] { "Pão de gergelim", "Hambúrguer de costela", "Queijo prato", "Bacon", "Cebola crispy" },
                    ["Byte Burguer"] = new[] { "Pão de gergelim", "Hambúrguer de costela", "Queijo coalho", "Molho de pimenta" }
                };
                foreach (var produto in produtos)
                {
                    if (mapLancheIngredientes.TryGetValue(produto.Name, out var nomesIngredientes))
                    {
                        foreach (var nomeIng in nomesIngredientes)
                        {
                            var ing = ingredientes.FirstOrDefault(i => i.Name == nomeIng);
                            if (ing != null)
                                Additionals.Add(new Additional { ProductId = produto.Id, IngredientId = ing.Id });
                        }
                    }
                }
                SaveChanges();

                // Combos (exemplo)
                var combos = new[] {
                    new Product { Name = "Combo Full Stack", Price = 49.90M, Image = "img/combos.png", CategoryId = catCombos.Id },
                    new Product { Name = "DevOps Duplo", Price = 89.90M, Image = "img/combos.png", CategoryId = catCombos.Id },
                    new Product { Name = "Frontend Family", Price = 129.90M, Image = "img/combos.png", CategoryId = catCombos.Id },
                    new Product { Name = "Byte Solo", Price = 39.90M, Image = "img/combos.png", CategoryId = catCombos.Id },
                    new Product { Name = "Stack Overflow", Price = 99.90M, Image = "img/combos.png", CategoryId = catCombos.Id }
                };
                Products.AddRange(combos);
                SaveChanges();

                // Promoções (exemplo)
                Promotions.Add(new Promotion { ProductId = produtos.First().Id, Percent = 10, ValidUntil = DateTime.Now.AddMonths(1) });
                SaveChanges();

                // Cliente de exemplo
                var cliente = new Client { Name = "Cliente Teste", CPF = "000.000.000-00" };
                Clients.Add(cliente);
                SaveChanges();

                // Funcionário de exemplo
                var funcionario = new Employee { Type = "admin", User = "admin", Password = "admin123" };
                Employees.Add(funcionario);
                SaveChanges();

                // Cupom de exemplo
                var cupom = new Cupom { Code = "PROMO10", Type = "percent", Value = 10 };
                Cupoms.Add(cupom);
                SaveChanges();
            }
        }
    }
} 