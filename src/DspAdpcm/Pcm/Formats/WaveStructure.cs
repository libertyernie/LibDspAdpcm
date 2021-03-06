﻿using System;

namespace DspAdpcm.Pcm.Formats
{
    /// <summary>
    /// Defines the structure and metadata
    /// of a WAVE file
    /// </summary>
    public class WaveStructure
    {
        /// <summary>
        /// The size of the RIFF chunk.
        /// </summary>
        public int RiffSize { get; set; }

        /// <summary>
        /// The audio format.
        /// </summary>
        public int FormatTag { get; set; }
        /// <summary>
        /// The number of channels in the WAVE file.
        /// </summary>
        public int NumChannels { get; set; }
        /// <summary>
        /// The audio sample rate.
        /// </summary>
        public int SampleRate { get; set; }
        /// <summary>
        /// The byte rate of the audio file.
        /// </summary>
        public int AvgBytesPerSec { get; set; }
        /// <summary>
        /// The number of bytes for one sample
        /// including all channels.
        /// </summary>
        public int BlockAlign { get; set; }
        /// <summary>
        /// The number of bits per audio sample.
        /// </summary>
        public int BitsPerSample { get; set; }
        /// <summary>
        /// Size of the extension in bits.
        /// </summary>
        public int CbSize { get; set; }
        /// <summary>
        /// Specifies the precision of the sample in bits.
        /// </summary>
        public int ValidBitsPerSample { get; set; }
        /// <summary>
        /// Speaker position mask.
        /// </summary>
        public uint ChannelMask { get; set; }
        /// <summary>
        /// Specifies the subformat.
        /// </summary>
        public Guid SubFormat { get; set; }
        /// <summary>
        /// The number of bytes per audio sample,
        /// rounded up if not an integer.
        /// </summary>
        public int BytesPerSample => BitsPerSample.DivideByRoundUp(8);
        /// <summary>
        /// The number of samples in the audio file.
        /// </summary>
        public int NumSamples { get; set; }
        internal short[][] AudioData { get; set; }
    }
}
