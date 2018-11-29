using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.DAL;

namespace Project.Models
{

    public class ArticleSort
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SortArticleId { get; set; }
        [Required]
        [Display(Name = "分类名称")]
        public string SortArticleName { get; set; }
    }

    public class Article
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "文章ID")]
        public int ArticleId { get; set; }
        [Required]
        public int SortArticleId { get; set; }
        [Required]
        [Display(Name = "所属分类")]
        public string SortArticleName { get; set; }
        [Required]
        [Display(Name = "文章封面")]
        public string ArticleCover { get; set; }        
        [Required]
        [Display(Name = "文章名")]
        public string ArticleName { get; set; }       
        [Required]
        [Display(Name = "文章内容")]
        public string ArticleContent { get; set; }
        [Display(Name = "文章发布时间")]
        public string ArticleTime { get; set; }
        [Required]
        [Display(Name = "文章模式")]
        public string ArticleType { get; set; }
        [Display(Name = "文章点击人数")]
        public int ArticleClick { get; set; }
        [Display(Name = "发布用户")]
        public string UserId { get; set; }

        public string ArticleUp { get; set; }

        public string ArticleSupport { get; set; }
    }

    public class ArticleListModel
    {
        
        /// <summary>
        /// 文章
        /// </summary>
        public IList<Article> ArtcleList { get; set; }

        /// <summary>
        /// 热门文章
        /// </summary>
        public IList<Article> ArtcileHot { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public Article ArtcleContent { get; set; }

        /// <summary>
        /// 留言内容列表
        /// </summary>
        public IList<LeavaMessage> Msg { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public  LeavaMessage LeavaMsg { get; set; }
        /// <summary>
        /// 壁纸&句子
        /// </summary>
        public IList<BackImage> BackImage { get; set; }
    }

}