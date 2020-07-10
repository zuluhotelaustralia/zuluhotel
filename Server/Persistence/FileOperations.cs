/***************************************************************************
 *                             FileOperations.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/


using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public static class FileOperations
    {
        public const int KB = 1024;
        public const int MB = 1024 * KB;

        private static int bufferSize = 1 * MB;
        private static int concurrency = 1;

        private static bool unbuffered = true;

        public static int BufferSize
        {
            get
            {
                return bufferSize;
            }
            set
            {
                bufferSize = value;
            }
        }

        public static int Concurrency
        {
            get
            {
                return concurrency;
            }
            set
            {
                concurrency = value;
            }
        }

        public static bool Unbuffered
        {
            get
            {
                return unbuffered;
            }
            set
            {
                unbuffered = value;
            }
        }

        public static bool AreSynchronous
        {
            get
            {
                return concurrency < 1;
            }
        }

        public static bool AreAsynchronous
        {
            get
            {
                return concurrency > 0;
            }
        }

        public static FileStream OpenSequentialStream(string path, FileMode mode, FileAccess access, FileShare share)
        {
            FileOptions options = FileOptions.SequentialScan;

            if (concurrency > 0)
            {
                options |= FileOptions.Asynchronous;
            }

            return new FileStream(path, mode, access, share, bufferSize, options);
        }
    }
}
