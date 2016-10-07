using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;

namespace CRRD.Resources.Models
{
    class CategoryListAdapter : BaseAdapter<string>
    {
        private List<string> _Items;
        private Context _context;
        private XMLHandeler _handler = new XMLHandeler();

        public CategoryListAdapter(Context context, List<string> items)
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

            TextView txtCategoryName = row.FindViewById<TextView>(Resource.Id.txtCategoryName);
            txtCategoryName.Text = _Items[position];

            TextView txtSubcatCount = row.FindViewById<TextView>(Resource.Id.txtSubcatCount);
            ////    txtSubcatCount.Text = _Items[position].SubcategoryList.Count.ToString();
            txtSubcatCount.Text = GetAllPosibleSubcategoryCount(_Items[position]).ToString();

            return row;
        }

        /// <summary>
        /// Gets the count of all posible unique subcategories for a given category name
        /// </summary>
        /// <param name="categoryName">The name of Category requesting the subcategory count</param>
        /// <returns>The count of all possible unique subcategories for a given category name</returns>
        private int GetAllPosibleSubcategoryCount(string categoryName)
        {
            List<string> subcategories = new List<string>();

            foreach (var c in _handler.CategoryList)
            {
                if (c.Name == categoryName)
                {
                    // A category may have n number of subcategories
                    foreach (var sub in c.SubcategoryList)
                    {
                        subcategories.Add(sub);
                    }
                }
            }

            // Return the unique list of all possible subcategories for a given category name
            return subcategories.Distinct().ToList().Count();
        }
    }
}