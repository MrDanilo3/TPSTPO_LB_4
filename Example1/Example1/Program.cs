
class Program
{
    static void Main(string[] args)
    {
        int size = 9;
        int[,] matrix = new int[size, size];
        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = random.Next(-10, 100);
            }
        }

        int[][] diagonals = Enumerable.Range(0, size * 2 - 1)
            .Select(i => Enumerable.Range(0, size)
                .Where(j => i - j >= 0 && i - j < size)
                .Select(j => matrix[j, i - j])
                .ToArray())
            .Where(arr => arr.Length > 0 && arr.All(x => x != 0))
            .ToArray();

        int[] sorted = diagonals.SelectMany(diagonal => diagonal)
            .OrderByDescending(x => Math.Abs(x))
            .ToArray();

        int k = sorted.Length > 0 ? Math.Abs(sorted[0]) : 0;
        int sum = ParallelEnumerable.Range(0, sorted.Length)
            .AsParallel()
            .Where(i => Math.Abs(sorted[i]) >= k)
            .Select(i => sorted[i])
            .Sum();

        Console.WriteLine($"Сумма елементiв найбiльшого порядку: {sum}");
    }
}