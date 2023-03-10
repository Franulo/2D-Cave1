using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Utils
{
    public class  Grid<T>
    {
        public T[] Cells { get; } //how does that work?
        public int Width { get; }
        public int Height { get; }

        public delegate T InitFunction(int x, int y); //???

        public Grid(int width, int height)
        {
            Cells = new T[width * height];
            Width = width;
            Height = height;
        }

        public Grid(int width, int height, InitFunction init) : this(width, height) //Whats that? Why cant I do it up there
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    Set(x, y, init(x, y));
                }
            }
        }

        public int CoordsToIndex(int x, int y)
        {
            return y * Width + x;
        }

        public int CoordsToIndex(Vector2Int coords)
        {
            return CoordsToIndex(coords.x, coords.y);
        }

        public Vector2Int IndexToCoords(int index)
        {
            return new Vector2Int(index % Width, index / Width);
        }

        public void Set(int x, int y, T value)
        {
            Cells[CoordsToIndex(x, y)] = value; //why can I accsses Cells?

        }

        public void Set(Vector2Int coords, T value)
        {
            Cells[CoordsToIndex(coords.x, coords.y)] = value;
        }

        public void Set(int index, T value)
        {
            Cells[index] = value;
        }

        public T Get(int x, int y)
        {
            return Cells[CoordsToIndex(x, y)];

        }

        public T Get(Vector2Int coords)
        {
            return Cells[CoordsToIndex(coords.x, coords.y)];
        }

        public T Get(int index)
        {
            return Cells[index];
        }

        public bool AreCoordsValid(int x, int y, int safeWalls)
        {
            if (x >= 0 + safeWalls && x < Width - safeWalls && y >= 0 + safeWalls && y < Height - safeWalls)
            {
                return true;
            }
            else
            {
                return false;
            }
                //return safeWalls ? //what is ? and :
                //(x > 0 && x < Width - 1 && y > 0 && y < Height - 1) :
                //(x >= 0 && x < Width && y >= 0 && y < Height);
        }

        public bool AreCoordsValid(Vector2Int coords, int safeWalls)
        {
            return AreCoordsValid(coords.x, coords.y, safeWalls);
        }

        public Vector2Int GetCoords(T value)
        {
            var i = Array.IndexOf(Cells, value);
            if (i == -1)
            {
                throw new ArgumentException(); //how does that work
            }

            return IndexToCoords(i);
        }

        public List<T> GetNeighbours(Vector2Int coords, int safeWalls)
        {
            var directions = (Direction[])Enum.GetValues(typeof(Direction)); //thats complicated
            var neighbours = new List<T>();  //how does "var" work?
            foreach (var direction in directions)
            {
                var neighbourCoords = coords + direction.ToCoords();
                if (AreCoordsValid(neighbourCoords, safeWalls))
                {
                    neighbours.Add(Get(coords));
                }
            }

            return neighbours;
        }
    }
}