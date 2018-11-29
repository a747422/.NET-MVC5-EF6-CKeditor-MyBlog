using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Project.App_Start;
using Project.DAL;
using Project.Models;

namespace Project.Controllers
{

    public class AccountController3 : Controller
    {
        private BlogContext db = new BlogContext();

        public AccountController3()
        {
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = "EditArticle";
            return View();
        }
        //
        // Post: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginAccount m, string returnUrl)
        {
            var url = returnUrl;
            var user = db.Account.Where(db => db.UserName == m.UserName & db.UserPwd == m.UserPwd);
            if (user.Count() > 0)
            {
                return Redirect("EditArticle");
            }
            else
            {
                ModelState.AddModelError("", "无效的登录尝试。");
            }
            return View();
        }
        //重定向
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return RedirectToAction(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        public ActionResult EditArticle()
        {
            //Tuple<IEnumerable<Article>, IEnumerable<ArticleSort> model = new Tuple<IEnumerable<Article>, IEnumerable<ArticleSort>>(article, articleSort);
            ////从数据库中读取
            //var categoryList = new List<ArticleSort>() {
            //  new ArticleSort() { SortArticleId = 1, SortArticleName = "C#" },
            //    new ArticleSort() { SortArticleId = 2, SortArticleName = "Java" },
            //    new ArticleSort() { SortArticleId = 3, SortArticleName = "JavaScript" },
            //    new ArticleSort() { SortArticleId = 4, SortArticleName = "C" }
            //};
            //var selectItemList = new List<SelectListItem>() {
            //    new SelectListItem(){Value="0",Text="行业新闻",Selected=true}
            //};
            //var selectList = new SelectList(categoryList, "SortArticleId", "SortArticleName");
            //selectItemList.AddRange(selectList);
            ////ViewBag.database = selectItemList;
            //ViewData["HourList"] = selectItemList;

            var articleList = db.ArticleSort.OrderBy(q => q.SortArticleName).ToList();
            ViewData["HourList"] = new SelectList(articleList, "SortArticleId", "SortArticleName");

            ViewBag.Title = "编辑文章";
            return View();
        }


        //
        // Post: /Home/ReleaseArticle
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult EditArticle(Article article, string returnUr)
        {
            var imgstr = Request.Form["imgstr"] as string;
            var index = imgstr.IndexOf(";base64,") + 8;
            imgstr = imgstr.Substring(index, imgstr.Length- index);

            var img = ImgHelper.Base64StringToImage(imgstr);
            var rootPath =AppDomain.CurrentDomain.BaseDirectory;
            var imgPath = "img/" + Guid.NewGuid() + ".jpg";

            var articleList = db.ArticleSort.OrderBy(q => q.SortArticleName).ToList();
            ViewData["HourList"] = new SelectList(articleList, "SortArticleId", "SortArticleName");
            //一些文章内部属性
            var sort = db.ArticleSort.Where(w => w.SortArticleId == article.SortArticleId).FirstOrDefault();
            article.SortArticleName = sort.SortArticleName;
            article.ArticleTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            article.UserId = "admin";
            article.ArticleCover = imgPath;
            article.ArticleClick = 0;
            db.Article.Add(article);

            if (db.SaveChanges()>0)
            {
                if (img != null)
                {
                    img.Save(rootPath + imgPath);
                }
            }    
            return View();
        }


        //[HttpPost]
        //public JsonResult Upload(HttpPostedFileBase upload)
        //{
        //    string savePath = "/Upload/Image/";
        //    string dirPath = Server.MapPath(savePath);
        //    //如果目录不存在则创建目录 
        //    if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        //    //获取图片文件名及扩展名
        //    var fileName = Path.GetFileName(upload.FileName);
        //    string fileExt = Path.GetExtension(fileName).ToLower();
        //    //用时间来生成新文件名并保存
        //    string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt; upload.SaveAs(dirPath + "/" + newFileName);
        //    //上传成功后，我们还需要返回Json格式的响应
        //    return Json(
        //        new { uploaded = 1, fileName = newFileName, url = savePath + newFileName });
        //}

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>上传文件结果信息</returns>
        //[HttpPost]
        //public ActionResult UploadFile()
        //{
        //    HttpPostedFileBase file = Request.Files["CoverFile"];
        //    if (file != null)
        //    {
        //        try
        //        {
        //            var filename = Path.Combine(Request.MapPath("/Upload"), file.FileName);
        //            file.SaveAs(filename);
        //            return Content("上传成功");
        //        }
        //        catch (Exception ex)
        //        {
        //            return Content(string.Format("上传文件出现异常：{0}", ex.Message));
        //        }
        //    }
        //    else
        //    {
        //        return Content("没有文件需要上传！");
        //    }
        //}


    }
}