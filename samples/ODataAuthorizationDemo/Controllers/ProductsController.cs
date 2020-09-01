﻿using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataAuthorizationDemo.Models;

namespace ODataAuthorizationDemo.Controllers
{
    public class ProductsController: ODataController
    {
        private AppDbContext _dbContext;

        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Get()
        {
            return Ok(_dbContext.Products);
        }

        public IActionResult Get(int key)
        {
            return Ok(_dbContext.Products.Find(key));
        }

        public async Task<IActionResult> Post([FromBody] Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return Ok(product);
        }

        public async Task<IActionResult> Update(int key, [FromBody] Delta<Product> delta)
        {
            var product = await _dbContext.Products.FindAsync(key);
            delta.Patch(product);
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return Ok(product);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var product = await _dbContext.Products.FindAsync(key);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok(product);
        }
    }
}
