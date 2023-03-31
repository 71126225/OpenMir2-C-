﻿using System;

namespace Cowboy.Sockets
{
    /// <summary>
    /// 固定长度帧构建器
    /// </summary>
    public sealed class FixedHeaderLengthFrameBuilder : FrameBuilder
    {
        public FixedHeaderLengthFrameBuilder(int fixedFrameLength)
            : this(new FixedHeaderLengthFrameEncoder(fixedFrameLength), new FixedHeaderLengthFrameDecoder(fixedFrameLength))
        {
        }

        public FixedHeaderLengthFrameBuilder(FixedHeaderLengthFrameEncoder encoder, FixedHeaderLengthFrameDecoder decoder)
            : base(encoder, decoder)
        {
        }
    }

    public sealed class FixedHeaderLengthFrameEncoder : IFrameEncoder
    {
        private readonly int _fixedFrameLength;

        public FixedHeaderLengthFrameEncoder(int fixedFrameLength)
        {
            if (fixedFrameLength <= 0)
                throw new ArgumentOutOfRangeException("fixedFrameLength");
            _fixedFrameLength = fixedFrameLength;
        }

        public int FixedFrameLength { get { return _fixedFrameLength; } }

        public void EncodeFrame(byte[] payload, int offset, int count, out byte[] frameBuffer, out int frameBufferOffset, out int frameBufferLength)
        {
            if (count == FixedFrameLength)
            {
                frameBuffer = payload;
                frameBufferOffset = offset;
                frameBufferLength = count;
            }
            else
            {
                var buffer = new byte[FixedFrameLength];
                if (count >= FixedFrameLength)
                {
                    Array.Copy(payload, offset, buffer, 0, FixedFrameLength);
                }
                else
                {
                    Array.Copy(payload, offset, buffer, 0, count);
                    for (int i = 0; i < FixedFrameLength - count; i++)
                    {
                        buffer[count + i] = (byte)'\n';
                    }
                }

                frameBuffer = buffer;
                frameBufferOffset = 0;
                frameBufferLength = buffer.Length;
            }
        }
    }

    public sealed class FixedHeaderLengthFrameDecoder : IFrameDecoder
    {
        private readonly int _fixedFrameLength;

        public FixedHeaderLengthFrameDecoder(int fixedFrameLength)
        {
            if (fixedFrameLength <= 0)
                throw new ArgumentOutOfRangeException("fixedFrameLength");
            _fixedFrameLength = fixedFrameLength;
        }

        public int FixedFrameLength { get { return _fixedFrameLength; } }

        public bool TryDecodeFrame(byte[] buffer, int offset, int count, out int frameLength, out byte[] payload, out int payloadOffset, out int payloadCount)
        {
            frameLength = 0;
            payload = null;
            payloadOffset = 0;
            payloadCount = 0;

            if (count < FixedFrameLength)
                return false;

            frameLength = FixedFrameLength;
            payload = buffer;
            payloadOffset = offset;
            payloadCount = FixedFrameLength;
            return true;
        }
    }
}
