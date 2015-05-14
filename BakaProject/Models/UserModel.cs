using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BakaProject.Models {
	public class UserModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public string FacebookLink { get; set; }
		public string FacebookId { get; set; }
		public bool? TeacherAccount { get; set; }
	}
}