﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using DspAdpcm.Compatibility;

namespace DspAdpcm
{
    internal static class Helpers
    {
        public const int BytesPerFrame = 8;
        public const int SamplesPerFrame = 14;
        public const int NibblesPerFrame = 16;

        public static int GetNibbleFromSample(int samples)
        {
            int frames = samples / SamplesPerFrame;
            int extraSamples = samples % SamplesPerFrame;
            int extraNibbles = extraSamples == 0 ? 0 : extraSamples + 2;

            return NibblesPerFrame * frames + extraNibbles;
        }

        public static int GetSampleFromNibble(int nibble)
        {
            int frames = nibble / NibblesPerFrame;
            int extraNibbles = nibble % NibblesPerFrame;
            int samples = SamplesPerFrame * frames;

            return samples + extraNibbles - (extraNibbles != 0 ? 2 : 0);
        }

        public static int GetNibbleAddress(int sample)
        {
            int frames = sample / SamplesPerFrame;
            int extraSamples = sample % SamplesPerFrame;

            return NibblesPerFrame * frames + extraSamples + 2;
        }

        public static int GetBytesForAdpcmSamples(int samples)
        {
            int extraBytes = 0;
            int frames = samples / SamplesPerFrame;
            int extraSamples = samples % SamplesPerFrame;

            if (extraSamples != 0)
            {
                extraBytes = (extraSamples / 2) + (extraSamples % 2) + 1;
            }

            return BytesPerFrame * frames + extraBytes;
        }

#if !(NET20 || NET35 || NET40)
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static short Clamp16(int value)
        {
            if (value > short.MaxValue)
                return short.MaxValue;
            if (value < short.MinValue)
                return short.MinValue;
            return (short)value;
        }

        public static int GetNextMultiple(int value, int multiple)
        {
            if (multiple <= 0)
                return value;

            if (value % multiple == 0)
                return value;

            return value + multiple - value % multiple;
        }

        public static void CheckStream(Stream stream, int minLength)
        {
            if (!stream.CanSeek)
            {
                throw new NotSupportedException("A seekable stream is required");
            }

            if (stream.Length < minLength)
            {
                throw new InvalidDataException($"File is only {stream.Length} bytes long");
            }
        }

        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (a1 == null || a2 == null) return false;
            if (a1 == a2) return true;
            if (a1.Length != a2.Length) return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (!a1[i].Equals(a2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public enum Endianness
        {
            BigEndian,
            LittleEndian
        }

        public static BinaryReader GetBinaryReader(Stream stream, Endianness endianness) =>
            endianness == Endianness.LittleEndian
                ? GetStream.GetBinaryReader(stream)
                : GetStream.GetBinaryReaderBE(stream);

        public static BinaryWriter GetBinaryWriter(Stream stream, Endianness endianness) =>
            endianness == Endianness.LittleEndian
                ? GetStream.GetBinaryWriter(stream)
                : GetStream.GetBinaryWriterBE(stream);
    }
}
