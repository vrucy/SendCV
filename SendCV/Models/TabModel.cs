using Syncfusion.Windows.Shared;

namespace SendCV.Models
{
    public class TabModel: NotificationObject
    {
		public TabModel() { }
		private string _headername;
		public string HeaderName
		{
			get
			{
				return _headername;
			}
			set
			{
				_headername = value;
				this.RaisePropertyChanged("HeaderName");
			}
		}
	}

}
