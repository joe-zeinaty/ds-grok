using Microsoft.EntityFrameworkCore;
using D_S_Grok.Models;

namespace D_S_Grok.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAttemptAnswer> QuizAttemptAnswers { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<ChallengeAttempt> ChallengeAttempts { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<XPTransaction> XPTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuestionOption>()
    .HasKey(qo => qo.OptionID);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Level>()
                .HasIndex(l => l.LevelNumber)
                .IsUnique();

            modelBuilder.Entity<LessonProgress>()
                .HasIndex(lp => new { lp.UserID, lp.LessonID })
                .IsUnique();

            modelBuilder.Entity<ChallengeAttempt>()
                .HasIndex(ca => new { ca.UserID, ca.ChallengeID })
                .IsUnique();

            modelBuilder.Entity<UserAchievement>()
                .HasIndex(ua => new { ua.UserID, ua.AchievementID })
                .IsUnique();
            
            modelBuilder.Entity<Level>().HasData(
    new Level { LevelID = 1, LevelNumber = 1, Title = "Pointer Fundamentals", Description = "Introduction to memory references and pointer concepts.", RequiredXP = 0 },
    new Level { LevelID = 2, LevelNumber = 2, Title = "Linked Lists", Description = "Introduction to nodes, head pointer, and traversal.", RequiredXP = 150 },
    new Level { LevelID = 3, LevelNumber = 3, Title = "Linked List Operations", Description = "Insertion, deletion, searching, and updating nodes.", RequiredXP = 350 }
);

modelBuilder.Entity<Lesson>().HasData(
    new Lesson { LessonID = 1, LevelID = 1, Title = "What is a Pointer?", Content = "A pointer is a variable that stores the memory address of another variable.", LessonOrder = 1, XPReward = 50 },
    new Lesson { LessonID = 2, LevelID = 1, Title = "Pointer Assignment and References", Content = "Pointers can reference values and be reassigned to point to different memory locations.", LessonOrder = 2, XPReward = 50 },

    new Lesson { LessonID = 3, LevelID = 2, Title = "What is a Node?", Content = "A node is a structure that stores data and a reference to the next node.", LessonOrder = 1, XPReward = 50 },
    new Lesson { LessonID = 4, LevelID = 2, Title = "Creating a Linked List", Content = "A linked list is created by connecting nodes together using pointers.", LessonOrder = 2, XPReward = 50 },
    new Lesson { LessonID = 5, LevelID = 2, Title = "Traversing a Linked List", Content = "Traversal means moving from one node to the next until the end of the list is reached.", LessonOrder = 3, XPReward = 50 },

    new Lesson { LessonID = 6, LevelID = 3, Title = "Inserting a Node", Content = "Insertion requires updating pointers so that the new node becomes part of the list.", LessonOrder = 1, XPReward = 50 },
    new Lesson { LessonID = 7, LevelID = 3, Title = "Deleting a Node", Content = "Deletion removes a node by updating the previous node pointer to skip the removed node.", LessonOrder = 2, XPReward = 50 },
    new Lesson { LessonID = 8, LevelID = 3, Title = "Searching and Updating Nodes", Content = "Searching checks each node until the target value is found, then the node data may be updated.", LessonOrder = 3, XPReward = 50 }
);

modelBuilder.Entity<Achievement>().HasData(
    new Achievement { AchievementID = 1, Title = "First Steps", Description = "Complete your first lesson.", RequirementType = "CompletedLessons", RequirementValue = 1, XPReward = 25 },
    new Achievement { AchievementID = 2, Title = "Pointer Explorer", Description = "Complete Level 1.", RequirementType = "CompletedLevel", RequirementValue = 1, XPReward = 50 },
    new Achievement { AchievementID = 3, Title = "List Master", Description = "Complete Level 2.", RequirementType = "CompletedLevel", RequirementValue = 2, XPReward = 50 },
    new Achievement { AchievementID = 4, Title = "Linked List Expert", Description = "Complete Level 3.", RequirementType = "CompletedLevel", RequirementValue = 3, XPReward = 100 },
    new Achievement { AchievementID = 5, Title = "Quiz Champion", Description = "Achieve 100% on any quiz.", RequirementType = "PerfectQuizScore", RequirementValue = 100, XPReward = 75 }
);

modelBuilder.Entity<Quiz>().HasData(
    new Quiz
    {
        QuizID = 1,
        LevelID = 1,
        Title = "Pointer Fundamentals Quiz",
        PassMark = 60,
        XPReward = 100
    }
);

modelBuilder.Entity<Question>().HasData(
    new Question
    {
        QuestionID = 1,
        QuizID = 1,
        QuestionText = "After assigning PTR = &X, what value does PTR store?",
        Explanation = "PTR stores the memory address of X, not the value stored inside X."
    },
    new Question
    {
        QuestionID = 2,
        QuizID = 1,
        QuestionText = "If X has value 10 and address 1001, what does &X represent?",
        Explanation = "&X represents the memory address of variable X."
    },
    new Question
    {
        QuestionID = 3,
        QuizID = 1,
        QuestionText = "What does NULL mean when used as a pointer value?",
        Explanation = "NULL means the pointer is not currently pointing to any valid memory address."
    }
);

modelBuilder.Entity<QuestionOption>().HasData(
    new QuestionOption { OptionID = 1, QuestionID = 1, OptionText = "10", IsCorrect = false },
    new QuestionOption { OptionID = 2, QuestionID = 1, OptionText = "1001", IsCorrect = true },
    new QuestionOption { OptionID = 3, QuestionID = 1, OptionText = "X", IsCorrect = false },
    new QuestionOption { OptionID = 4, QuestionID = 1, OptionText = "NULL", IsCorrect = false },

    new QuestionOption { OptionID = 5, QuestionID = 2, OptionText = "The value of X", IsCorrect = false },
    new QuestionOption { OptionID = 6, QuestionID = 2, OptionText = "The name of X", IsCorrect = false },
    new QuestionOption { OptionID = 7, QuestionID = 2, OptionText = "The address of X", IsCorrect = true },
    new QuestionOption { OptionID = 8, QuestionID = 2, OptionText = "The data type of X", IsCorrect = false },

    new QuestionOption { OptionID = 9, QuestionID = 3, OptionText = "The pointer has no target address", IsCorrect = true },
    new QuestionOption { OptionID = 10, QuestionID = 3, OptionText = "The pointer stores zero XP", IsCorrect = false },
    new QuestionOption { OptionID = 11, QuestionID = 3, OptionText = "The pointer stores the value 10", IsCorrect = false },
    new QuestionOption { OptionID = 12, QuestionID = 3, OptionText = "The pointer deletes the variable", IsCorrect = false }
);

modelBuilder.Entity<Challenge>().HasData(
    new Challenge
    {
        ChallengeID = 1,
        LevelID = 2,
        Title = "Create and Traverse a Linked List",
        Description = "Build a linked list visually and observe how nodes are connected through pointers.",
        ChallengeType = "ListTraversal",
        XPReward = 75
    },
    new Challenge
    {
        ChallengeID = 2,
        LevelID = 3,
        Title = "Linked List Operations Simulator",
        Description = "Practice insertion, deletion, and searching in a linked list.",
        ChallengeType = "ListOperations",
        XPReward = 100
    }
);
        }
    }
}