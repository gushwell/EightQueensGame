﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EightQueensGame
{
    public class Record
    {
        public Record Root;
        public int Index { get; private set; }
        public Piece Piece { get; set; }

        // Childrenには可能性のある相手の手が複数格納される
        public List<Record> Children { get; private set; }

        public Record(int loc)
        {
            Index = loc;
            Children = new List<Record>();
        }

        public Record()
        {
            Children = new List<Record>();
        }

        public void Add(Record rec)
        {
            Children.Add(rec);
            rec.Root = this.Root;
        }

        public void Clear()
        {
            Children.Clear();
        }

        public string result = "";

        // デバッグ用
        public void Print(int indent)
        {
            if (Piece == Piece.Black || Piece == Piece.White)
            {
                string s = string.Format("{0} {1}\n", new string(' ', indent), Index.ToString());
                this.Root.result += s;
            }
            foreach (var x in Children)
                x.Print(indent + 1);
        }
    }
}
