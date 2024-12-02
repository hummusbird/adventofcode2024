using System.Threading.Tasks;

namespace advent_2;

class Program
{
    static void Main(string[] args)
    {
        List<List<int>> reports = File.ReadAllLines("input.txt").Select(x => x.Split(" ").Select(x => int.Parse(x)).ToList()).ToList();
        int p1SafeReports = 0;
        int p2SafeReports = 0;

        Parallel.ForEach(reports, report =>
        {
            bool isAscending = false;
            bool safe = true;

            for (int i = 1; i < report.Count; i++)
            {
                isAscending = report[0] < report[1];

                if (report[i - 1] < report[i] != isAscending) { safe = false; break; }
                if (Math.Abs(report[i - 1] - report[i]) > 3 || Math.Abs(report[i - 1] - report[i]) < 1) { safe = false; break; }
            }

            if (safe) { p1SafeReports++; p2SafeReports++; }

            else // if the report fails, process report with The Problem Dampener
            {
                for (int j = 0; j < report.Count; j++)
                {
                    List<int> dampReport = report.ToList();
                    dampReport.RemoveAt(j);

                    bool p2isAscending = false;
                    bool p2safe = true;

                    for (int i = 1; i < dampReport.Count; i++)
                    {
                        p2isAscending = dampReport[0] < dampReport[1];

                        if (dampReport[i - 1] < dampReport[i] != p2isAscending) { p2safe = false; break; }
                        if (Math.Abs(dampReport[i - 1] - dampReport[i]) > 3 || Math.Abs(dampReport[i - 1] - dampReport[i]) < 1) { p2safe = false; break; }
                    }

                    if (p2safe) { p2SafeReports++; break; }
                }
            }
        });

        Console.WriteLine("Part 1: " + p1SafeReports);
        Console.WriteLine("Part 2: " + p2SafeReports);
    }
}
