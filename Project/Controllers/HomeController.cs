using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.DAL;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext db = new BlogContext();

        public HomeController()
        {
        }
        public ActionResult Index()
        {
            string sql = " Select TOP 3 * From BackImage Order By newID() ";
            List<Article> articleList = db.Article.Where(w => w.ArticleType == "1").OrderByDescending(q => q.ArticleClick).Take(10).ToList();
            //只显示公开文章
            var articleNow = db.Article.Where(w => w.ArticleType == "1").OrderByDescending(q => q.ArticleTime).Take(10).ToList();
            var model = new ArticleListModel();
            model.BackImage = db.Back.SqlQuery(sql).ToList();
            model.ArtcleList = articleNow;
            model.ArtcileHot = articleList;
            return View(model);
        }
        /// <summary>
        /// 前端技术文章
        /// </summary>
        /// <returns></returns>
        public ActionResult WebIndex()
        {
            List<Article> articleList = db.Article.Where(w => w.ArticleType == "1").OrderByDescending(q => q.ArticleClick).Take(10).ToList();
            var articleNow = db.Article.Where(w=>w.SortArticleId==2).Where(w=>w.ArticleType=="1").OrderByDescending(q => q.ArticleTime).Take(10).ToList();
            var model = new ArticleListModel();
            model.ArtcleList = articleNow;
            model.ArtcileHot = articleList;
            return View(model);
        }

        /// <summary>
        /// 后端技术文章
        /// </summary>
        /// <returns></returns>
        public ActionResult BackIndex()
        {
            List<Article> articleList = db.Article.Where(w => w.ArticleType == "1").OrderByDescending(q => q.ArticleClick).Take(10).ToList();
            var articleNow = db.Article.Where(w => w.SortArticleId == 1).Where(w => w.ArticleType == "1").OrderByDescending(q => q.ArticleTime).Take(10).ToList();
            var model = new ArticleListModel();
            model.ArtcleList = articleNow;
            model.ArtcileHot = articleList;
            return View(model);
        }
        //public ActionResult ArticleList()
        //{
        //    return View(db.Article);
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ArticleContent(int articleId)
        {
            //阅读量
            var articleCount = db.Article.Where(q => q.ArticleId == articleId).FirstOrDefault();
            articleCount.ArticleClick += 1;
            db.SaveChanges();

            //文章内容
            Article articleContent = db.Article.Where(q => q.ArticleId== articleId).FirstOrDefault();
            //top10热门文章
            List<Article> articleList = db.Article.Where(w => w.ArticleType == "1").OrderByDescending(q => q.ArticleClick).Take(10).ToList();

            var model = new ArticleListModel();  
            model.ArtcileHot = articleList;
            model.ArtcleContent = articleContent;
            return View(model);
        }
        /// <summary>
        /// GET 留言墙
        /// </summary>
        /// <returns></returns>
        public ActionResult LeavaMassage()
        {
        
            List<LeavaMessage> msg = db.Msg.OrderByDescending(q => q.MessageTime).Take(10).ToList();
            var model = new ArticleListModel();
            model.Msg = msg;
            return View(model);
        }

        /// <summary>
        /// POST 留言墙
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LeavaMassage(ArticleListModel model)
        {
            model.LeavaMsg.MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            db.Msg.Add(model.LeavaMsg);
            db.SaveChanges();
            List<Article> articleList = db.Article.OrderByDescending(q => q.ArticleClick).Take(10).ToList();
          
            model.ArtcileHot = articleList;
            model.Msg = db.Msg.ToList();
            return View(model);
        }
    }
}