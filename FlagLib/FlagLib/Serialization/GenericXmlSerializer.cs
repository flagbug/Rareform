/*
 * This source is released under the MIT-license.
 *
 * Copyright (c) 2011 Dennis Daume
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 * and associated documentation files (the "Software"), to deal in the Software without restriction,
 * including without limitation the rights to use, copy, modify, merge, publish, distribute,
 * sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

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