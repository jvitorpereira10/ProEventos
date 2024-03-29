using Microsoft.EntityFrameworkCore;
using ProEvents.Domain;

namespace ProEvents.Persistence
{
    public class ProEventsContext : DbContext
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options) : base(options)
        {
        }
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventsSpeakers { get; set; }
        public DbSet<SocialNetwork> SocialMedia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSpeaker>()
                .HasKey(pe => new {pe.EventId, pe.SpeakerId});

            modelBuilder.Entity<Event>()
                .HasMany(e => e.SocialMedia)
                .WithOne(sm => sm.Event)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Speaker>()
                .HasMany(e => e.SocialMedia)
                .WithOne(sm => sm.Speaker)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}