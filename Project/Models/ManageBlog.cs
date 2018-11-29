

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class FirendlyLink
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LinkId { get; set; }
        [Required]
        public string LinkName { get; set; }
        [Required]
        public string LinkUrl { get; set; }
  
        public string LinkLogo { get; set; }

        public int ShowOrder { get; set; }
    }
        public class LeavaMessage
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        [Required]
        [Display(Name = "昵称")]
        public string MessageName { get; set; }    
        [Display(Name = "联系邮箱")]
        public string MessageEmali { get; set; }
        [Required]
        [Display(Name = "留言内容")]
        public string MessageContent { get; set; }
        [Required]
        public string MessageTime { get; set; }
        
    }
    public class BackImage
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int BackImgId { get; set; }      
        [Required]
        [Display(Name = "壁纸")]
        public string  BackImg { get; set; }
        [Required]
        [Display(Name = "句子")]
        public string BackImgSentence { get; set; }
        [Required]
        [Display(Name = "发布时间")]
        public string BackTime { get; set; }    
        [Display(Name = "发布用户")]
        public string UserId { get; set; }
        public string BackImgUp { get; set; }
    }

    public class ManageListModel
    {
        
        /// <summary>
        /// 文章内容
        /// </summary>
        public IList<Article> ArtcleList { get; set; }
        /// <summary>
        /// 壁纸&句子
        /// </summary>
        public IList<BackImage> BackImage { get; set; }

    }
}