using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CardsProbability
{
  public partial class WorkSheet : Form
  {
    public string expName  // имя события
    {
      get;
      set;
    }

    public Card[] cards = new Card[98];  // все - 98
    byte cardsCount = 0;
    Card SelectedCard = null;
    public List<Card> selectedCards = new List<Card>();  // список с выбранными картами
    int selectedPageIndex = 0;    // индекс выбранной страницы, по которой происходит перерисовка

    public Bitmap bmp_one  // изображение из allCardsPB
    {
      get;
      set;
    }

    public Bitmap bmp_second  // изображение из coloredCardsPB
    {
      get;
      set;
    }

    public Bitmap bmp_third  // изображение из otherCardsPB
    {
      get;
      set;
    }

    public Bitmap selected_bmp  // изображение выбранных карт
    {
      get;
      set;
    }

    public WorkSheet()
    {
      InitializeComponent();
    }

    private void WorkSheet_Shown(object sender, EventArgs e)
    {
      if (bmp_one != null)
      {
        allCardsPB.Image = bmp_one;
        coloredCardsPB.Image = bmp_second;
        otherCardsPB.Image = bmp_third;
        pictureBox2.Image = selected_bmp;
        return;
      }

      // инициализация всех bmp
      bmp_one = new Bitmap(allCardsPB.Width, allCardsPB.Height);
      bmp_second = new Bitmap(coloredCardsPB.Width, coloredCardsPB.Height);
      bmp_third = new Bitmap(otherCardsPB.Width, otherCardsPB.Height);
      selected_bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

      // прорисовка первых карт
      Graphics g = Graphics.FromImage(bmp_one);

      for (int i = 1; i <= 4; i++)
        for (int TypeEnum = 2; TypeEnum < 15; TypeEnum++)
        {
          Card CurrentCard = new Card((Suits)i, (Types)TypeEnum, Colors.None, TypeEnum * 65 + 2, 65 * i - 20);
          cards[cardsCount] = CurrentCard;
          g.DrawImage(CurrentCard.Picture, CurrentCard.X, CurrentCard.Y, CurrentCard.Width - 2, CurrentCard.Height);
          cardsCount++;
        }

      allCardsPB.Image = bmp_one;

      // прорисовка цветных карт - вторая вкладка

      g = Graphics.FromImage(bmp_second);

      for (int i = 0; i <= 2; i++)
        for (int TypeEnum = 2; TypeEnum < 15; TypeEnum++)
        {
          Card CurrentCard = new Card(Suits.None, (Types)TypeEnum, (Colors)i, TypeEnum * 65 + 2, 65 * i + 85);

          if (i == 0)                        // максимальное количество карт
            CurrentCard.maxCount = 4;
          else
            CurrentCard.maxCount = 2;

          cards[cardsCount] = CurrentCard;
          g.DrawImage(CurrentCard.Picture, CurrentCard.X, CurrentCard.Y, CurrentCard.Width - 2, CurrentCard.Height);
          cardsCount++;
        }

      coloredCardsPB.Image = bmp_second;

      // прорисовка всех остальных карт

      g = Graphics.FromImage(bmp_third);

      for (int i = 0; i < 3; i++)  // уменьшить!
      {
        Card CurrentCard = new Card(Suits.None, Types.None, (Colors)i, i * 65 + 350 + 2, 65 * 2 - 10);

        if (i == 0)
          CurrentCard.maxCount = 52; // максимальное???
        else
          CurrentCard.maxCount = 52 / 2;  // max/2

        cards[cardsCount] = CurrentCard;
        g.DrawImage(CurrentCard.Picture, CurrentCard.X, CurrentCard.Y, CurrentCard.Width - 5, CurrentCard.Height);
        cardsCount++;
      }

      for (int i = 1; i <= 4; i++)
      {
        Card CurrentCard = new Card((Suits)i, Types.None, Colors.None, i * 65 + 350 + 130 + 2, 65 * 2 - 10);

        CurrentCard.maxCount = 52 / 4;  // max/4

        cards[cardsCount] = CurrentCard;
        g.DrawImage(CurrentCard.Picture, CurrentCard.X, CurrentCard.Y, CurrentCard.Width - 5, CurrentCard.Height);
        cardsCount++;
      }

      otherCardsPB.Image = bmp_third;
    }


    /// <summary>
    /// получение номера карты среди выбранных по координатам
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int getSelectedCardNumber(int x, int y)
    {
      Card tempCard = null;  //  что-то присвоить требует

      for (int i = 0; i < selectedCards.Count; i++)   // ищем карту по списку и координатам
      {
        if (i + 15 < selectedCards.Count)
        {
          if (x > selectedCards[i].X &&
              y > selectedCards[i].Y &&
              x < selectedCards[i].X + selectedCards[i].Width &&
              y < selectedCards[i].Y + 60)
          {
            tempCard = selectedCards[i];
            break;
          }
        }
        else
        {
          if (x > selectedCards[i].X &&
              y > selectedCards[i].Y &&
              x < selectedCards[i].X + selectedCards[i].Width &&
              y < selectedCards[i].Y + selectedCards[i].Height)
          {
            tempCard = selectedCards[i];
            break;
          }
        }
      }

      if (tempCard == null)
        return -1;

      for (int i = 0; i < 98; i++)       // поиск карт по массиву
      {
        if (cards[i].Type == tempCard.Type &&
            cards[i].Suit == tempCard.Suit &&
            cards[i].Color == tempCard.Color)
        {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// получение номера карты по координатам
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="numberOfRows">количество строк с картами</param>
    /// <returns></returns>
    private int getCardNumber(int x, int y)
    {
      if (selectedPageIndex == 0)           // если поиск производится при выбранном первом окне
      {
        int numberOfRows = 4;
        for (int i = 0; i < numberOfRows; i++)
        {
          for (int j = 0; j < 13; j++)
          {
            if (i != numberOfRows - 1 &&
                x > 132 + j * 65 &&
                x < 132 + (j + 1) * 65 &&
                y > 45 + i * 65 &&
                y < 45 + (i + 1) * 65)
            {
              if (!cards[i * 13 + j].choosen)
                return i * 13 + j;
              else
                return -1;
            }

            if (i == numberOfRows - 1 &&          // для последней строки
            x > 132 + j * 65 &&
            x < 132 + (j + 1) * 65 &&
            y > 45 + i * 65 &&
            y < 45 + (i) * 65 + cards[0].Height)
            {
              if (!cards[i * 13 + j].choosen)
                return i * 13 + j;
              else
                return -1;
            }
          }
        }
        return -1;
      }

      if (selectedPageIndex == 1)            //  если выбрано второе окно
      {
        int numberOfRows = 3;
        for (int i = 0; i < numberOfRows; i++)
        {
          for (int j = 0; j < 13; j++)
          {
            if (i != numberOfRows - 1 &&
                x > 132 + j * 65 &&
                x < 132 + (j + 1) * 65 &&
                y > 85 + i * 65 &&
                y < 85 + (i + 1) * 65)
            {
              if (!cards[i * 13 + j + 52].choosen)
                return i * 13 + j + 52;
              else
                return -1;
            }

            if (i == numberOfRows - 1 &&          // для последней строки
            x > 132 + j * 65 &&
            x < 132 + (j + 1) * 65 &&
            y > 85 + i * 65 &&
            y < 85 + (i) * 65 + cards[0].Height)
            {
              if (!cards[i * 13 + j + 52].choosen)
                return i * 13 + j + 52;
              else
                return -1;
            }
          }
        }
        return -1;
      }

      if (selectedPageIndex == 2)             // если выбрано третье окно
      {
        for (int i = 0; i < 7; i++)
        {
          if (x > 352 + i * 65 &&
              x < 352 + (i + 1) * 65 &&
              y > 120 &&
              y < 120 + cards[0].Height)
          {
            if (!cards[91 + i].choosen)
              return 91 + i;
            else
              return -1;
          }
        }
        return -1;
      }

      return -1;
    }

    /// <summary>
    /// получение номера столбца по номеру карты
    /// </summary>
    /// <param name="cardNum">номер карты</param>
    /// <returns></returns>
    private int getColumnNum(int cardNum)
    {
      while (true)
      {
        if (cardNum > 12)
          cardNum -= 13;
        else
          return cardNum;
      }
    }

    /// <summary>
    /// Получение номера строки по координатам. Прим. - пока без ограничений, так как они не слишком то и нужны
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int getLineNumber(int x, int y)
    {
      switch (selectedPageIndex)
      {
        case 0:
          {
            if (y < 45 || y > 345)
              return -1;
            int num = -1;  // номер строки
            int y1 = 45;  // вспом. - верхний предел перерисовки карт
            while (true)
            {
              if (y > y1)
              {
                y1 += 65;
                num++;
              }

              else return num;
            }
          }
        case 1:
          {
            if (y < 85 || y > 320)
              return -1;
            int num = -1;  // номер строки
            int y1 = 45;  // вспом. - верхний предел перерисовки карт
            while (true)
            {
              if (y > y1)
              {
                y1 += 65;
                num++;
              }

              else return num;
            }
          }
        case 2:
          {
            return 0;
          }

      }
      return -1;

    }

    /// <summary>
    /// Перерисовка столбца по номеру карты
    /// </summary>
    /// <param name="num">номер карты</param>
    /// <param name="lines"></param>
    private void paintRow(int num)
    {
      if (num == -1)
        return;
      Graphics g = Graphics.FromImage(bmp_one);
      int lines = 0;

      switch (selectedPageIndex)
      {
        case 0: { g = Graphics.FromImage(bmp_one); lines = 4; break; }
        case 1: { g = Graphics.FromImage(bmp_second); lines = 3; break; }
        case 2: { g = Graphics.FromImage(bmp_third); lines = 1; break; }
      }

      int currLine = getLineNumber(cards[num].X + 1, cards[num].Y + 1);
      while (currLine < lines)
      {
        if (!cards[num].choosen)
          g.DrawImage(cards[num].Picture, cards[num].X, cards[num].Y, cards[num].Width - 2, cards[num].Height);
        num += 13;
        currLine++;
      }

      switch (selectedPageIndex)
      {
        case 0: { allCardsPB.Image = bmp_one; break; }
        case 1: { coloredCardsPB.Image = bmp_second; break; }
        case 2: { otherCardsPB.Image = bmp_third; break; }
      }
    }

    /// <summary>
    /// Получение позиции для карты в выбранных картах
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void getPositionInSelectedCards(out int x, out int y)
    {
      x = 10;
      y = 20;                      // две точки, из которых будем рисовать карты

      for (int i = 0; i < selectedCards.Count; i++)  // поиск расположения карты на groupbox1
      {
        if (x < 1050)
          x += 75;
        else
        {
          x = 10;
          y += 60;
        }
      }
    }

    /// <summary>
    /// Перерисовка всех выбранных карт
    /// </summary>
    private void repaintSelectedCards()
    {
      Bitmap temp_bmp = selected_bmp;

      Graphics g = Graphics.FromImage(temp_bmp);

      g.Clear(Color.Transparent);

      int x = 10, y = 20;

      for (int i = 0; i < selectedCards.Count; i++)
      {
        g.DrawImage(selectedCards[i].Picture, x, y, selectedCards[i].Width, selectedCards[i].Height);

        selectedCards[i].X = x;
        selectedCards[i].Y = y;

        if (x < 1050)
          x += 75;
        else
        {
          x = 10;
          y += 60;
        }
      }

      pictureBox2.Image = temp_bmp;
      selected_bmp = temp_bmp;
    }

    /// <summary>
    /// Перерисовка выбранной карты и всех под ней
    /// </summary>
    /// <param name="num"></param>
    private void repaintSelectedCards(int num)
    {
      if (num == -1)
        return;

      Bitmap temp_bmp = selected_bmp;

      Graphics g = Graphics.FromImage(temp_bmp);

      while (num < selectedCards.Count)
      {
        g.DrawImage(selectedCards[num].Picture, selectedCards[num].X, selectedCards[num].Y, selectedCards[num].Width, selectedCards[num].Height);
        num += 15;
      }
      pictureBox2.Image = temp_bmp;
      selected_bmp = temp_bmp;
    }


    // нажатие по панели со всеми картами
    private void tabPage1_MouseClick(object sender, MouseEventArgs e)
    {
      Bitmap bmp1 = bmp_one;

      Graphics g = Graphics.FromImage(bmp1);

      if (SelectedCard != null)  // если до этого была выбрана какая-то карта, нужно её зарисовать
      {
        if (SelectedCard.choosen)  // если выделенная карта - среди выбранных
        {
          int selectedCardNumber = selectedCards.IndexOf(SelectedCard);

          repaintSelectedCards(selectedCardNumber);

          SelectedCard = null;
        }
        else
        {
          if (e.X >= SelectedCard.X &&                         // если выбираем уже выбранную карту
              e.Y >= SelectedCard.Y &&
              e.X <= SelectedCard.X + SelectedCard.Width &&
              e.Y <= SelectedCard.Y + SelectedCard.Height)
            return;

          // перерисовка карт 
          int num = getCardNumber(SelectedCard.X + 1, SelectedCard.Y + 1);
          paintRow(num);
        }
      }

      // при нажатии на карту происходит её выделение

      if (getCardNumber(e.X, e.Y) != -1)
      {
        SelectedCard = cards[getCardNumber(e.X, e.Y)];
        g.DrawImage(SelectedCard.Picture, SelectedCard.X, SelectedCard.Y, SelectedCard.Width - 2, SelectedCard.Height);
      }
      else
      {
        SelectedCard = null;
      }

      allCardsPB.Image = bmp1;
      bmp_one = bmp1;
    }

    private void tabPage1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      //Graphics g = panel1.CreateGraphics();


      if (selected_bmp == null)
        selected_bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);

      Bitmap temp_bmp = selected_bmp;

      Graphics g = Graphics.FromImage(temp_bmp);

      if (SelectedCard != null)  // вся прорисовка (=
      {
        Text += SelectedCard.Name;
        if (e.X >= SelectedCard.X &&         // двойной клик по уже выбранной карте                         
            e.Y >= SelectedCard.Y &&
            e.X <= SelectedCard.X + SelectedCard.Width &&
            e.Y <= SelectedCard.Y + SelectedCard.Height)
        {
          int num = getCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5);

          if (num == -1)
            return;

          cards[num].nil = true;
          paintRow(num);
          cards[num].nil = false;

          int x = 10, y = 20;
          getPositionInSelectedCards(out x, out y);

          selectedCards.Add(cards[num]);

          cards[num].X = x;
          cards[num].Y = y;
          cards[num].choosen = true;
          g.DrawImage(cards[num].Picture, x, y, cards[num].Width, cards[num].Height);

          SelectedCard = null;
        }
      }

      pictureBox2.Image = temp_bmp;
      selected_bmp = temp_bmp;
    }

    private void panel1_MouseClick(object sender, MouseEventArgs e)
    {
      if (selectedCards.Count == 0)
        return;

      Bitmap temp_bmp = selected_bmp;

      Graphics g = Graphics.FromImage(temp_bmp);

      if (SelectedCard != null)
      {
        if (e.X > SelectedCard.X &&                            // если нажатие по той же карте, что и выбрана
            e.Y > SelectedCard.Y &&
            e.X < SelectedCard.X + SelectedCard.Width &&
            e.Y < SelectedCard.Y + SelectedCard.Height)
        {
          return;
        }

        if (!SelectedCard.choosen)  // если была выделена карта среди общей колоды
          paintRow(getCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5));
        else
        {
          // перерисовка области с картой, которая была выделена
          int selectedCardNumber = selectedCards.IndexOf(SelectedCard);

          repaintSelectedCards(selectedCardNumber);

          SelectedCard = null;
        }
      }

      int num = getSelectedCardNumber(e.X, e.Y);
      if (num == -1)
        return;

      //g.DrawImage(cards[num].Picture, cards[num].X, cards[num].Y, cards[num].Width, cards[num].Height);

      SelectedCard = cards[num];

      int x = 10, y = 20;

      for (int i = 0; i < selectedCards.Count; i++)  // поиск расположения карты на groupbox1
      {
        if (selectedCards[i].Suit == SelectedCard.Suit &&
            selectedCards[i].Color == SelectedCard.Color &&
            selectedCards[i].Type == SelectedCard.Type &&
            e.X > selectedCards[i].X &&
            e.Y > selectedCards[i].Y &&
            e.X < selectedCards[i].X + selectedCards[i].Width &&
            e.Y < selectedCards[i].Y + selectedCards[i].Height)
          break;
        if (x < 1050)
          x += 75;
        else
        {
          x = 10;
          y += 60;
        }

      }

      SelectedCard.X = x;
      SelectedCard.Y = y;
      SelectedCard.choosen = true;

      g.DrawImage(cards[num].Picture, x, y, cards[num].Width, cards[num].Height);


      /*
            

      // Получение числа одинаковых карт
      int cardsInSelected = 0;
      foreach (Card card in selectedCards)
      {
          if (card.Suit == SelectedCard.Suit &&
              card.Type == SelectedCard.Type &&
              card.Color == SelectedCard.Color)
              cardsInSelected++;
      }

      // Получение расположения карты, по которой кликнули
      int x = 10, y = 20;

      for (int i = 0; i < selectedCards.Count; i++)
      {
          if (x < 1050)
              x += 75;
          else
          {
              x = 10;
              y += 60;
          }
          if (cards[i].Type == SelectedCard.Type &&
              cards[i].Color == SelectedCard.Color &&
              cards[i].Suit == SelectedCard.Suit)
              break;
      }

      SelectedCard.X = x;
      SelectedCard.Y = y;
      SelectedCard.choosen = true; */

      //g.DrawImage(SelectedCard.Picture, SelectedCard.X, SelectedCard.Y, SelectedCard.Width, SelectedCard.Height);

      /*
      if (cardsInSelected > 1)
      {
          SelectedCard = new Card(cards[num].Suit, cards[num].Type, cards[num].Color);

          SelectedCard.choosen = true;
      }


      int x = 10, y = 20;

      while (x < e.X && y < e.Y)  // поиск расположения карты на groupbox1
      {
          if (x < 1050)
              x += 75;
          else
          {
              x = 10;
              y += 60;
          }
      }

      SelectedCard.X = x;
      SelectedCard.Y = y;

      g.DrawImage(SelectedCard.Picture, SelectedCard.X, SelectedCard.Y, SelectedCard.Width, SelectedCard.Height); */

      pictureBox2.Image = temp_bmp;
      selected_bmp = temp_bmp;
    }

    private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (SelectedCard != null && SelectedCard.choosen)
      {
        if (e.X > SelectedCard.X &&
            e.Y > SelectedCard.Y &&
            e.X < SelectedCard.X + SelectedCard.Width &&
            e.Y < SelectedCard.Y + SelectedCard.Height)
        {
          int num = getSelectedCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5);

          if (num == -1)
            return;

          if (num < 52)
          {
            selectedPageIndex = 0;
            tabControl1.SelectedIndex = 0;
          }
          if (num >= 52 && num < 91)
          {
            selectedPageIndex = 1;
            tabControl1.SelectedIndex = 1;
          }
          if (num >= 91)
          {
            selectedPageIndex = 2;
            tabControl1.SelectedIndex = 2;
          }

          int cardsInSelected = 0;
          foreach (Card card in selectedCards)
          {
            if (card.Suit == SelectedCard.Suit &&
                card.Type == SelectedCard.Type &&
                card.Color == SelectedCard.Color)
              cardsInSelected++;
          }

          // Если остаётся только 1 карта
          if (cardsInSelected == cards[num].maxCount)
          {

            switch (selectedPageIndex)
            {
              case 0:
                {
                  cards[num].X = 132;
                  cards[num].Y = 45;
                  // определение координат карты
                  for (int i = 0; i < num; i++)
                  {
                    cards[num].X += 65;
                    if (i == 12 || i == 25 || i == 38)
                    {
                      cards[num].X = 132;
                      cards[num].Y += 65;
                    }
                  }
                  break;
                }
              case 1:
                {
                  cards[num].X = 132;
                  cards[num].Y = 85;
                  for (int i = 52; i < num; i++)
                  {
                    cards[num].X += 65;
                    if (i == 64 || i == 77)
                    {
                      cards[num].X = 132;
                      cards[num].Y += 65;
                    }
                  }
                  break;
                }
              case 2:
                {
                  cards[num].X = 352;
                  cards[num].Y = 120;
                  for (int i = 91; i < num; i++)
                  {
                    cards[num].X += 65;
                  }
                  break;
                }
            }


            cards[num].choosen = false;

            paintRow(num);                     // возвращаем карту на место

            selectedCards.Remove(cards[num]);  // удаляем из выходного списка карту

            repaintSelectedCards();            // перерисовываем все выбранные карты 

          }
          // В том случае, если сверху не все возможные карты
          else
          {
            int x = 10, y = 20;

            int i = 0;

            for (i = 0; i < selectedCards.Count; i++)  // поиск расположения карты на groupbox1
            {
              if (selectedCards[i].Suit == SelectedCard.Suit &&
                  selectedCards[i].Color == SelectedCard.Color &&
                  selectedCards[i].Type == SelectedCard.Type &&
                  e.X > selectedCards[i].X &&
                  e.Y > selectedCards[i].Y &&
                  e.X < selectedCards[i].X + selectedCards[i].Width &&
                  e.Y < selectedCards[i].Y + selectedCards[i].Height)
                break;
              if (x < 1050)
                x += 75;
              else
              {
                x = 10;
                y += 60;
              }

            }

            cards[num].choosen = false;

            switch (selectedPageIndex)
            {
              case 0:
                {
                  cards[num].X = 132;
                  cards[num].Y = 45;
                  // определение координат карты
                  for (int j = 0; j < num; j++)
                  {
                    cards[num].X += 65;
                    if (j == 12 || j == 25 || j == 38)
                    {
                      cards[num].X = 132;
                      cards[num].Y += 65;
                    }
                  }
                  break;
                }
              case 1:
                {
                  cards[num].X = 132;
                  cards[num].Y = 85;
                  for (int j = 52; j < num; j++)
                  {
                    cards[num].X += 65;
                    if (j == 64 || j == 77)
                    {
                      cards[num].X = 132;
                      cards[num].Y += 65;
                    }
                  }
                  break;
                }
              case 2:
                {
                  cards[num].X = 352;
                  cards[num].Y = 120;
                  for (int j = 91; j < num; j++)
                  {
                    cards[num].X += 65;
                  }
                  break;
                }
            }


            selectedCards.Remove(selectedCards[i]);  // удаляем из выходного списка карту

            //cards[num].choosen = false;

            repaintSelectedCards();            // перерисовываем все выбранные карты 
          }
        }
        else
          return;
      }
    }

    private void coloredCardsPB_MouseClick(object sender, MouseEventArgs e)
    {
      Graphics g = Graphics.FromImage(bmp_second);

      if (SelectedCard != null)  // если до этого была выбрана какая-то карта, нужно её зарисовать
      {
        if (SelectedCard.choosen)  // если выделенная карта - среди выбранных
        {
          int selectedCardNumber = selectedCards.IndexOf(SelectedCard);

          repaintSelectedCards(selectedCardNumber);

          SelectedCard = null;
        }
        else
        {
          if (e.X >= SelectedCard.X &&                         // если выбираем уже выбранную карту
              e.Y >= SelectedCard.Y &&
              e.X <= SelectedCard.X + SelectedCard.Width &&
              e.Y <= SelectedCard.Y + SelectedCard.Height)
            return;

          // перерисовка карт 
          int num = getCardNumber(SelectedCard.X + 1, SelectedCard.Y + 1);
          paintRow(num);
        }
      }

      // при нажатии на карту происходит её выделение

      if (getCardNumber(e.X, e.Y) != -1)
      {
        SelectedCard = cards[getCardNumber(e.X, e.Y)];
        g.DrawImage(SelectedCard.Picture, SelectedCard.X, SelectedCard.Y, SelectedCard.Width - 2, SelectedCard.Height);
      }
      else
      {
        SelectedCard = null;
      }

      coloredCardsPB.Image = bmp_second;
    }


    private void coloredCardsPB_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (selectedPageIndex == 1 && SelectedCard != null)
      {
        if (e.X >= SelectedCard.X &&         // двойной клик по уже выбранной карте                         
            e.Y >= SelectedCard.Y &&
            e.X <= SelectedCard.X + SelectedCard.Width &&
            e.Y <= SelectedCard.Y + SelectedCard.Height)
        {
          Graphics g = Graphics.FromImage(selected_bmp);

          int num = getCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5);

          if (num == -1)
            return;

          /* cards[num].nil = true;
           paintRow(num);
           cards[num].nil = false;

           int x = 10, y = 20;
           getPositionInSelectedCards(out x, out y);

           selectedCards.Add(cards[num]);

           cards[num].X = x;
           cards[num].Y = y;
           cards[num].choosen = true;
           g.DrawImage(cards[num].Picture, x, y, cards[num].Width, cards[num].Height);

           SelectedCard = null; */


          // Работа с добавлением нескольких карт

          Card temp = new Card(cards[num].Suit, cards[num].Type, cards[num].Color);  // временная карта

          int cardsInSelected = 0;
          foreach (Card card in selectedCards)
          {
            if (card.Suit == cards[num].Suit &&
                card.Type == cards[num].Type &&
                card.Color == cards[num].Color)
              cardsInSelected++;
          }

          if (cardsInSelected >= cards[num].maxCount - 1)
          {
            cards[num].nil = true;
            paintRow(num);
            cards[num].nil = false;

            int x = 10, y = 20;
            getPositionInSelectedCards(out x, out y);

            selectedCards.Add(cards[num]);

            cards[num].X = x;
            cards[num].Y = y;
            cards[num].choosen = true;
            g.DrawImage(cards[num].Picture, x, y, cards[num].Width, cards[num].Height);

            SelectedCard = null;
          }
          else
          {
            int x = 10, y = 20;
            getPositionInSelectedCards(out x, out y);

            paintRow(num);

            selectedCards.Add(temp);

            temp.choosen = true;
            temp.X = x;
            temp.Y = y;

            g.DrawImage(temp.Picture, x, y, temp.Width, temp.Height);

            SelectedCard = null;
          }

          pictureBox2.Image = selected_bmp;
        }
      }
    }

    private void otherCardsPB_MouseClick(object sender, MouseEventArgs e)
    {
      if (SelectedCard != null)  // если до этого была выбрана какая-то карта, нужно её зарисовать
      {
        if (SelectedCard.choosen)  // если выделенная карта - среди выбранных
        {
          int selectedCardNumber = selectedCards.IndexOf(SelectedCard);

          repaintSelectedCards(selectedCardNumber);

          SelectedCard = null;
        }
        else
        {
          if (e.X >= SelectedCard.X &&                         // если выбираем уже выбранную карту
              e.Y >= SelectedCard.Y &&
              e.X <= SelectedCard.X + SelectedCard.Width &&
              e.Y <= SelectedCard.Y + SelectedCard.Height)
            return;
        }
      }

      // при нажатии на карту происходит её выделение

      if (getCardNumber(e.X, e.Y) != -1)
      {
        SelectedCard = cards[getCardNumber(e.X, e.Y)];
      }
      else
      {
        SelectedCard = null;
      }
    }


    private void otherCardsPB_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (selectedPageIndex == 2 && SelectedCard != null)
      {
        if (e.X >= SelectedCard.X &&         // двойной клик по уже выбранной карте                         
            e.Y >= SelectedCard.Y &&
            e.X <= SelectedCard.X + SelectedCard.Width &&
            e.Y <= SelectedCard.Y + SelectedCard.Height)
        {
          Graphics g = Graphics.FromImage(selected_bmp);

          int num = getCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5);

          if (num == -1)
            return;

          // Работа с добавлением нескольких карт

          Card temp = new Card(cards[num].Suit, cards[num].Type, cards[num].Color);  // временная карта

          int cardsInSelected = 0;
          foreach (Card card in selectedCards)
          {
            if (card.Suit == cards[num].Suit &&
                card.Type == cards[num].Type &&
                card.Color == cards[num].Color)
              cardsInSelected++;
          }

          if (cardsInSelected >= cards[num].maxCount - 1)
          {
            cards[num].nil = true;
            paintRow(num);
            cards[num].nil = false;

            int x = 10, y = 20;
            getPositionInSelectedCards(out x, out y);

            selectedCards.Add(cards[num]);

            cards[num].X = x;
            cards[num].Y = y;
            cards[num].choosen = true;
            g.DrawImage(cards[num].Picture, x, y, cards[num].Width, cards[num].Height);

            SelectedCard = null;
          }
          else
          {
            int x = 10, y = 20;
            getPositionInSelectedCards(out x, out y);

            selectedCards.Add(temp);

            temp.choosen = true;
            temp.X = x;
            temp.Y = y;

            g.DrawImage(temp.Picture, x, y, temp.Width, temp.Height);

            SelectedCard = null;
          }

          pictureBox2.Image = selected_bmp;
        }
      }
    }

    private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (SelectedCard != null && !SelectedCard.choosen)  // при выборе другой вкладки, если была выбранная карта, то она уходит в null и её столбец перерисовывается
      {
        switch (selectedPageIndex)
        {
          case 0: { paintRow(getCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5)); break; }
          case 1: { paintRow(getCardNumber(SelectedCard.X + 5, SelectedCard.Y + 5)); break; }
        }

        SelectedCard = null;
      }
      selectedPageIndex = e.TabPageIndex;   // изменяем хранимый индекс
    }


    // Завершение создание опыта
    private void завершитьToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
