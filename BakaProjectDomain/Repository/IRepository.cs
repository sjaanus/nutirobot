using BakaProjectDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BakaProjectDomain.Repository {
	public interface IRepository : IDisposable {
		IQueryable<Exercise> Exercises { get; }
		IQueryable<Share> Shares { get; }
		IQueryable<User> Users { get; }
		IQueryable<Resource> Resources { get; }

		void AddExercise(Exercise exercise);
		void AddUser(User user);


		void DeleteExercise(Exercise exercise);
		void DeleteTag(Tag tag);
		void DeleteResource(Resource resource);

		void SaveChanges();
		void SetModified(object o);
	}
}