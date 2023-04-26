using Blog.Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Model.Entities.Abstract
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

		private DateTime _createdDate=DateTime.Now;

		public DateTime CreatedDate
		{
			get { return _createdDate; }
			set { _createdDate = value; }
		}


		private  Statu _statu=Statu.Confirmation;

		public Statu Statu
		{
			get { return _statu; }
			set { _statu = value; }
		}

        private string _StatuDescription = "";

        [NotMapped]
        public string StatuDescription
        {
            get
            {
                switch (Statu)
                {
                    case Statu.Active:
                        _StatuDescription= "Aktif";
                        break;
                    case Statu.Passive:
                        _StatuDescription = "Pasif";
                        break;
                    case Statu.Confirmation:
                        _StatuDescription = "Admin Onayında";
                        break;
                    case Statu.Modified:
                        _StatuDescription = "Düzenlendi";
                        break;
                    case Statu.Rejection:
                        _StatuDescription = "Red";
                        break;
                }
                return _StatuDescription;
            } 
        }
    }
}
