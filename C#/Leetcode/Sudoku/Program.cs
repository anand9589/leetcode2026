// See https://aka.ms/new-console-template for more information

using System.Text;
//fillBox();
//string fileName = @"C:\Users\anand\Downloads\play\639036710242318728";// fillBox();
string fileName = fillBox();
int[][] board = solve(fileName);



static int[][] solve(string fileName)
{

    int[][] board = readFile(fileName);
    HashSet<int>[][] tempFields = new HashSet<int>[board.Length][];
    for (int i = 0; i < 9; i++)
    {
        tempFields[i] = new HashSet<int>[9];
        for (int j = 0; j < 9; j++)
        {
            tempFields[i][j] = new HashSet<int>();
            if (board[i][j] == 0)
            {
                for (int k = 1; k <= 9; k++)
                {
                    tempFields[i][j].Add(k);
                }
            }
        }
    }

    for (int row = 0; row < board.Length; row++)
    {
        for (int col = 0; col < board[row].Length; col++)
        {
            if (board[row][col] == 0)
            {
                readFromBox(board, row, col, tempFields[row][col]);
                if (tempFields.Count() > 1)
                {
                    readFromRow(board, row, tempFields[row][col]);
                }
                if (tempFields.Count() > 1)
                {
                    readFromCol(board, col, tempFields[row][col]);
                }
                if (tempFields[row][col].Count == 1)
                {
                    updateTheTempFields(board, tempFields, row, col, tempFields[row][col].First());
                }
            }
        }
    }
    bool doItagain = true;

    while (doItagain)
    {
        doItagain = false;

        for (int row = 0; row < board.Length; row++)
        {
            for (int col = 0; col < board[row].Length; col++)
            {
                if (board[row][col] == 0)
                {
                    readFromBox(board, row, col, tempFields[row][col]);
                    if (tempFields.Count() > 1)
                    {
                        readFromRow(board, row, tempFields[row][col]);
                    }
                    if (tempFields.Count() > 1)
                    {
                        readFromCol(board, col, tempFields[row][col]);
                    }
                    if (tempFields[row][col].Count == 1)
                    {
                        updateTheTempFields(board, tempFields, row, col, tempFields[row][col].First());
                    }
                    checkUniques(board, tempFields, row, col);
                }
            }
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i][j] == 0)
                {
                    doItagain = true;
                    break;
                }
            }
            if (doItagain) break;
        }
    }
    for (int row = 0; row < 9; row++)
    {
        for (int col = 0; col < board[row].Length; col++)
        {
            Console.WriteLine($"[{row},{col}] : Available : {string.Join(' ', tempFields[row][col])}   AND Filled : {board[row][col]}");
        }
    }

    return board;
}

static void updateTheTempFields(int[][] board, HashSet<int>[][] tempFields, int row, int col, int valToRemove)
{
    Console.WriteLine($"[{row},{col}] : {valToRemove}");

    board[row][col] = valToRemove;
    Console.ReadKey();
    Console.WriteLine();
    tempFields[row][col].Clear();
    updateTheBoxTempFields(board, tempFields, row, col);
    updateTheRowTempFields(board, tempFields, row, board[row][col]);
    updateTheColTempFields(board, tempFields, col, board[row][col]);
}

static void checkUniques(int[][] board, HashSet<int>[][] tempFields, int row, int col)
{
    checkTheUniqueInBox(board, tempFields, row, col);
    checkTheUniqueInRow(board, tempFields, row);
    checkTheUniqueInCol(board, tempFields, col);
}

static void checkTheUniqueInBox(int[][] board, HashSet<int>[][] tempFields, int row, int col)
{
    int curRow = row > 5 ? 6 : row > 2 ? 3 : 0;
    int curCol = col > 5 ? 6 : col > 2 ? 3 : 0;


    int[] count = new int[9];

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            foreach (var item in tempFields[curRow + i][curCol + j])
            {
                count[item - 1]++;
            }
        }
    }

    for (int i = 0; i < 9; i++)
    {
        if (count[i] == 1)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (tempFields[curRow + j][curCol + k].Contains(i + 1))
                    {
                        updateTheTempFields(board, tempFields, curRow + j, curCol + k, i + 1);
                    }
                }
            }
        }
    }

}

static void checkTheUniqueInCol(int[][] board, HashSet<int>[][] tempFields, int col, bool searchUnique = false)
{
    int[] count = new int[9];

    for (int row = 0; row < 9; row++)
    {
        foreach (var curr in tempFields[row][col])
        {
            count[curr - 1]++;
        }
    }

    for (int i = 0; i < 9; i++)
    {
        if (count[i] == 1)
        {
            for (int row = 0; row < 9; row++)
            {
                if (tempFields[row][col].Contains(i + 1))
                {
                    updateTheTempFields(board, tempFields, row, col, i + 1);
                }
            }
        }
    }
}

