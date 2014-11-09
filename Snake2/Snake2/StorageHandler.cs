using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace Snake2
{
    [XmlInclude(typeof(Resources.SettingsLibrary)), XmlInclude(typeof(Resources.DataLibrary))]
    class StorageHandler
    {
        StorageDevice device;
        StorageContainer container;

        public T Load<T>(string filename)
        {
            GetDevice();
            GetContainer("save files");

            T t;

            if (!container.FileExists(filename))
            {
                container.CreateFile(filename);

            }

            using (Stream stream = container.OpenFile(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                t = (T)serializer.Deserialize(stream);
            }

            container.Dispose();
            container = null;

            return t;
        }

        public void Save<T>(string filename, T obj)
        {
            GetDevice();
            GetContainer("save files");

            if (obj == null)
            {
                
            }
            else
            {
                using (Stream stream = container.OpenFile(filename, FileMode.Create))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stream, obj);
                }
            }
        }

        public void GetDevice()
        {
            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            result.AsyncWaitHandle.WaitOne();
            device = StorageDevice.EndShowSelector(result);
            result.AsyncWaitHandle.Close();
        }

        public void GetContainer(string displayName)
        {
            IAsyncResult result = device.BeginOpenContainer(displayName, null, null);
            result.AsyncWaitHandle.WaitOne();
            container = device.EndOpenContainer(result);
            result.AsyncWaitHandle.Close();
        }
    }
}
