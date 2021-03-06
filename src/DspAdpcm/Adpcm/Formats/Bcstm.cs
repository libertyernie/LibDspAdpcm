﻿using System.IO;
using DspAdpcm.Adpcm.Formats.Configuration;
using DspAdpcm.Adpcm.Formats.Internal;
using DspAdpcm.Adpcm.Formats.Structures;
using static DspAdpcm.Helpers;

namespace DspAdpcm.Adpcm.Formats
{
    /// <summary>
    /// Represents a BCSTM file.
    /// </summary>
    public class Bcstm
    {
        private BCFstm BCFstm { get; set; }
        private static int HeaderSize => 0x40;

        /// <summary>
        /// The underlying <see cref="AdpcmStream"/> used to build the BCSTM file.
        /// </summary>
        public AdpcmStream AudioStream => BCFstm.AudioStream;

        /// <summary>
        /// Contains various settings used when building the BCSTM file.
        /// </summary>
        public BcstmConfiguration Configuration { get; set; } = new BcstmConfiguration();

        /// <summary>
        /// The size in bytes of the BCSTM file.
        /// </summary>
        public int FileSize => BCFstm.FileSize;

        /// <summary>
        /// Initializes a new <see cref="Bcstm"/> from an <see cref="AdpcmStream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="AdpcmStream"/> used to
        /// create the <see cref="Bcstm"/>.</param>
        /// <param name="configuration">A <see cref="BcstmConfiguration"/>
        /// to use for the <see cref="Bcstm"/></param>
        public Bcstm(AdpcmStream stream, BcstmConfiguration configuration = null)
        {
            if (stream.Channels.Count < 1)
            {
                throw new InvalidDataException("Stream must have at least one channel ");
            }

            BCFstm = new BCFstm { AudioStream = stream };
            Configuration = configuration ?? Configuration;
        }

        /// <summary>
        /// Initializes a new <see cref="Bcstm"/> by parsing an existing
        /// BCSTM file.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> containing 
        /// the BCSTM file. Must be seekable.</param>
        /// <param name="configuration">A <see cref="BcstmConfiguration"/>
        /// to use for the <see cref="Bcstm"/></param>
        public Bcstm(Stream stream, BcstmConfiguration configuration = null)
        {
            ReadStream(stream, Configuration);
        }

        /// <summary>
        /// Initializes a new <see cref="Bcstm"/> by parsing an existing
        /// BCSTM file.
        /// </summary>
        /// <param name="file">A <c>byte[]</c> containing 
        /// the BCSTM file.</param>
        /// <param name="configuration">A <see cref="BcstmConfiguration"/>
        /// to use for the <see cref="Bcstm"/></param>
        public Bcstm(byte[] file, BcstmConfiguration configuration = null)
        {
            using (var stream = new MemoryStream(file))
            {
                ReadStream(stream, Configuration);
            }
        }

        /// <summary>
        /// Parses the header of a BCSTM file and returns the metadata
        /// and structure data of that file.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> containing 
        /// the BCSTM file. Must be seekable.</param>
        /// <returns>A <see cref="BcstmStructure"/> containing
        /// the data from the BCSTM header.</returns>
        public static BcstmStructure ReadMetadata(Stream stream)
        {
            CheckStream(stream, HeaderSize);
            return (BcstmStructure)BCFstm.ReadBCFstmFile(stream, false);
        }

        /// <summary>
        /// Builds a BCSTM file from the current <see cref="AudioStream"/>.
        /// </summary>
        /// <returns>A BCSTM file</returns>
        public byte[] GetFile()
        {
            var file = new byte[FileSize];
            var stream = new MemoryStream(file);
            WriteFile(stream);
            return file;
        }

        /// <summary>
        /// Writes the BCSTM file to a <see cref="Stream"/>.
        /// The file is written starting at the beginning
        /// of the <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to write the
        /// BCSTM to.</param>
        public void WriteFile(Stream stream)
        {
            BCFstm.Configuration = Configuration.Configuration;
            BCFstm.WriteBCFstmFile(stream, BCFstm.BCFstmType.Bcstm);
        }

        private void ReadStream(Stream stream, BcstmConfiguration configuration = null)
        {
            BCFstm = new BCFstm(stream, configuration?.Configuration);
            Configuration.Configuration = BCFstm.Configuration;
        }
    }
}