static void checkTheUniqueInRow(int[][] board, HashSet<int>[][] tempFields, int row)
{
    int[] count = new int[9];

    for (int col = 0; col < 9; col++)
    {
        foreach (var curr in tempFields[row][col])
        {
            count[curr - 1]++;
        }
    }

    for (int i = 0; i < 9; i++)
    {
        if (count[i] == 1)
        {
            for (int col = 0; col < 9; col++)
            {
                if (tempFields[row][col].Contains(i + 1))
                {
                    updateTheBoxTempFields(board, tempFields, row, col, true);
                }
            }
        }
    }
}

static void updateTheColTempFields(int[][] board, HashSet<int>[][] tempFields, int col, int valToRemove)
{
    for (int row = 0; row < 9; row++)
    {
        tempFields[row][col].Remove(valToRemove);
        if (tempFields[row][col].Count == 1)
        {
            updateTheTempFields(board, tempFields, row, col, tempFields[row][col].First());
        }
    }

}

static void updateTheRowTempFields(int[][] board, HashSet<int>[][] tempFields, int row, int valToRemove)
{
    for (int col = 0; col < 9; col++)
    {
        tempFields[row][col].Remove(valToRemove);
        if (tempFields[row][col].Count == 1)
        {
            updateTheTempFields(board, tempFields, row, col, tempFields[row][col].First());
        }
    }
}

static void updateTheBoxTempFields(int[][] board, HashSet<int>[][] tempFields, int row, int col, bool searchUnique = false)
{
    int valToRemove = board[row][col];
    int curRow = row > 5 ? 6 : row > 2 ? 3 : 0;
    int curCol = col > 5 ? 6 : col > 2 ? 3 : 0;
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            tempFields[curRow + i][curCol + j].Remove(valToRemove);
            if (tempFields.Count() == 1)
            {
                updateTheTempFields(board, tempFields, curRow + i, curCol + j, tempFields[curRow + i][curCol + j].First());
            }
        }
    }
}

static void readFromCol(int[][] board, int col, HashSet<int> tmp)
{
    for (int row = 0; row < 9; row++)
    {
        tmp.Remove(board[row][col]);
    }
}

static void readFromRow(int[][] board, int row, HashSet<int> tmp)
{
    for (int col = 0; col < 9; col++)
    {
        tmp.Remove(board[row][col]);
    }
}

static void readFromBox(int[][] board, int row, int col, HashSet<int> tmp)
{
    int curRow = row > 5 ? 6 : row > 2 ? 3 : 0;
    int curCol = col > 5 ? 6 : col > 2 ? 3 : 0;

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            tmp.Remove(board[curRow + i][curCol + j]);
        }
    }
}

//Console.WriteLine(board);

static int[][] readFile(string file)
{
    string contents = File.ReadAllText(file);
    int[][] board = new int[9][];
    int index = 0;
    for (int i = 0; i < 9; i++)
    {
        board[i] = new int[9];
        for (int j = 0; j < 9; j++)
        {
            while (!char.IsDigit(contents[index]))
            {
                index++;
            }
            board[i][j] = contents[index++] - '0';
        }
    }

    return board;
}

//fillBox();

static string fillBox()
{
    int[][] arr = new int[9][];
    StringBuilder stringBuilder = new StringBuilder();
    for (int i = 0; i < 9; i++)
    {
        arr[i] = new int[9];
        for (int j = 0; j < 9; j++)
        {
            bool tryAgain = true;

            Console.Write($"Enter Value for Cell [{i},{j}]");
            while (tryAgain)
            {

                var k = Console.ReadKey();
                tryAgain = false;
                if (char.IsDigit(k.KeyChar))
                {
                    arr[i][j] = k.KeyChar - '0';
                }
                else
                {
                    Console.WriteLine($"Invalid : Valid Values are digit 1 - 9 or 0 for skip, try again Cell [{i},{j}]");
                    tryAgain = true;
                }
                Console.WriteLine();

            }

            stringBuilder.Append(arr[i][j]);
            stringBuilder.Append(' ');
        }
        stringBuilder.Append(Environment.NewLine);
    }

    Console.WriteLine(stringBuilder.ToString());
    string fileName = DateTime.UtcNow.Ticks.ToString();
    string dir = @"C:\Users\anand\Downloads\play";

    if (!Directory.Exists(dir))
    {
        Directory.CreateDirectory(dir);
    }
    string fullFileName = dir + @"\" + fileName;
    File.WriteAllText(fullFileName, stringBuilder.ToString());
    return fullFileName;
}

