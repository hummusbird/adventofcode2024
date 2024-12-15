﻿using System.Net;

namespace advent_15;

// i love my girlfriend (anya <3)

class Program
{
    static List<List<char>> warehouse;
    static int robot_y;
    static int robot_x;

    static void Main(string[] args)
    {
        IEnumerable<string> input = File.ReadAllText("input.txt").Split("\n\n");
        warehouse = input.First().Split("\n").Select(x => x.ToCharArray().ToList()).ToList();
        List<char> moves = input.Last().Replace("\n", "").ToCharArray().ToList();

        robot_y = warehouse.IndexOf(warehouse.Where(l => l.Contains('@')).First());
        robot_x = warehouse.Where(l => l.Contains('@')).First().IndexOf('@');

        moves.ForEach(m =>
        {
            LeRobot(m, robot_y, robot_x);

            // warehouse.ForEach(line =>
            // {
            //     Console.WriteLine(new string(line.ToArray()));
            // });
            // Console.Write("\n");
        });

        Console.WriteLine("Part 1: " + warehouse.Select((line, y) => line.Select((spot, x) => spot == 'O' ? y * 100 + x : 0).Sum()).Sum());
    }

    static Tuple<int, int> ConvertMove(char move)
    {
        switch (move)
        {
            case '^':
                return new Tuple<int, int>(-1, 0);
            case '>':
                return new Tuple<int, int>(0, 1);
            case 'v':
                return new Tuple<int, int>(1, 0);
            case '<':
                return new Tuple<int, int>(0, -1);
        }

        return null;
    }

    static bool LeRobot(char move, int y, int x)
    {
        Tuple<int, int> diff = ConvertMove(move);
        int next_y = y + diff.Item1;
        int next_x = x + diff.Item2;

        char next = warehouse[next_y][next_x];

        if (next == '#') { return false; }

        if (next == '.')
        {
            warehouse[next_y][next_x] = warehouse[y][x];
            warehouse[y][x] = '.';
            if (warehouse[next_y][next_x] == '@') { robot_y = next_y; robot_x = next_x; }
            return true;
        }

        if (next == 'O')
        {
            if (LeRobot(move, next_y, next_x))
            {
                warehouse[next_y][next_x] = warehouse[y][x];
                warehouse[y][x] = '.';
                if (warehouse[next_y][next_x] == '@') { robot_y = next_y; robot_x = next_x; }
                return true;
            }
            return false;
        }

        return false;
    }
}