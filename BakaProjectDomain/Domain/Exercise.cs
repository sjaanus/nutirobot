using BakaProjectDomain.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakaProjectDomain.Extensions;

namespace BakaProjectDomain.Domain {
	public class Exercise {

		public Exercise() {
			Tags = new List<Tag>();
		}

		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Question { get; set; }
		public virtual string Answer { get; set; }
		public virtual string Tip { get; set; }
		public virtual List<Tag> Tags { get; set; }
		public virtual User User { get; set; }

		[Column("Privacy")]
		public string PrivacyString {
			get { return Privacy.ToString(); }
			private set { Privacy = EnumExtensions.ParseEnum<Privacy>(value); }
		}

		[NotMapped]
		public virtual Privacy Privacy { get; set; }
	}
}
