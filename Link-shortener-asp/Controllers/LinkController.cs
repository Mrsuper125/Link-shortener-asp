using Link_shortener_asp.Data;
using Link_shortener_asp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Link_shortener_asp.Controllers;

[Route("/")]
public class LinkController : Controller
{
    private readonly LinkShortenerContext _context;

    public LinkController(LinkShortenerContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        ViewData["scheme"] = HttpContext.Request.Scheme;
        ViewData["host"] = HttpContext.Request.Host;
        return View("LinkForm"); //Return link creation form on index page
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateLink(string? fullLink) //When user submits the form
    {
        if (fullLink == null) //If there's no full link, return an error
        {
            return NoContent();
        }

        var newLink = new Link
        {
            FullLink = fullLink, ExpireDate = DateTime.Now + TimeSpan.FromDays(1)
        }; //Create new link lasting for 1 day


        _context.Links.Add(newLink);
        await _context.SaveChangesAsync();

        int id = newLink.Id; //Get the link id

        return Redirect("~/created/?id=" + id); //Redirect user to the page and display the link id on it
    }

    [HttpGet("created")]
    public IActionResult LinkCreated(string? id) //This page just displays the link
    {
        ViewData["scheme"] = HttpContext.Request.Scheme;
        ViewData["host"] = HttpContext.Request.Host;
        ViewData["id"] = id;
        return View("LinkCreated"); //Compose link of 3 parts: current schema, host and link id
    }

    [HttpGet("{id}")]
    public IActionResult LinkOpened(int id)
    {
        Link? found = _context.Links.Find([id]); //Get full link by short's id
        if (found == null) //No such link = 404
        {
            return NotFound();
        }

        if (found.ExpireDate < DateTime.Now) //Delete if expired
        {
            _context.Links.Remove(found);
            _context.SaveChangesAsync();
            return View("LinkExpired"); //TODO: change to static file
        }
        
        found.Clicks++;
        _context.Links.Update(found); //Update link usage statistics
        _context.SaveChangesAsync();

        return Redirect(found.FullLink); //Finally redirect the user
    }
}