﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGL.Objects.Texts.FreeTypes
{
    [StructLayout(LayoutKind.Sequential)]
    public class BBox
    {
        public int xMin, yMin;
        public int xMax, yMax;
    }
}
