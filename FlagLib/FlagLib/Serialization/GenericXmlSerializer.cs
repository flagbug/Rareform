using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FlagLib.Serialization
{
    /// <summary>
    /// Provides a static and generic xml serializer to serialize IEnumerables an single objects
    /// </summary>
    public static class GenericXmlSerializer
    {
        /// <summary>
        /// Saves the specified collection at the specified path.
        /// </summary>
        /// <typeparam name="T">Type of the items to serialize</typeparam>
        /// <param name="items">The items tho serialize.</param>
        /// <param name="path">The path of the file.</param>
        public static void SaveCollection<T>(ICollection<T> items, string path) where T : new()
        {
            XmlSerializer serializer = new XmlSerializer(items.GetType());

            using (TextWriter writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, items);
            }
        }

        /// <summary>
        /// Saves the specified item at the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item to serialize.</param>
        /// <param name="path">The path of the file.</param>
        public static void SaveSingle<T>(T item, string path) where T : new()
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());

            using (TextWriter writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, item);
            }
        }

        /// <summary>
        /// Reads the items at the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the serialized items</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <returns>The deserialized items</returns>
        public static IEnumerable<T> ReadEnumerable<T>(string path) where T : new()
        {
            List<T> items = new List<T>();

            XmlSerializer serializer = new XmlSerializer(items.GetType());
            TextReader reader = new StreamReader(path);

            try
            {
                items = (List<T>)serializer.Deserialize(reader);
            }

            catch (InvalidOperationException)
            {
                throw;
            }

            finally
            {
                reader.Close();
            }

            return items;
        }

        /// <summary>
        /// Reads the item at the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the serialized item</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <returns>The deserialized item.</returns>
        public static T ReadSingle<T>(string path) where T : new()
        {
            T item = new T();

            XmlSerializer serializer = new XmlSerializer(item.GetType());
            TextReader reader = new StreamReader(path);

            try
            {
                item = (T)serializer.Deserialize(reader);
            }

            catch (InvalidOperationException)
            {
                throw;
            }

            finally
            {
                reader.Close();
            }

            return item;
        }
    }
}