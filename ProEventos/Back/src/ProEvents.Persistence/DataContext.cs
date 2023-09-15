using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEvents.API.Models;
using ProEvents.Domain;

namespace ProEvents.API.Data
{
    public class ProEventsContext : DbContext
    {
        public ProEventsContext(DbContextOptions<ProEventsContext> options) : base(options)
        {
        }
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Batch> Batch { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventsSpeakers { get; set; }
        public DbSet<SocialNetwork> SocialMedia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSpeaker>()
            .HasKey(pe => new {pe.EventId, pe.SpeakerId});
        }
    }
}