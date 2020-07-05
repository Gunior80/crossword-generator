using System;
using System.Collections.Generic;


namespace crossword_generator
{
    class Word
    {
        string word;
        int length;
        public bool vertical;
        public int col, row;
        public Word(string w)
        {
            word = w.ToLower();
            length = word.Length;
            vertical = false;
        }
        public string Vector()
        {
            if (vertical)
            {
                return "По вертикали";
            }
            else
            {
                return "По Горизонтали";
            }

        }
        public string Text
        {
            get
            {
                return word;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }
        }
    }

    class Generator
    {
        bool IntToBool(int digit)
        {
            if (digit != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        char empty;
        List<Word> available_words, genWordsList;
        public List<Word> used_words;
        List<List<char>> grid;
        public List<List<char>> GetGrid
        {
            get
            {
                return grid;
            }
        }

        int cols, rows;
        public Generator(char e = '-', List<string> words = null)
        {
            available_words = new List<Word>();
            genWordsList = new List<Word>();
            used_words = new List<Word>();
            grid = new List<List<char>>();
            empty = e;
            foreach (string word in words)
            {
                available_words.Add(new Word(word));
            }
        }

        private void clear_grid(int CountWords)
        {
            cols = CountWords * 5;
            rows = CountWords * 5;
            grid.Clear();
            for (int i = 0; i < rows; i++)
            {
                grid.Add(new List<char>());
                for (int j = 0; j < cols; j++)
                {
                    grid[i].Add(empty);
                }
            }
        }

        private void sort_words()
        {
            available_words.Sort((x, y) => x.Length.CompareTo(y.Length));
        }

        public void generate_crossword(int CountWords = 0)
        {
            if (CountWords <= 0 | CountWords > available_words.Count) // Проверка на корректность количества слов 
            {
                CountWords = available_words.Count;
            }
            clear_grid(CountWords);
            sort_words();
            var tempList = available_words;
            genWordsList.Clear();
            Random rand = new Random();
            for (int wordNum = 0; wordNum < CountWords; wordNum++)
            {
                var randomEl = tempList[rand.Next(0, tempList.Count - 1)];
                genWordsList.Add(randomEl); // Помещение в список заданного количества случайных слов
                tempList.Remove(randomEl);
            }

            foreach (Word word in genWordsList)
            {
                fill_grid(word);
            }
            format_grid();

        }
        private void fill_grid(Word word)
        {
            var count = 0;
            bool fit = false;

            while (!fit)
            {
                if (used_words.Count == 0)
                {
                    if (word.Length < grid.Count)
                    {
                        Random rand = new Random();
                        if (rand.Next(0, 1) == 1)
                        {
                            set_word(cols / 2, rows / 2 - word.Length / 2, true, word, true);
                        }
                        else
                        {
                            set_word(cols / 2 - word.Length / 2, rows / 2, false, word, true);
                        }
                        fit = true;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    var coordlist = find_coord(word);
                    try
                    {
                        int col = coordlist[count][0];
                        int row = coordlist[count][1];
                        bool vertical = IntToBool(coordlist[count][2]);
                        if (coordlist[count][4] > 0)
                        {
                            fit = true;
                            set_word(col, row, vertical, word, true);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                count++;
            }
        }

        private List<List<int>> find_coord(Word word)
        {
            List<List<int>> coordlist = new List<List<int>> { };
            int c_pos = -1;                            // Позиция символа в слове (изначально -1)
            foreach (char given_char in word.Text)     // Перебираем символы слова
            {
                c_pos++;
                int rowc = 0;
                foreach (List<char> row in grid)       // Перебираем строки Грида
                {
                    rowc++;
                    int colc = 0;
                    foreach (char c in row)            // Перебираем символы в строке Грида
                    {
                        colc++;
                        if (given_char == c)           // Если символ найден
                        {
                            try                        // Проверка вертикального расположения
                            {
                                if (rowc - c_pos > 0)  // Проверяем, не выступает ли слово за пределы грида (по минимуму)
                                    if (((rowc - c_pos) + word.Length) <= rows)  // Проверяем, не выступает ли слово за пределы грида (по максимуму)
                                        coordlist.Add(new List<int> { colc, rowc - c_pos, 1, colc + (rowc - c_pos), 0 });
                            }
                            catch
                            {

                            }
                            try                        // Проверка горизонтально расположения
                            {
                                if (colc - c_pos > 0)  // Проверяем, не выступает ли слово за пределы грида (по минимуму)
                                    if (((colc - c_pos) + word.Length) <= cols)  // Проверяем, не выступает ли слово за пределы грида (по максимуму)
                                        coordlist.Add(new List<int> { colc - c_pos, rowc, 0, rowc + (colc - c_pos), 0 });
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            List<List<int>> new_coordlist = sort_coordlist(coordlist, word);
            return new_coordlist;
        }

        private List<List<int>> sort_coordlist(List<List<int>> coordlist, Word word)
        {
            List<List<int>> new_coordlist = new List<List<int>> { };
            foreach (List<int> coord in coordlist)
            {
                int col = coord[0];
                int row = coord[1];
                bool vertical = IntToBool(coord[2]);
                coord[4] = check_fit(col, row, vertical, word);
                if (coord[4] > 0)
                {
                    new_coordlist.Add(coord);
                }
            }
            Random rand = new Random();

            for (int i = new_coordlist.Count - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                List<int> tmp = new_coordlist[j];
                new_coordlist[j] = new_coordlist[i];
                new_coordlist[i] = tmp;
            }
            return new_coordlist;
        }

        private int check_fit(int col, int row, bool vertical, Word word)
        {
            if (col < 1 | row < 1)
            {
                return 0;
            }

            int count = 1;
            int score = 1;
            foreach (char letter in word.Text)
            {
                char active_cell;
                try
                {
                    active_cell = grid[row - 1][col - 1];
                }
                catch
                {
                    return 0;
                }


                if ((active_cell != empty) & (active_cell != letter))
                {
                    return 0;
                }


                if (active_cell == letter)
                {
                    score++;
                }

                if (vertical)                                   // Проверка возможности вставить слово по вертикали
                {
                    if (active_cell != letter)                  // условие для первой буквы
                    {
                        if (!check_clear(col + 1, row))         // Проверка правой позиции
                        {
                            return 0;
                        }

                        if (!check_clear(col - 1, row))         // Проверка левой позиции
                        {
                            return 0;
                        }
                    }

                    if (count == 1)                             // Проверка верхней позиции относительно первой буквы
                    {
                        if (!check_clear(col, row - 1))
                        {
                            return 0;
                        }
                    }


                    if (count == word.Length)                   // Проверка нижней позиции относительно последней буквы
                    {
                        if (!check_clear(col, row + 1))
                        {
                            return 0;
                        }
                    }
                }
                else                                            // Проверка возможности вставить слово по горизонтали
                {
                    if (active_cell != letter)                  // условие для первой буквы
                    {
                        if (!check_clear(col, row - 1))         // Проверка верхней позиции
                        {
                            return 0;
                        }

                        if (!check_clear(col, row + 1))         // Проверка нижней позиции
                        {
                            return 0;
                        }
                    }

                    if (count == 1)                             // Проверка левой позиции относительно первой буквы
                    {
                        if (!check_clear(col - 1, row))
                        {
                            return 0;
                        }
                    }

                    if (count == word.Length)                   // Проверка правой позиции относительно последней буквы
                    {
                        if (!check_clear(col + 1, row))
                        {
                            return 0;
                        }
                    }
                }

                if (vertical)
                {
                    row++;
                }
                else
                {
                    col++;
                }
                count++;
            }
            return score;
        }


        private void set_word(int col, int row, bool vertical, Word word, bool force)   // Вставка слова в Грид
        {
            if (force)
            {
                word.col = col;
                word.row = row;
                word.vertical = vertical;
                used_words.Add(word);
                foreach (char letter in word.Text)
                {
                    grid[row - 1][col - 1] = letter;
                    if (vertical)
                    {
                        row += 1;
                    }
                    else
                    {
                        col += 1;
                    }
                }
            }
        }

        private bool check_clear(int col, int row)                                       // Проверка незанятости поля
        {
            try
            {
                char cell = grid[row - 1][col - 1];
                if (cell == empty)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private bool check_list(List<char> list)                                         // Функция проверки строки или столбца на возможность удаления
        {
            foreach (char letter in list)
            {
                if (letter != empty)
                {
                    return false;
                }
            }
            return true;
        }

        private void format_grid()                                                       // Функция удаления строк и столбцов
        {
            int CurrentRow = 0;                                                          // Сначала удаляем пустые строки
            while (CurrentRow < grid.Count)
            {
                if (check_list(grid[CurrentRow]))
                {
                    grid.RemoveAt(CurrentRow);
                }
                else
                {
                    CurrentRow++;
                }
            }

            int CurrentCol = 0;                                                          // Потом пустые столбцы
            while (CurrentCol < grid[0].Count)
            {
                List<char> column = new List<char> { };
                for (int row = 0; row < grid.Count; row++)
                {
                    column.Add(grid[row][CurrentCol]);
                }
                if (check_list(column))
                {
                    for (int row = 0; row < grid.Count; row++)
                    {
                        grid[row].RemoveAt(CurrentCol);
                    }

                }
                else
                {
                    CurrentCol += 1;
                }
            }
        }
    }
}
