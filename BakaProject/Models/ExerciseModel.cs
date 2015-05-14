using BakaProjectDomain.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakaProject.Models {
	public class ExerciseModel {
		public int Id { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
		public string Tip { get; set; }
		public string Title { get; set; }
		public string User { get; set; }
		public List<TagModel> Tags { get; set; }
		public Privacy Privacy { get; set; }
	}
}