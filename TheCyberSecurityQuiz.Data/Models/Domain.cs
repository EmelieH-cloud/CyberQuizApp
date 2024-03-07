using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TheCyberSecurityQuiz.UI.Data;

namespace TheCyberSecurityQuiz.Data.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public List<SegmentModel> Segments { get; set; } = null!; // one-to-many med segments

    }
    public class SegmentModel
    {
        [Key]

        public int Id { get; set; }
        public int SegmentNumber { get; set; }
        public int CategoryId { get; set; }  // one-to-many med category
        public CategoryModel Category { get; set; } // one-to-many med category
        public List<SubcategoryModel> Subcategories { get; set; } = null!; // one-to-many med subcategories
    }

    public class SubcategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public int SegmentId { get; set; }  // one-to-many med SegmentModel
        public SegmentModel Segment { get; set; }  // one-to-many med SegmentModel
        public List<QuestionModel> Questions { get; set; } = null!; // one-to-many med Questionsmodel
    }

    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }
        public SolutionModel Solution { get; set; } = null!; // one-to-one med Solutionmodel
        public int SubCategoryId { get; set; }  // one-to-many med SubcategoryModel
        public SubcategoryModel SubCategory { get; set; } // one-to-many med SubcategoryModel

        public List<ResultModel> Results { get; set; } // one-to-many med Resultmodel
    }


    public class SolutionModel
    {
        [Key]
        public int Id { get; set; }
        public string CorrectAnswer { get; set; } = null!;
        public int QuestionId { get; set; } // one-to one med QuestionModel
        public QuestionModel Question { get; set; } // one-to one med QuestionModel
    }

    public class ResultModel
    {
        [Key]
        public int Id { get; set; }
        public string Answer { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }  // one-to-many med QuestionModel
        public QuestionModel Question { get; set; } // one-to-many med QuestionModel
        public List<UserResult> UserResults { get; set; } // many-to-many med user
    }


    [PrimaryKey("UserId", new string[] { "ResultId" })]
    public class UserResult // junction table 
    {

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ResultId { get; set; }
        public ResultModel Result { get; set; }
    }
}
