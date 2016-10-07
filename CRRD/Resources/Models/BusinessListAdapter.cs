using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace CRRD.Resources.Models
{
    class BusinessListAdapter : BaseAdapter<string>
    {
        private List<string> _Items;
        private Context _context;

		// Start class to Get and parse the local XML file to the associated classes (Business & Category)
		private XMLHandeler _handler = new XMLHandeler();

        public BusinessListAdapter(Context context, List<string> items)
        {
            _context = context;
            _Items = items;
        }

        public override int Count
        {
            get { return _Items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return _Items[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(_context).Inflate(Resource.Layout.ListCategory_Row, null, false);
            }

            // WHAT EACH PART WILL DISPLAY

            TextView listIndex = row.FindViewById<TextView>(Resource.Id.txtIndex);
            listIndex.Text = (position + 1).ToString();

            TextView txtBusinessName = row.FindViewById<TextView>(Resource.Id.txtCategoryName);
            txtBusinessName.Text = _Items[position];

            // MAY NOT BE USED
            TextView txtSubcatCount = row.FindViewById<TextView>(Resource.Id.txtSubcatCount);

			// Check if the busienss has LatLng values
			Business B = _handler.GetBusinessByName(_Items[position]);
			int hasLatLngFlag = (B.Latitude == 0 || B.Longitude == 0) ? 0 : 1;

            //txtSubcatCount.Text = "";
			txtSubcatCount.Text = (hasLatLngFlag == 0) ? "" : "X";

            return row;
        }
    }
}