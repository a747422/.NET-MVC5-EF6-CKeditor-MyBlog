using System.Collections.Generic;
using System.Data.Entity;
using Project.Models;

namespace Project.DAL
{
    public class BlogInitalizer : DropCreateDatabaseIfModelChanges<BlogContext>
    {   
        //初始化数据库数据
        protected override void Seed(BlogContext context)
        {
            var sort = new List<ArticleSort>
            {
                new ArticleSort{SortArticleName="常用软件文章"},
                new ArticleSort{SortArticleName="前端技术文章"}
            };
            sort.ForEach(s => context.ArticleSort.Add(s));
            context.SaveChanges();
        }
    }
    
}
