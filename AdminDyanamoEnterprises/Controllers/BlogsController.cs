using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository;
using Microsoft.AspNetCore.Mvc;

public class BlogsController : Controller
{
    private readonly IBlogsRepository _repository;

    public BlogsController(IBlogsRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var blogs = _repository.GetAllBlogs();
        return View(blogs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(BlogsModel blog)
    {
        if (ModelState.IsValid)
        {
            _repository.InsertBlog(blog);
            return RedirectToAction("Index");
        }
        return View(blog);
    }

    public IActionResult Edit(int id)
    {
        var blog = _repository.GetBlogById(id);
        return View(blog);
    }

    [HttpPost]
    public IActionResult Edit(BlogsModel blog)
    {
        if (ModelState.IsValid)
        {
            _repository.UpdateBlog(blog);
            return RedirectToAction("Index");
        }
        return View(blog);
    }

    public IActionResult Delete(int id)
    {
        var blog = _repository.GetBlogById(id);
        return View(blog);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _repository.DeleteBlog(id);
        return RedirectToAction("Index");
    }
}
