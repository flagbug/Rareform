using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using FlagLib.Extensions;

namespace FlagLib.Serialization
{
    /// <summary>
    /// Provides a static and generic xml serializer to serialize collections an single objects.
    /// </summary>
    public static class GenericXmlSerializer
    {
        /// <summary>
        /// Serializes the specified collection at the specified path.
        /// </summary>
        /// <typeparam name="T">Type of the items to serialize.</typeparam>
        /// <param name="collection">The items to serialize.</param>
        /// <param name="path">The path of the file.</param>
        public static void SerializeCollection<T>(ICollection<T> collection, string path) where T : class
        {
            collection.ThrowIfNull(() => collection);
            path.ThrowIfNull(() => path);

            GenericXmlSerializer.InternalSerialize(collection, path);
        }

        /// <summary>
        /// Serializes the specified item to the specified path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item to serialize.</param>
        /// <param name="path">The path of the file.</param>
        public static void SerializeItem<T>(T item, string path) where T : class
        {
            item.ThrowIfNull(() => item);
            path.ThrowIfNull(() => path);

            GenericXmlSerializer.InternalSerialize(item, path);
        }

        /// <summary>
        /// Deserializes the collection from the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the serialized items</typeparam>
        /// <param name="path">The path of the file with the deserialized data.</param>
        /// <returns>
        /// The deserialized collection.
        /// </returns>
        public static ICollection<T> DeserializeCollection<T>(string path) where T : class, new()
        {
            path.ThrowIfNull(() => path);

            return GenericXmlSerializer.InternDeserialize<Collection<T>>(path);
        }

        /// <summary>
        /// Deserializes the item at the specified path.
        /// </summary>
        /// <typeparam name="T">The type of the serialized item</typeparam>
        /// <param name="path">The path of the file with the serialized data.</param>
        /// <returns>
        /// The deserialized item.
        /// </returns>
        public static T DeserializeItem<T>(string path) where T : class, new()
        {
            path.ThrowIfNull(() => path);

            return GenericXmlSerializer.InternDeserialize<T>(path);
        }

        /// <summary>
        /// Executes the intern Serialize method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object">The object to serialize.</param>
        /// <param name="path">The path of the file.</param>
        private static void InternalSerialize<T>(T @object, string path) where T : class
        {
            @object.ThrowIfNull(() => @object);
            path.ThrowIfNull(() => path);

            var serializer = new XmlSerializer(@object.GetType());

            using (TextWriter writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, @object);
            }
        }

        /// <summary>
        /// Executes the intern Deserialize method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path of the file with the serialized data.</param>
        /// <returns></returns>
        private static T InternDeserialize<T>(string path) where T : class, new()
        {
            path.ThrowIfNull(() => path);

            T @object = new T();

            var serializer = new XmlSerializer(@object.GetType());

            using (TextReader reader = new StreamReader(path))
            {
                @object = (T)serializer.Deserialize(reader);
            }

            return @object;
        }
    }
}