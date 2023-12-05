using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using testFS.Models;
using Infrastructure.Data;
using Domain.Interfaces;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Services;
using System.Drawing;
using System.Text.Json;
using Domain.Entities;


namespace testFS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //эту часть можно бы и отделить, но пока будет так
        private readonly MyDotsDBContext _myDBContext;
        private readonly IPointRepository _pointRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPointService _pointService;
        private readonly ICommentService _commentService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _myDBContext = new MyDotsDBContext();
            _pointRepository = new PointRepository(_myDBContext);
            _commentRepository = new CommentRepository(_myDBContext);
            _pointService = new PointService(_pointRepository);
            _commentService = new CommentService(_commentRepository);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pointService.GetAllPoints());            
        }

        [HttpPost]
        public IActionResult MovePoint([FromBody] MyDot item)
        {
            _pointService.MovePoint(item.ID, item.Cord_x, item.Cord_y);
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult ResizePoint([FromBody] MyDot item) 
        {
            _pointService.UpdatePointSize(item.ID, item.Radius);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult UpdateText([FromBody] MyComment item)
        {
            //_commentService.UpdateCommentText(item.ID, item.Text);
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult CreateComment([FromBody] MyComment item) 
        {
            //_commentService.AddComment(item);
            return Json(new { success = true });
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
