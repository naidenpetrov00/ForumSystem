namespace ForumSystem.Web.ViewModels.Posts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using ForumSystem.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class PostCreateInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        public IEnumerable<SelectListItem> SelectListCategories => this.Categories?.Select(x => new SelectListItem(x.Name, x.Id.ToString()));
    }
}
