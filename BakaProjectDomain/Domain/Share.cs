using BakaProjectDomain.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BakaProjectDomain.Extensions;

namespace BakaProjectDomain.Domain {
	public class Share {

		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Question { get; set; }
		public virtual string Answer { get; set; }
		public virtual string Tip { get; set; }
		public virtual string Tags { get; set; }
		public virtual string Hash { get; set; }
		public virtual User User { get; set; }
	}
}
