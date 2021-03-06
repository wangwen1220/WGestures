﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Win32;

namespace WGestures.Core
{
    /// <summary>
    /// 表示一个手势实例
    /// </summary>
    public class Gesture
    {
        private static char[] dirs = { '上', '↗', '右', '↘', '下', '↙', '左', '↖' };

        public GestureButtons GestureButton { get; set; }
        
        public List<GestureDir> Dirs { get; set; }
        public GestureModifier Modifier { get; set; }

        public Gesture(GestureButtons gestureBtn=GestureButtons.RightButton, int defaultCapacity=12)
        {
            GestureButton = gestureBtn;
            Dirs = new List<GestureDir>(defaultCapacity);
            Modifier = GestureModifier.None;
        }

        public void Add(params GestureDir[] newDirs)
        {
            foreach (var d in newDirs)
            {
                Dirs.Add(d);
            }
        }

        public int Count()
        {
            return Dirs.Count;
        }

        internal GestureDir? Last()
        {
            return Dirs.Count == 0 ? (GestureDir?)null : Dirs.Last();
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Count() + 4);

            sb.Append(GestureButton.ToMnemonic());

            foreach (var d in Dirs)
            {
                sb.Append(dirs[(byte)d]);
            }

            sb.Append(Modifier.ToMnemonic());


            return sb.ToString();

        }

        public override int GetHashCode()
        {
            var hash = 19;
            hash += hash*31 + GestureButton.GetHashCode();
            foreach (var d in Dirs)
            {
                hash = hash * 31 + d.GetHashCode();
            }
            hash = hash * 31 + Modifier.GetHashCode();
           

            return hash;
        }

        public override bool Equals(object obj)
        {
           
            var o = obj as Gesture;
            return o != null && 
                o.GestureButton == GestureButton &&
                Dirs.SequenceEqual(o.Dirs) && Modifier == o.Modifier;
        }

        public enum GestureDir
        {
            Up = 0, RightUp, Right, RightDown, Down, LeftDown, Left, LeftUp
        }

    }
}
