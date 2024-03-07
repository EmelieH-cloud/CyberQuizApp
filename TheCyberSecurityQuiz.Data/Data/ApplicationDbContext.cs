using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCyberSecurityQuiz.Data.Models;


namespace TheCyberSecurityQuiz.UI.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SegmentModel> Segments { get; set; }
        public DbSet<UserResult> UserResults { get; set; }
        public DbSet<QuestionModel> Question { get; set; }
        public DbSet<SubcategoryModel> Subcategories { get; set; }
        public DbSet<SolutionModel> Solutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Beskrivning: en kategori inneh�ller flera segment men ett segment tillh�r bara en kategori. 
            // 1:N mellan category och segment:
            modelBuilder.Entity<SegmentModel>()
            .HasOne(s => s.Category)
            .WithMany(c => c.Segments)
            .HasForeignKey(s => s.CategoryId);

            // Beskrivning: en subkategori tillh�r ett segment men ett segment inneh�ller flera subkategorier. 
            // 1:N mellan segment och subcategory:
            modelBuilder.Entity<SubcategoryModel>()
            .HasOne(s => s.Segment)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(s => s.SegmentId);

            // Beskrivning: En subkategori inneh�ller flera fr�gor men en fr�ga tillh�r bara en subkategori. 
            // 1:N mellan subcategory och question:
            modelBuilder.Entity<QuestionModel>()
            .HasOne(s => s.SubCategory)
            .WithMany(c => c.Questions)
            .HasForeignKey(s => s.SubCategoryId);

            // Beskrivning: En fr�ga har bara en solution och en solution tillh�r bara en fr�ga. 
            // 1:1 mellan question och solution:
            modelBuilder.Entity<QuestionModel>()
           .HasOne(q => q.Solution)
           .WithOne(s => s.Question)
           .HasForeignKey<SolutionModel>(s => s.QuestionId);

            // Beskrivning: Ett svarsresultat tillh�r bara en fr�ga men en fr�ga kan ha m�nga svarsresultat
            //  1:N mellan result och question:
            modelBuilder.Entity<ResultModel>()
           .HasOne(r => r.Question)
           .WithMany(q => q.Results)
           .HasForeignKey(s => s.QuestionId);


            // Beskrivning: en user kan ha m�nga svarsresultat och ett svarsresultat kan has av m�nga users 
            // M:N mellan user och result
            modelBuilder.Entity<UserResult>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserResults)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserResult>()
                .HasOne(u => u.Result)
                .WithMany(t => t.UserResults)
                .HasForeignKey(tt => tt.ResultId);


        }
    }
}
