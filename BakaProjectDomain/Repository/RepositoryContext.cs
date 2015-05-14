using BakaProjectDomain.Domain;
using BakaProjectDomain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BakaProjectDomain.Repository {
	public class RepositoryContext : DbContext, IRepository {

		public DbSet<Exercise> Exercises { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Share> Shares { get; set; }
		public DbSet<Resource> Resources { get; set; }

		public RepositoryContext()
			: base("DefaultConnection") { // name in xml
		}

		IQueryable<Exercise> IRepository.Exercises {
			get { return Exercises; }
		}

		public void AddExercise(Exercise exercise) {
			Exercises.Add(exercise);
		}

		public void DeleteExercise(Exercise exercise) {
			Exercises.Remove(exercise);
		}

		public new void SaveChanges() {
			base.SaveChanges();
		}

		public void SetModified(object o) {
			this.Entry(o).State = EntityState.Modified;
		}


		IQueryable<User> IRepository.Users {
			get { return Users; }
		}

		public void AddUser(User user) {
			Users.Add(user);
		}


		public void DeleteTag(Tag tag) {
			Tags.Remove(tag);
		}


		IQueryable<Share> IRepository.Shares {
			get { return Shares; }
		}


		IQueryable<Resource> IRepository.Resources {
			get { return Resources; }
		}

		public void DeleteResource(Resource resource) {
			Resources.Remove(resource);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {

			modelBuilder.Entity<Exercise>()
					.HasMany(c => c.Tags)
					.WithRequired(o => o.Exercise)
					.WillCascadeOnDelete(true);
		}
	}
}