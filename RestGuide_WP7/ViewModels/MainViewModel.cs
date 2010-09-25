using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using System.Xml.Serialization;
using System.Collections.Generic;


namespace RestGuide
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Restaurants = new ObservableCollection<Restaurant>();
        }

        public ObservableCollection<Restaurant> Restaurants { get; private set; }



        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
       // public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            #region load data from XML
            StreamResourceInfo sr = Application.GetResourceStream(new Uri(@"SampleData\restaurants.xml", UriKind.Relative));
            using (TextReader reader = new StreamReader(sr.Stream))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Restaurant>));
                var restaurants = (ObservableCollection<Restaurant>)serializer.Deserialize(reader);


                var rt = new Rot13Table();
                foreach (var r in restaurants)
                {
                    Restaurants.Add(r);
                    //r.Text = rt.Transform(r.Text);
                    //r1.Add(r);
                }


                //var serializer1 = new XmlSerializer(typeof(List<Restaurant>));
                //var writer = new System.IO.StringWriter();
                //serializer1.Serialize(writer, r1);
                //var a = writer.ToString();
            }
            #endregion



           

            this.IsDataLoaded = true;
        }
        //List<Restaurant> r1 = new List<Restaurant>();

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }




    /// <summary>
    /// Contains the ROT13 text cipher code.
    /// </summary>
    class Rot13Table
    {
        /// <summary>
        /// Lookup table to shift characters.
        /// </summary>
        char[] _shift = new char[char.MaxValue];

        /// <summary>
        /// Generates the lookup table.
        /// </summary>
        public Rot13Table()
        {
            // Set these as the same.
            for (int i = 0; i < char.MaxValue; i++)
            {
                _shift[i] = (char)i;
            }
            // Specify capital letters are shifted.
            for (char c = 'A'; c <= 'Z'; c++)
            {
                char x;
                if (c <= 'M')
                {
                    x = (char)(c + 13);
                }
                else
                {
                    x = (char)(c - 13);
                }
                _shift[(int)c] = x;
            }
            // Specify lowercase letters are shifted.
            for (char c = 'a'; c <= 'z'; c++)
            {
                char x;
                if (c <= 'm')
                {
                    x = (char)(c + 13);
                }
                else
                {
                    x = (char)(c - 13);
                }
                _shift[(int)c] = x;
            }
        }

        /// <summary>
        /// Rotate the specified text with ROT13.
        /// </summary>
        public string Transform(string value)
        {
            try
            {
                // Convert to char array.
                char[] a = value.ToCharArray();
                // Shift each letter we need to shift.
                for (int i = 0; i < a.Length; i++)
                {
                    int t = (int)a[i];
                    a[i] = _shift[t];
                }
                // Return new string.
                return new string(a);
            }
            catch
            {
                // Just return original value on failure.
                return value;
            }
        }
    }
}