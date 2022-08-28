using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Context;

public class AppDataContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<Chat> Chats { get; set; }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<AppRole> Roles { get; set; }




    public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Message>(mess =>
        {

            mess.HasOne(m => m.User).WithMany(u => u.Messages).HasForeignKey(m => m.UserId);


            mess.HasOne(m => m.Chat).WithMany(c => c.Messages).HasForeignKey(m => m.ChatId);
        });

        modelBuilder.Entity<Chat>(chat =>
        {

            chat.HasMany(chat => chat.Users)
                .WithMany(user => user.Chats)
                .UsingEntity<UserChat>(
                    uc => uc.HasOne<AppUser>(uc => uc.User)
                        .WithMany(u => u.UserChatKeys)
                        .HasForeignKey(uc => uc.UserId),
                    uc => uc.HasOne<Chat>(uc => uc.Chat)
                         .WithMany(s => s.UserChatKeys)
                         .HasForeignKey(uc => uc.ChatId),

                    uc => uc.HasKey(uc => new { uc.UserId, uc.ChatId })
                );

        });

        modelBuilder.Entity<AppRole>().HasData(new AppRole {Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "Admin".ToUpper() }) ;


        modelBuilder.Entity<Chat>().HasData(
                  new Chat() { Id = Guid.NewGuid(), ChatName = "Some chat",DateCreated = DateTime.Now,IsPrivate=false },
                  new Chat() { Id = Guid.NewGuid(), ChatName = "Group chat", DateCreated = DateTime.Now, IsPrivate = false },
                  new Chat() { Id = Guid.NewGuid(), ChatName = "Work chat", DateCreated = DateTime.Now, IsPrivate = false }
                  );


        base.OnModelCreating(modelBuilder);

    }
}
