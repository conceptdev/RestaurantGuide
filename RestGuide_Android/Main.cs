using System;
using Android.App;

using System.IO;
using System.Threading;
using Android.Widget;
using Android.Content;
using Android;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace RestGuide
{
    [Application]
    public class RestGuideApplication : Application
    {
        public List<Restaurant> Restaurants;

        public RestGuideApplication(IntPtr handle)
            : base(handle)
        { }

        public override void OnCreate()
        {
            base.OnCreate();
            Console.WriteLine("[RestGuideApplication] OnCreate");
            
            // I never said this was the best place for loading data
            // just that it works for now...
            var s = Resources.OpenRawResource(Resource.Raw.restaurants);
            using (TextReader reader = new StreamReader(s))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Restaurant>));
                Restaurants = (List<Restaurant>)serializer.Deserialize(reader);
            }
            Console.WriteLine("[RestGuideApplication] Loaded {0} restaurants", Restaurants.Count);
        }




        // readStream is the stream you need to read
        // writeStream is the stream you want to write to
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}