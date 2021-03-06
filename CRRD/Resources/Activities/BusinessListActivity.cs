using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using CRRD.Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CRRD.Resources.Adapters;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;

namespace CRRD.Resources.Activities
{
    /// <summary>
    /// Android Activity: Used for displaying the List of associated Businesses for the given Category and Subcategory.
    /// </summary>
    /// <seealso cref="Android.App.Activity" />
    [Activity(Label = "@string/BusinessListActivityLabel", Icon = "@drawable/CSCLogo")]
    public class BusinessListActivity : AppCompatActivity
    {
        private ListView _ListView;
        private Button _MapView;
        private string _categoryName, _subcategoryName;
        private List<string> _businessList = new List<string>();

        // Start class to Get and parse the local XML file to the associated classes (Business & Category)
        private DataHandler _handler = new DataHandler();

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            ErrorCheckActivity.checkDataHandlerInitialization(this.ApplicationContext, _handler.isInitialized);

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ListBusiness);

            //Set the toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = this.ApplicationContext.GetString(Resource.String.ApplicationName);

            //Getting Views
            _ListView = FindViewById<ListView>(Resource.Id.lvListArea);
            _MapView = FindViewById<Button>(Resource.Id.btnToMapView);

            // Get the passed categoryName
            _categoryName = Intent.GetStringExtra("categoryName") ?? "No Data Found";
            _subcategoryName = Intent.GetStringExtra("subcategoryName") ?? "No Data Found";

            // Get unique List of all possible Businesses for the given categoryName & subcategoryName
            GetBusinessList(_categoryName, _subcategoryName);

            // Set the custom adapter
            BusinessListAdapter adapter = new BusinessListAdapter(this, _businessList);
            _ListView.Adapter = adapter;

            // Events ...
            _ListView.ItemClick += _ListView_ItemClick;
            _MapView.Click += _MapView_Click;
        }

        /// <summary>
        /// Gets the list of all possible business names for a given categoryName and subcategoryName combination key.
        /// </summary>
        /// <param name="categoryName">Name of the category.</param>
        /// <param name="subcategoryName">Name of the subcategory.</param>
        /// <returns>
        /// A sorted, uniqe list of all possible business names for the given categoryName and subcategoryName combination key.
        /// </returns>
        private List<string> GetBusinessList(string categoryName, string subcategoryName)
        {
            foreach (var b in _handler.BusinessList)
            {
                // A business may have n number of categories
                foreach (var c in b.CategoryList)
                {
                    if (c.Name == categoryName)
                    {
                        // A category may have n number of subcategories
                        foreach (var sc in c.SubcategoryList)
                        {
                            if (sc == subcategoryName)
                            {
                                _businessList.Add(b.Name);
                            }
                        }
                    }
                }
            }
            _businessList = _businessList.Distinct().ToList();
            _businessList.Sort();

            return _businessList;
        }

        private void _ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(BusinessDetailsActivity));
            intent.PutExtra("categoryName", _categoryName);
            intent.PutExtra("subcategoryName", _subcategoryName);
            intent.PutExtra("businessName", _businessList[e.Position]);
            StartActivity(intent);
        }

        private void _MapView_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(MapViewerActivity));
            intent.PutExtra("categoryName", _categoryName);
            intent.PutExtra("subcategoryName", _subcategoryName);
            StartActivity(intent);
        }

        /// <summary>
		/// Creates the menu for the Toolbar/Action Bar to use
		/// </summary>
		/// <param name="menu">The menu</param>
		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Layout.Menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// Manages on-click actions when menu options are selected
        /// </summary>
        /// <param name="item">The menu</param>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId.Equals(Resource.Id.menu_home))
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                return base.OnOptionsItemSelected(item);
            }
            else if (item.ItemId.Equals(Resource.Id.menu_about))
            {
                var intent = new Intent(this, typeof(AboutActivity));
                StartActivity(intent);
                return base.OnOptionsItemSelected(item);
            }
            else if (item.ItemId.Equals(Resource.Id.menu_contact))
            {
                var intent = new Intent(this, typeof(ContactActivity));
                StartActivity(intent);
                return base.OnOptionsItemSelected(item);
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}