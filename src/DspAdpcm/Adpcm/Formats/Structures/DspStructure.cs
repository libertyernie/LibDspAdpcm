﻿using System.Collections.Generic;
using static DspAdpcm.Helpers;

namespace DspAdpcm.Adpcm.Formats.Structures
{
    /// <summary>
    /// Defines the structure of a DSP file.
    /// </summary>
    public class DspStructure
    {
        internal DspStructure() { }

        /// <summary>
        /// The number of samples in the DSP.
        /// </summary>
        public int NumSamples { get; set; }
        /// <summary>
        /// The number of ADPCM nibbles in the DSP.
        /// </summary>
        public int NumNibbles { get; set; }
        /// <summary>
        /// The sample rate of the audio.
        /// </summary>
        public int SampleRate { get; set; }
        /// <summary>
        /// This flag is set if the DSP loops.
        /// </summary>
        public bool Looping { get; set; }
        /// <summary>
        /// The format of 
        /// </summary>
        public short Format { get; set; }
        /// <summary>
        /// The address, in nibbles, of the start
        /// loop point.
        /// </summary>
        public int StartAddress { get; set; }
        /// <summary>
        /// The address, in nibbles, of the end
        /// loop point.
        /// </summary>
        public int EndAddress { get; set; }
        /// <summary>
        /// The address, in nibbles, of the initial
        /// playback position.
        /// </summary>
        public int CurrentAddress { get; set; }
        /// <summary>
        /// The number of channels in the DSP file.
        /// Only used in multi-channel DSP files.
        /// </summary>
        public int NumChannels { get; set; }
        /// <summary>
        /// The number of ADPCM frames in each
        /// interleaved audio data block.
        /// Only used in multi-channel DSP files.
        /// </summary>
        public int FramesPerInterleave { get; set; }
        /// <summary>
        /// The ADPCM information for each channel.
        /// </summary>
        public IList<AdpcmChannelInfo> Channels { get; } = new List<AdpcmChannelInfo>();

        /// <summary>
        /// The start loop point in samples.
        /// </summary>
        public int LoopStart => GetSampleFromNibble(StartAddress);
        /// <summary>
        /// The end loop point in samples.
        /// </summary>
        public int LoopEnd => GetSampleFromNibble(EndAddress);
        internal byte[][] AudioData { get; set; }
    }
}
