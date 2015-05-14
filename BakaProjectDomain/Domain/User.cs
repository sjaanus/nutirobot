using BakaProjectDomain.Domain.Enums;
using BakaProjectDomain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakaProjectDomain.Domain {
	public class User {

		public User() {
			Tokens = new List<Token>();
			Exercises = new List<Exercise>();
		}
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual List<Token> Tokens { get; set; }
		public virtual string AuthenticationId { get; set; }
		public virtual List<Exercise> Exercises { get; set; }
		public virtual List<Share> Shares { get; set; }
		public virtual bool? TeacherAccount { get; set; }
		public virtual List<Resource> Resources { get; set; }

		[Column("AuthenticationType")]
		public string AuthenticationTypeString {
			get { return AuthenticationType.ToString(); }
			private set { AuthenticationType = EnumExtensions.ParseEnum<AuthenticationType>(value); }
		}

		[NotMapped]
		public virtual AuthenticationType AuthenticationType { get; set; }

		public string UserLink { get; set; }
	}

}
