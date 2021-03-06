﻿using System;
using static DspAdpcm.Helpers;

namespace DspAdpcm.Adpcm.Formats.Configuration
{
    /// <summary>
    /// Contains the options used to build the IDSP file.
    /// </summary>
    public class IdspConfiguration
    {
        private int _bytesPerInterleave = BytesPerFrame * 2;

        /// <summary>
        /// If <c>true</c>, recalculates the loop context when building the file.
        /// If <c>false</c>, reuses the loop context read from the imported file,
        /// if available.
        /// Default is <c>true</c>.
        /// </summary>
        public bool RecalculateLoopContext { get; set; } = true;

        /// <summary>
        /// If <c>true</c>, trims the output file length to the set end loop point.
        /// If <c>false</c> or if the <see cref="Idsp"/> does not loop,
        /// the output file is not trimmed.
        /// if available.
        /// Default is <c>true</c>.
        /// </summary>
        public bool TrimFile { get; set; } = true;

        /// <summary>
        /// When building the audio file, the loop points and audio will
        /// be adjusted so that the start loop point is a multiple of
        /// this number. Default is 28.
        /// </summary>
        public int LoopPointAlignment { get; set; } = SamplesPerFrame * 2;

        /// <summary>
        /// The number of bytes in each block when interleaving
        /// the audio data.
        /// Must be divisible by 8.
        /// Default is 16.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if value is negative 
        /// or not divisible by 8.</exception>
        public int BytesPerInterleave
        {
            get { return _bytesPerInterleave; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        "Number of samples per interleave must be non-negative");
                }
                if (value % BytesPerFrame != 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value,
                        "Number of samples per interleave must be divisible by 14");
                }
                _bytesPerInterleave = value;
            }
        }
    }
}
