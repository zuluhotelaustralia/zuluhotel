/***************************************************************************
 *                              SaveMetrics.cs
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Server
{
    public sealed class SaveMetrics : IDisposable
    {

        public SaveMetrics()
        {
        }

        public void OnItemSaved(int numberOfBytes)
        {
        }

        public void OnMobileSaved(int numberOfBytes)
        {
        }

        public void OnGuildSaved(int numberOfBytes)
        {
        }

        public void OnFileWritten(int numberOfBytes)
        {
        }

        private bool isDisposed;

        public void Dispose()
        {
        }
    }
}
