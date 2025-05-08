using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ECommerceDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ECommerceDbContext eCommerceDb, StoreIdentityDbContext identityDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = eCommerceDb;
            _identityDbContext = identityDbContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        

        public async Task InitializeAsync()
        {
            try
            {
                // Create DataBase If It Doesnot Exist && Apply To Any Pending Migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                // Data Seeding

                // Seeding ProductTypes From Json File
                




                if (!_context.ProductTypes.Any())
                {
                    // 1. Read All Data From types Json File As String
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");

                    // 2. Transform String To C# Objects [List<ProductTypes>]

                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3. Add List<ProductTypes> To DataBase
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(productTypes);
                        await _context.SaveChangesAsync();
                    }
                }


                // Seeding ProductBrands From Json File
                if (!_context.ProductBrands.Any())
                {
                    // 1. Read All Data From types Json File As String

                    var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");

                    // 2. Transform String To C# Objects [List<ProductBrand>]

                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

                    // 3. Add List<ProductBrand> To DataBase
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(productBrands);
                        await _context.SaveChangesAsync();
                    }
                }
                // Seeding Products From Json File

                if (!_context.Products.Any())
                {
                    // 1. Read All Data From Json File As String
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");

                    // 2. Transform String To C# Object [List<Product>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3. Add List<Product> To DataBase
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public async Task InitializeIdentityAsync()
        {
            if(_identityDbContext.Database.GetPendingMigrations().Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }


            // Seeding 

            if(!_roleManager.Roles.Any())
            {
                
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin",
                });

                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                });
            }


            if(!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SupperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789",
                };

                var admin = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789",
                };

                await _userManager.CreateAsync(superAdminUser, "p@ssW0rd");
                await _userManager.CreateAsync(admin, "p@ssW0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");
                
            }
        }
    }
}
