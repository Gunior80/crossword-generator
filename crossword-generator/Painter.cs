using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace crossword_generator
{
    class Painter
    {
        const int SQUARE = 50;

        private static void DrawText(Graphics pic, string text, bool num, int x, int y)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            pic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            int x1, y1, width, heigth, fsize;
            if (num)
            {
                x1 = (x * SQUARE) - 2;
                y1 = (y * SQUARE) + 3;
                width = SQUARE;
                heigth = SQUARE / 3;
                fsize = SQUARE / 3;
            }
            else
            {
                sf.Alignment = StringAlignment.Center;
                x1 = (x * SQUARE);
                y1 = (y * SQUARE);
                width = SQUARE;
                heigth = SQUARE;
                fsize = SQUARE / 2;
            }
            pic.DrawString(text,
                               new System.Drawing.Font("Times New Roman", fsize),
                               new SolidBrush(Color.Black), new RectangleF(x1, y1, width, heigth), sf);

        }
        public static void DrawImage(List<List<char>> list, bool filled, PictureBox el)
        {
            int w_l = list.Count;
            int h_l = list[0].Count;

            int width = w_l * SQUARE + 2;
            int height = h_l * SQUARE + 2;
            int num = 0;
            el.Image = (Image)new Bitmap(width, height);    // создаем битмап заданных размеров
            Graphics pic = Graphics.FromImage(el.Image);
            Pen p = new Pen(Color.Black, 1);                // цвет линии и толщина
            pic.Clear(Color.White);


            for (int y1 = 0; y1 < h_l; y1++)
            {
                for (int x1 = 0; x1 < w_l; x1++)
                {
                    if (list[x1][y1] != '-')
                    {
                        pic.DrawLine(p, new Point((x1 * SQUARE), (y1 * SQUARE)), new Point((x1 + 1) * SQUARE, (y1 * SQUARE)));         // 
                        pic.DrawLine(p, new Point((x1 * SQUARE), (y1 + 1) * SQUARE), new Point((x1 + 1) * SQUARE, (y1 + 1) * SQUARE)); // рисуем линии
                        pic.DrawLine(p, new Point((x1 * SQUARE), (y1 * SQUARE)), new Point((x1 * SQUARE), (y1 + 1) * SQUARE));         // 
                        pic.DrawLine(p, new Point((x1 + 1) * SQUARE, (y1 * SQUARE)), new Point((x1 + 1) * SQUARE, (y1 + 1) * SQUARE)); // 
                        

                        if ((x1 == 0) & (y1 == 0))  // Проверка нулевых координат
                        {
                            if ((list[x1][y1 + 1] != '-') | (list[x1 + 1][y1] != '-'))
                            {
                                num++;
                                DrawText(pic, num.ToString(), true, x1, y1);
                            }
                        }
                        else
                        {
                            if ((x1 == 0) | (y1 == 0)) // Проверка хотя-бы одной нулевой координаты
                            {
                                if (x1 == 0)
                                {
                                    if (list[x1 + 1][y1] != '-')
                                    {
                                        num++;
                                        DrawText(pic, num.ToString(), true, x1, y1);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if ((list[x1][y1 + 1] != '-') & (list[x1][y1 - 1] == '-'))
                                            {
                                                num++;
                                                DrawText(pic, num.ToString(), true, x1, y1);
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                if (y1 == 0)
                                {
                                    if (list[x1][y1 + 1] != '-')
                                    {
                                        num++;
                                        DrawText(pic, num.ToString(), true, x1, y1);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if ((list[x1 + 1][y1] != '-') & (list[x1 - 1][y1] == '-'))
                                            {
                                                num++;
                                                DrawText(pic, num.ToString(), true, x1, y1);
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            if ((x1 != 0) & (y1 != 0))    // Проверка остальных позиций
                            {

                                if ((list[x1 - 1][y1] == '-') & (list[x1][y1 - 1] == '-'))
                                {
                                    num++;
                                    DrawText(pic, num.ToString(), true, x1, y1);
                                }
                                else
                                {
                                    try
                                    {
                                        if ((list[x1 - 1][y1] == '-') & (list[x1 + 1][y1] != '-'))
                                        {
                                            num++;
                                            DrawText(pic, num.ToString(), true, x1, y1);
                                        }
                                    }
                                    catch { }
                                    finally
                                    {
                                        try
                                        {
                                            if ((list[x1][y1 - 1] == '-') & (list[x1][y1 + 1] != '-'))
                                            {
                                                num++;
                                                DrawText(pic, num.ToString(), true, x1, y1);
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }

                        if (filled)
                        {
                            DrawText(pic, list[x1][y1].ToString(), false, x1, y1);
                        }
                    }
                }
            }

            pic.Dispose();

        }
    }
}
