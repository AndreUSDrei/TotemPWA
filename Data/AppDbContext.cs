using Microsoft.EntityFrameworkCore;
using TotemPWA.Models;

namespace TotemPWA.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Additional> Additionals { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cupom> Cupoms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Customize> Customizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ComboProduct: chave composta
            modelBuilder.Entity<ComboProduct>()
                .HasKey(cp => new { cp.ComboId, cp.ProductId });
            modelBuilder.Entity<ComboProduct>()
                .HasOne(cp => cp.Combo)
                .WithMany(c => c.ComboProducts)
                .HasForeignKey(cp => cp.ComboId);
            modelBuilder.Entity<ComboProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.ComboProducts)
                .HasForeignKey(cp => cp.ProductId);

            // Additional: chave composta
            modelBuilder.Entity<Additional>()
                .HasKey(a => new { a.ProductId, a.IngredientId });
            modelBuilder.Entity<Additional>()
                .HasOne(a => a.Product)
                .WithMany(p => p.Additionals)
                .HasForeignKey(a => a.ProductId);
            modelBuilder.Entity<Additional>()
                .HasOne(a => a.Ingredient)
                .WithMany(i => i.Additionals)
                .HasForeignKey(a => a.IngredientId);

            // Customize: chave composta (opcional, mas aqui usaremos Id como PK e FKs para Ingredient e OrderItem)
            modelBuilder.Entity<Customize>()
                .HasOne(c => c.Ingredient)
                .WithMany(i => i.Customizes)
                .HasForeignKey(c => c.IngredientId);
            modelBuilder.Entity<Customize>()
                .HasOne(c => c.OrderItem)
                .WithMany(oi => oi.Customizes)
                .HasForeignKey(c => c.OrderItemId);

            // Employee/Client: herança via FK
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.ClientId);
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Client)
                .WithMany()
                .HasForeignKey(e => e.ClientId);

            // Category: auto-relacionamento
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product-Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            // Promotion-Product
            modelBuilder.Entity<Promotion>()
                .HasOne(pr => pr.Product)
                .WithMany(p => p.Promotions)
                .HasForeignKey(pr => pr.ProductId);

            // Order-Client
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            // Order-Cupom
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cupom)
                .WithMany()
                .HasForeignKey(o => o.CupomId)
                .OnDelete(DeleteBehavior.SetNull);

            // OrderItem-Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // OrderItem-Product
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            // Payment-Order
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);
        }

        public static void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var lanches = new Category { Name = "Lanches", Slug = "lanches" };
                var sobremesas = new Category { Name = "Sobremesas", Slug = "sobremesas" };
                var bebidas = new Category { Name = "Bebidas", Slug = "bebidas" };
                var molhos = new Category { Name = "Molhos", Slug = "molhos" };
                context.Categories.AddRange(lanches, sobremesas, bebidas, molhos);
                context.SaveChanges();

                context.Products.AddRange(
                    new Product { Name = "Burguer 404", Slug = "burguer-404", Price = 24.90M, CategoryId = lanches.Id, Image = "img/menuburguerimage.png" },
                    new Product { Name = "Full Stack", Slug = "full-stack", Price = 39.90M, CategoryId = lanches.Id, Image = "img/full.png" },
                    new Product { Name = "Looping Triplo", Slug = "looping-triplo", Price = 32.90M, CategoryId = lanches.Id, Image = "img/loopingduplo.png" },
                    new Product { Name = "Hello Word", Slug = "hello-word", Price = 24.90M, CategoryId = lanches.Id, Image = "img/helloword.png" },
                    new Product { Name = "VS Veggie", Slug = "vs-veggie", Price = 21.90M, CategoryId = lanches.Id, Image = "img/vegan.png" },
                    new Product { Name = "Backend", Slug = "backend", Price = 22.90M, CategoryId = lanches.Id, Image = "img/backend.jpg" },
                    new Product { Name = "Frontend", Slug = "frontend", Price = 44.90M, CategoryId = lanches.Id, Image = "img/front.png" },
                    new Product { Name = "DevOps Bacon", Slug = "devops-bacon", Price = 34.90M, CategoryId = lanches.Id, Image = "img/devopsbacon.png" },
                    new Product { Name = "Byte Burguer", Slug = "byte-burguer", Price = 24.90M, CategoryId = lanches.Id, Image = "img/byte.png" },
                    new Product { Name = "Petit Gateau", Slug = "petit-gateau", Price = 14.90M, CategoryId = sobremesas.Id, Image = "img/petit.png" },
                    new Product { Name = "Brownie", Slug = "brownie", Price = 12.90M, CategoryId = sobremesas.Id, Image = "img/brownie.png" },
                    new Product { Name = "Sorvete Cookie", Slug = "sorvete-cookie", Price = 10.90M, CategoryId = sobremesas.Id, Image = "img/sorvetecookie.png" },
                    new Product { Name = "Sorvete Morango", Slug = "sorvete-morango", Price = 10.90M, CategoryId = sobremesas.Id, Image = "img/sorvetemorango.png" },
                    new Product { Name = "Sorvete Chocolate", Slug = "sorvete-chocolate", Price = 10.90M, CategoryId = sobremesas.Id, Image = "img/sorvetechocolate.png" },
                    new Product { Name = "Coca Cola", Slug = "coca-cola", Price = 8.90M, CategoryId = bebidas.Id, Image = "img/coca.png" },
                    new Product { Name = "Coca Zero", Slug = "coca-zero", Price = 8.90M, CategoryId = bebidas.Id, Image = "img/cocazero.png" },
                    new Product { Name = "Sprite", Slug = "sprite", Price = 8.90M, CategoryId = bebidas.Id, Image = "img/sprite.png" },
                    new Product { Name = "Sprite Zero", Slug = "sprite-zero", Price = 8.90M, CategoryId = bebidas.Id, Image = "img/spritezero.png" },
                    new Product { Name = "Fanta Laranja", Slug = "fanta-laranja", Price = 8.90M, CategoryId = bebidas.Id, Image = "img/fantalaranja.png" },
                    new Product { Name = "Fanta Uva", Slug = "fanta-uva", Price = 8.90M, CategoryId = bebidas.Id, Image = "img/fantauva.png" },
                    new Product { Name = "Suco de Pessego", Slug = "suco-pessego", Price = 9.90M, CategoryId = bebidas.Id, Image = "img/sucodepessego.png" },
                    new Product { Name = "Suco de Uva", Slug = "suco-uva", Price = 9.90M, CategoryId = bebidas.Id, Image = "img/sucouva.png" },
                    new Product { Name = "Água", Slug = "agua", Price = 7.90M, CategoryId = bebidas.Id, Image = "img/agua.png" },
                    new Product { Name = "Água com Gás", Slug = "agua-com-gas", Price = 7.90M, CategoryId = bebidas.Id, Image = "img/aguacomgas.png" },
                    new Product { Name = "Molho Barbecue", Slug = "molho-barbecue", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhodebarbecue.png" },
                    new Product { Name = "Molho Ketchup", Slug = "molho-ketchup", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhodeketchup.png" },
                    new Product { Name = "Molho Pimenta", Slug = "molho-pimenta", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhodepimenta.png" },
                    new Product { Name = "Molho Queijos", Slug = "molho-queijos", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhodequeijos.png" },
                    new Product { Name = "Molho Rose", Slug = "molho-rose", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhorose.png" },
                    new Product { Name = "Molho Verde", Slug = "molho-verde", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhoverdedacsa.png" },
                    new Product { Name = "Molho Ultra Secreto", Slug = "molho-ultra-secreto", Price = 2.00M, CategoryId = molhos.Id, Image = "img/molhoultrasecretodacasa.png" }
                );
                context.SaveChanges();
            }
        }
    }
} 