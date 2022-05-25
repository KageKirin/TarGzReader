using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Collections.Generic;

using UnityEngine;

namespace KageKirin.TarGzReader
{
    public class Tar : IDisposable
    {
        public Dictionary<string, byte[]> dictionary { get; }

        /// <summary>
        /// Constructor from a <c>tar</c> archive filename
        /// </summary>
        /// <param name="filename">The <i>.tar</i> to read.</param>
        public Tar(string filename) : this(File.OpenRead(filename)) { }

        /// <summary>
        /// Constructor from a <c>tar</c> archive stream
        /// </summary>
        /// <param name="stream">The <i>.tar</i> data stream to read.</param>
        public Tar(Stream stream)
        {
            dictionary = new Dictionary<string, byte[]>();
            ReadStream(stream);
        }

        public void Dispose()
        {
            // TODO
        }

        /// <summary>
        /// Reads the stream this instance has been constructed with
        /// </summary>
        /// <param name="stream">The <i>.tar</i> data stream to read.</param>
        private void ReadStream(Stream stream)
        {
            var buffer = new byte[100];
            while (true)
            {
                stream.Read(buffer, 0, 100);
                var name = Encoding.ASCII.GetString(buffer).Trim('\0');
                Debug.Log($"entry: {name}");

                if (String.IsNullOrWhiteSpace(name))
                    break;

                stream.Seek(24, SeekOrigin.Current);
                stream.Read(buffer, 0, 12);
                var size = Convert.ToInt64(
                    Encoding.UTF8.GetString(buffer, 0, 12).Trim('\0').Trim(),
                    8
                );

                stream.Seek(376L, SeekOrigin.Current);

                Debug.Log($"{name} -> {size} bytes");

                if (!name.Equals("./", StringComparison.InvariantCulture))
                {
                    var buf = new byte[size];
                    stream.Read(buf, 0, buf.Length);

                    if (!name.Contains("PaxHeader"))
                    {
                        dictionary[name] = buf;
                    }
                }

                var pos = stream.Position;
                var offset = 512 - (pos % 512);
                if (offset == 512)
                    offset = 0;

                stream.Seek(offset, SeekOrigin.Current);
            }
        }
    }
}
