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
        /// Serializes the specified collection at the specified path.
        /// </summary>
        /// <typeparam name="T">Type of the items to serialize</typeparam>
        /// <param name="collection">The items tho serialize.</param>
        /// <param name="path">The path of the file.</param>
        public static void SerializeCollection<T>(ICollection<T> collection, string path) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(collection.GetType());

            using (TextWriter writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, collection);
            }
        }

        /// <summary>
        /// Serializes the specified item to the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item to serialize.</param>
        /// <param name="path">The path of the file.</param>
        public static void SerializeItem<T>(T item, string path) where T : class, new()
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());

            using (TextWriter writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, item);
            }
        }

        /// <summary>
        /// Deserializes the collection from the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the serialized items</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// The deserialized collection.
        /// </returns>
        public static ICollection<T> Deserializecollection<T>(string path) where T : class, new()
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
        /// Deserializes the item at the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the serialized item</typeparam>
        /// <param name="path">The path of the file.</param>
        /// <returns>
        /// The deserialized item.
        /// </returns>
        public static T DeserializeItem<T>(string path) where T : class, new()
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