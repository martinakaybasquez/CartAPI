using Cart_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cart_API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CartItemController : ControllerBase
{
    private static List<CartItem> items = new List<CartItem>()
    {
        new CartItem() { Id = 1, price = 25, product = "Shampoo", quantity = 10 },
        new CartItem() { Id = 2, price = 25, product = "Conditioner", quantity = 10 },
        new CartItem() { Id = 3, price = 30, product = "Toothbrush Head Replacements", quantity = 30 },
        new CartItem() { Id = 4, price = 15, product = "Wax Strips", quantity = 20 },
        new CartItem() { Id = 5, price = 18, product = "Face Wash", quantity = 15 },
        new CartItem() { Id = 6, price = 22, product = "Moisturizer", quantity = 8 },
        new CartItem() { Id = 7, price = 10, product = "Body Scrub", quantity = 5 },
        new CartItem() { Id = 8, price = 12, product = "Hair Mask", quantity = 12 },
        new CartItem() { Id = 9, price = 20, product = "Nail Polish", quantity = 10 },
        new CartItem() { Id = 10, price = 28, product = "Makeup Remover", quantity = 7 }
    };

    private static int nextId = 11;
    
    /*
    curl -X 'GET' \
       'http://localhost:5023/api/CartItem?maxPrice=15' \
       -H 'accept: * /*' 
    */
    
    [HttpGet()]
    public IActionResult GetAll(double? maxPrice = null, string? prefix = null, int? pageSize = null)
    {
        List<CartItem> everything = items;

        if (maxPrice != null)
        {
            everything = everything.Where(e => e.price <= maxPrice.Value).ToList();
        }
        if (prefix != null)
        {
            everything = everything.Where(e => e.product.ToLower().Contains(prefix)).ToList();
        }
        if (pageSize != null)
        {
            everything = everything.Where(e => e.Id <= pageSize.Value).ToList();
        }
        
        return Ok(everything);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        CartItem stuffs = items.FirstOrDefault(e => e.Id == id);
        
        if (stuffs == null)
        {
            return NotFound("Stuffs Not Found :-(");
        }

        return Ok(stuffs);
    }

    [HttpPost()]
    public IActionResult AddStuffs([FromBody] CartItem newStuffs)
    {
        newStuffs.Id = nextId;
        items.Add(newStuffs);
        nextId++;
        return Created($"/api/CartItem/{newStuffs.Id}", newStuffs);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStuffs(int id, [FromBody] CartItem updatedStuffs)
    {
        int index = items.FindIndex(i => i.Id == id);
        items[index] = updatedStuffs;
        return Created($"/api/CartItem/{updatedStuffs.Id}", updatedStuffs.Id);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteById(int id)
    {
        int index = items.FindIndex(e => e.Id == id);
        items.RemoveAt(index);
        return NoContent();
    }
}