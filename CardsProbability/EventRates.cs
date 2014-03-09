using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CardsProbability
{
  public static class EventRates
  {
    public static Fraction ReversedArrangements(byte k, byte N)
    {
      Fraction Result;
      Result.Numerator = Result.Denominator = 1;
      for (byte i = N; (N - i) < k; i--)
      {

        Result.Denominator *= i;
      }
      return FractionCalculation.Reduce(Result);
    }


    /// <summary>
    /// Факториал
    /// </summary>
    public static ulong Factorial(int N)
    {
      ulong Result = 1;
      for (byte i = 2; i <= N; i++)
        Result *= i;
      return Result;
    }


    /// <summary>
    /// Мистический биномиал
    /// </summary>
    public static ulong Binomial(byte K, byte N)
    {
      ulong Result = 1;
      if (K > N / 2)
        K = Convert.ToByte(N - K);
      for (byte i = 1; i <= K; i++)
      {
        if ((N - i + 1) % i == 0)
          Result = Convert.ToByte((N - i + 1) / i) * Result;
        else
          Result = Result / i * Convert.ToByte((N - i + 1));
      }
      return Result;
    }

    /// <summary>
    /// Получает вероятность, что из "Extracted" карт "OneTypeCardsAmount" будут типа, заданного пользователем.
    /// <para></para>В случае неверных вводных данных записывает ошибку в ErrorMessage.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneTypeCardsAmount">Число карт одного типа</param>
    /// <param name="ErrorMessage">Сообщение об ошибке</param>
    public static Fraction OneTypeEventRate(byte TotalCardsAmount, byte Extracted, byte OneTypeCardsAmount, out string ErrorMessage)
    {
      ErrorMessage = "";
      if (Extracted < OneTypeCardsAmount)
        ErrorMessage = "Число карт одного типа не может быть меньше количества вынутых карт";
      if (Extracted + 4 - OneTypeCardsAmount > TotalCardsAmount)
        ErrorMessage = "Неосуществимое условие, при " + Extracted + " вынутых картах будет вытащено не менее чем " + (Extracted + 4 - TotalCardsAmount) + " карт одного типа";
      if (OneTypeCardsAmount > 4)
        ErrorMessage = "Существует всего 4 карты каждого типа";
      if (Extracted > TotalCardsAmount)
        ErrorMessage = "Нельзя вынуть карт больше, чем есть в колоде";
      if (ErrorMessage == "")
        return OneTypeEventRate(TotalCardsAmount, Extracted, OneTypeCardsAmount);
      else
      {
        Fraction Frac;
        Frac.Numerator = 0;
        Frac.Denominator = 1;
        return Frac;
      }
    }

    /// <summary>
    /// Получает вероятность, что из "Extracted" карт "OneTypeCardsAmount" будут типа, заданного пользователем.
    /// <para></para>Проверка на правильность исходных данных не осуществляется.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneTypeCardsAmount">Число карт одного типа</param>
    public static Fraction OneTypeEventRate(byte TotalCardsAmount, byte Extracted, byte OneTypeCardsAmount)
    {
      Fraction Result;
      Result.Numerator = Binomial(OneTypeCardsAmount, 4) * Binomial(Convert.ToByte(Extracted - OneTypeCardsAmount), Convert.ToByte(TotalCardsAmount - 4));
      Result.Denominator = Binomial(Extracted, TotalCardsAmount);
      return FractionCalculation.Reduce(Result);
    }
    //Для масти не протестировано!!
    /// <summary>
    /// Получает вероятность, что из "Extracted" карт "OneSuitCardsAmount" будут масти, заданного пользователем.
    /// <para></para>В случае неверных вводных данных записывает ошибку в ErrorMessage.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneSuitCardsAmount">Число карт одной масти</param>
    /// <param name="ErrorMessage">Сообщение об ошибке</param>
    public static Fraction OneSuitEventRate(byte TotalCardsAmount, byte Extracted, byte OneSuitCardsAmount, out string ErrorMessage)
    {
      ErrorMessage = "";
      if (Extracted < OneSuitCardsAmount)
        ErrorMessage = "Число карт одной масти не может быть меньше количества вынутых карт";
      if (Extracted + TotalCardsAmount / 4 - OneSuitCardsAmount > TotalCardsAmount)
        ErrorMessage = "Неосуществимое условие, при " + Extracted + " вынутых картах будет вытащено не менее чем " + (Extracted + TotalCardsAmount / 4 - TotalCardsAmount) + " карт одной масти";
      if (OneSuitCardsAmount > TotalCardsAmount / 4)
        ErrorMessage = "Существует всего " + TotalCardsAmount / 4 + " карты каждой масти";
      if (Extracted > TotalCardsAmount)
        ErrorMessage = "Нельзя вынуть карт больше, чем есть в колоде";
      if (ErrorMessage == "")
        return OneSuitEventRate(TotalCardsAmount, Extracted, OneSuitCardsAmount);
      else
      {
        Fraction Frac;
        Frac.Numerator = 0;
        Frac.Denominator = 1;
        return Frac;
      }
    }

    /// <summary>
    /// Получает вероятность, что из "Extracted" карт "OneSuitCardsAmount" будут масти, заданного пользователем.
    /// <para></para>Проверка на правильность исходных данных не осуществляется.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneSuitCardsAmount">Число карт одной масти</param>
    public static Fraction OneSuitEventRate(byte TotalCardsAmount, byte Extracted, byte OneSuitCardsAmount)
    {
      Fraction Result;
      // k - количество вынутых одной масти, n - число карт одной масти (1/4 всех карт в колоде)
      Result.Numerator = Binomial(OneSuitCardsAmount, Convert.ToByte(TotalCardsAmount / 4))
        // k - остальные вынутые карты из числа других мастей (3/4 всех карт в колоде)
          * Binomial(Convert.ToByte(Extracted - OneSuitCardsAmount), Convert.ToByte(TotalCardsAmount - TotalCardsAmount / 4));
      Result.Denominator = Binomial(Extracted, TotalCardsAmount);
      return FractionCalculation.Reduce(Result);
    }

    /// <summary>
    /// Получает вероятность, что из "Extracted" карт "OneColorCardsAmount" будут одного цвета, заданного пользователем.
    /// <para></para>В случае неверных вводных данных записывает ошибку в ErrorMessage.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneColorCardsAmount">Число карт одного цвета</param>
    /// <param name="ErrorMessage">Сообщение об ошибке</param>
    public static Fraction OneColorEventRate(byte TotalCardsAmount, byte Extracted, byte OneColorCardsAmount, out string ErrorMessage)
    {
      ErrorMessage = "";
      if (Extracted < OneColorCardsAmount)
        ErrorMessage = "Число карт одного цвета не может быть меньше количества вынутых карт";
      if (Extracted + Convert.ToByte(TotalCardsAmount / 2) - OneColorCardsAmount > TotalCardsAmount)
        ErrorMessage = "Неосуществимое условие, при " + Extracted + " вынутых картах будет вытащено не менее чем " + (Extracted + Convert.ToByte(TotalCardsAmount / 2) - TotalCardsAmount) + " карт одного цвета";
      if (OneColorCardsAmount > Convert.ToByte(TotalCardsAmount / 2))
        ErrorMessage = "Существует всего " + Convert.ToByte(TotalCardsAmount / 2) + " карт каждого цвета";
      if (Extracted > TotalCardsAmount)
        ErrorMessage = "Нельзя вынуть карт больше, чем есть в колоде";
      if (ErrorMessage == "")
        return OneColorEventRate(TotalCardsAmount, Extracted, OneColorCardsAmount);
      else
      {
        Fraction Frac;
        Frac.Numerator = 0;
        Frac.Denominator = 1;
        return Frac;
      }
    }

    /// <summary>
    /// Получает вероятность, что из "Extracted" карт "OneColorCardsAmount" будут одного цвета, заданного пользователем.
    /// <para></para>Проверки исходных данных не происходит.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneColorCardsAmount">Число карт одного цвета</param>
    public static Fraction OneColorEventRate(byte TotalCardsAmount, byte Extracted, byte OneColorCardsAmount)
    {
      Fraction Result;
      Result.Numerator = Binomial(OneColorCardsAmount, Convert.ToByte(TotalCardsAmount / 2))
                         * Binomial(Convert.ToByte(Extracted - OneColorCardsAmount), Convert.ToByte(TotalCardsAmount - Convert.ToByte(TotalCardsAmount / 2)));
      Result.Denominator = Binomial(Extracted, TotalCardsAmount);
      return FractionCalculation.Reduce(Result);
    }
    //Посчитать вручную для проверки
    /// <summary>
    /// Получает вероятность, что из "Extracted" карт будет вытащенно количество карт разных типов, указанное пользователем.
    /// <para></para>Исходные данные проверяются.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="CardsAmount">Массив количества карт одного типа</param>
    /// <param name="ErrorMessage">Сообщение об ошибке</param>
    public static Fraction MultiTypeEventRate(byte TotalCardsAmount, byte Extracted, byte[] CardsAmount, out string ErrorMessage)
    {
      ErrorMessage = "";
      Fraction Result;
      Result.Numerator = 0;
      Result.Denominator = 1;
      if (TotalCardsAmount / 4 < CardsAmount.Length)
        ErrorMessage = "В колоде из " + TotalCardsAmount + " карт может быть не более " + TotalCardsAmount / 4 + " типов карт";
      int CardsSum = 0;
      if (ErrorMessage == "")
      {
        for (int i = 0; i < CardsAmount.Length; i++)
        {
          if (CardsAmount[i] <= 4)
            CardsSum += CardsAmount[i];
          else
          {
            ErrorMessage = "В колоде не может быть более 4 карт одного типа";
            break;
          }
        }
      }
      if ((ErrorMessage == "") && CardsSum > Extracted)
        ErrorMessage = "Нельзя получить " + CardsSum + " карт из " + Extracted + " извлеченных";
          if ((ErrorMessage == "") && Extracted + 4 * CardsAmount.Length - TotalCardsAmount > CardsSum)
          ErrorMessage = "Будет вытащенно не менее чем " + (Extracted + 4 * CardsAmount.Length - TotalCardsAmount) + " карт заданных типов";
      if (ErrorMessage == "")
      {
        Result.Numerator = Binomial(Convert.ToByte(Extracted - CardsSum), Convert.ToByte(TotalCardsAmount - 4 * CardsAmount.Length));
        Result.Denominator = Binomial(Extracted, TotalCardsAmount);
        for (int i = 0; i < CardsAmount.Length; i++)
          Result.Numerator *= Binomial(CardsAmount[i], 4);
        Result = FractionCalculation.Reduce(Result);
      }
      return Result;

    }
    //Не проверенно
    /// <summary>
    /// Получает вероятность, что из "Extracted" карт будет вытащенно количество карт разных цветов,
    /// <para></para>количесто которых указанно пользователем.
    /// <para></para>Исходные данные проверяются.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneColorCardsAmount">Массив количества карт одного цвета</param>
    /// <param name="ErrorMessage">Сообщение об ошибке</param>
    public static Fraction MultiColorEventRate(byte TotalCardsAmount, byte Extracted, byte[] CardsAmount, out string ErrorMessage)
    {
      ErrorMessage = "";
      Fraction Result;
      Result.Numerator = 0;
      Result.Denominator = 1;
      if (2 < CardsAmount.Length)
        ErrorMessage = "В любой колоде всего 2 цвета карт";
      int CardsSum = 0;
      if (ErrorMessage == "")
      {
        for (int i = 0; i < CardsAmount.Length; i++)
        {
          if (CardsAmount[i] <= TotalCardsAmount / 2)
            CardsSum += CardsAmount[i];
          else
          {
            ErrorMessage = "В колоде не может быть более " + TotalCardsAmount / 2 + " карт одного цвета";
            break;
          }
        }
      }
      if ((ErrorMessage == "") && CardsSum > Extracted)
        ErrorMessage = "Нельзя получить " + CardsSum + " карт из " + Extracted + " извлеченных";
      if ((ErrorMessage == "") && Extracted + TotalCardsAmount / 2 * CardsAmount.Length - TotalCardsAmount > CardsSum)
        ErrorMessage = "Будет вытащенно не менее чем " + (Extracted + TotalCardsAmount / 2 * CardsAmount.Length - TotalCardsAmount) + " карт заданных цветов";
      if (ErrorMessage == "")
      {
        Result.Numerator = Binomial(Convert.ToByte(Extracted - CardsSum), Convert.ToByte(TotalCardsAmount - TotalCardsAmount / 2 * CardsAmount.Length));
        Result.Denominator = Binomial(Extracted, TotalCardsAmount);
        for (int i = 0; i < CardsAmount.Length; i++)
          Result.Numerator *= Binomial(CardsAmount[i], Convert.ToByte(TotalCardsAmount / 2));
        Result = FractionCalculation.Reduce(Result);
      }
      return Result;

    }
    //Не проверенно
    /// <summary>
    /// Получает вероятность, что из "Extracted" карт будет вытащенно количество карт разных мастей,
    /// <para></para>количесто которых указанно пользователем.
    /// <para></para>Исходные данные проверяются.
    /// </summary>
    /// <param name="TotalCardsAmount">Число карт в колоде</param>
    /// <param name="Extracted">Количество вынутых карт</param>
    /// <param name="OneSuitCardsAmount">Массив количества карт одной масти</param>
    /// <param name="ErrorMessage">Сообщение об ошибке</param>
    public static Fraction MultiSuitEventRate(byte TotalCardsAmount, byte Extracted, byte[] CardsAmount, out string ErrorMessage)
    {
      ErrorMessage = "";
      Fraction Result;
      Result.Numerator = 0;
      Result.Denominator = 1;
      if (4 < CardsAmount.Length)
        ErrorMessage = "В любой колоде всего 4 масти";
      int CardsSum = 0;
      if (ErrorMessage == "")
      {
        for (int i = 0; i < CardsAmount.Length; i++)
        {
          if (CardsAmount[i] <= TotalCardsAmount / 4)
            CardsSum += CardsAmount[i];
          else
          {
            ErrorMessage = "В колоде не может быть более " + TotalCardsAmount / 4 + " карт одной масти";
            break;
          }
        }
      }
      if ((ErrorMessage == "") && CardsSum > Extracted)
        ErrorMessage = "Нельзя получить " + CardsSum + " карт из " + Extracted + " извлеченных";
      if ((ErrorMessage == "") && Extracted + TotalCardsAmount / 4 * CardsAmount.Length - TotalCardsAmount > CardsSum)
        ErrorMessage = "Будет вытащенно не менее чем " + (Extracted + TotalCardsAmount / 4 * CardsAmount.Length - TotalCardsAmount) + " карт заданных цветов";
      if (ErrorMessage == "")
      {
        Result.Numerator = Binomial(Convert.ToByte(Extracted - CardsSum), Convert.ToByte(TotalCardsAmount - TotalCardsAmount / 4 * CardsAmount.Length));
        Result.Denominator = Binomial(Extracted, TotalCardsAmount);
        for (int i = 0; i < CardsAmount.Length; i++)
          Result.Numerator *= Binomial(CardsAmount[i], Convert.ToByte(TotalCardsAmount / 4));
        Result = FractionCalculation.Reduce(Result);
      }
      return Result;

    }

    private static void GetSuitableCards(byte TotalCardsAmount, Card[] SelectedCards, ref BitArray[] SuitableCards)
    {
      for (byte i = 0; i < SuitableCards.Length; i++)
      {
        if (SelectedCards[i].Type == 0)//Если тип карты не выбран
          if (SelectedCards[i].Suit == 0)//Если тип и масть карты не выбраны
            if (SelectedCards[i].Color == 0)//Если тип и цвет краты не выбраны
              for (byte j = 0; j < SuitableCards[i].Length; j++)
                SuitableCards[i].Set(j, true);
            else//Если выбран только цвет карты
              for (byte j = 0; j < SuitableCards[i].Length; j++)
              {
                if (j % 2 == (byte)(SelectedCards[i].Color - 1))
                  SuitableCards[i].Set(j, true);
              }
          else//Если выбрана только масть карты
            for (byte j = 0; j < SuitableCards[i].Length; j++)
            {
              if (j % 4 == (byte)(SelectedCards[i].Suit - 1))
                SuitableCards[i].Set(j, true);
            }
        //Если тип карты выбран
        else
        {
          byte TypesAmount = (byte)(TotalCardsAmount / 4);
          if (SelectedCards[i].Suit == 0)//Если масть карты не выбрана
            if (SelectedCards[i].Color == 0)//Если выбран только тип карты
              for (byte j = 0; j < SuitableCards[i].Length; j++)
              {
                if (j % TypesAmount == (byte)(SelectedCards[i].Type - (Types.Ace - TypesAmount) - 1))
                  SuitableCards[i].Set(j, true);
              }
            else//Если выбраны тип и цвет карты
              for (byte j = 0; j < SuitableCards[i].Length; j++)
              {
                if ((j % TypesAmount == (byte)(SelectedCards[i].Type - (Types.Ace - TypesAmount) - 1)) && (j % 2 == (byte)(SelectedCards[i].Color - 1)))
                  SuitableCards[i].Set(j, true);
              }
          else//Если выбраны тип и масть карты
            for (byte j = 0; j < SuitableCards[i].Length; j++)
            {
              if ((j % TypesAmount == (byte)(SelectedCards[i].Type - (Types.Ace - TypesAmount) - 1)) && (j % 4 == (byte)(SelectedCards[i].Suit - 1)))
                SuitableCards[i].Set(j, true);
            }
        }
      }
    }


    private static void GetSuitableCards(byte TotalCardsAmount, Card[] SelectedCards, out byte[][] SuitableCards)
    {
      //В массиве SuitableCards хранятся номера карт, соответствующих выбранным картам
      //Каждой карте соответствует своя строка
      SuitableCards = new byte[SelectedCards.Length][];
      for (byte i = 0; i < SuitableCards.Length; i++)
      {
        if (SelectedCards[i].Type == 0)//Если тип карты не выбран
          if (SelectedCards[i].Suit == 0)//Если тип и масть карты не выбраны
            if (SelectedCards[i].Color == 0)//Если тип и цвет краты не выбраны
            {
              SuitableCards[i] = new byte[TotalCardsAmount];
              for (byte j = 0; j < TotalCardsAmount; j++)
                SuitableCards[i][j] = j;
            }
            else//Если выбран только цвет карты
            {
              SuitableCards[i] = new byte[TotalCardsAmount / 2];
              for (byte j = 0; j < SuitableCards[i].Length; j++)
                SuitableCards[i][j] = (byte)(SelectedCards[i].Color - 1 + j * 2);
            }
          else//Если выбрана только масть карты
          {
            SuitableCards[i] = new byte[TotalCardsAmount / 4];

            for (byte j = 0; j < SuitableCards[i].Length; j++)
              SuitableCards[i][j] = (byte)(SelectedCards[i].Suit - 1 + j * 4);
          }
        //Если тип карты выбран
        else
        {
          //Количество типов карт
          byte TypesAmount = (byte)(TotalCardsAmount / 4);
          if (SelectedCards[i].Suit == 0)//Если масть карты не выбрана
            if (SelectedCards[i].Color == 0)//Если выбран только тип карты
            {
              SuitableCards[i] = new byte[4];
              for (byte j = 0; j < 4; j++)
                SuitableCards[i][j] = (byte)((SelectedCards[i].Type - (Types.Ace - TypesAmount) - 1) * 4 + j);

            }
            else//Если выбраны тип и цвет карты
            {
              SuitableCards[i] = new byte[2];
              for (byte j = 0; j < 2; j++)
                SuitableCards[i][j] = (byte)((SelectedCards[i].Type - (Types.Ace - TypesAmount) - 1) * 4 + SelectedCards[i].Color - 1 + j * 2);
            }
          else//Если выбраны тип и масть карты
          {
            SuitableCards[i] = new byte[1];
            SuitableCards[i][0] = (byte)((SelectedCards[i].Type - (Types.Ace - TypesAmount) - 1) * 4 + SelectedCards[i].Suit - 1);
          }
        }
      }
    }

    public static Fraction RatesByEnumeration(byte TotalCardsAmount, byte Extracted, Card[] SelectedCards, bool OrderIsSignificant = true)
    {
      Card[] AllSelectedCards = new Card[Extracted];
      for (byte i = 0; i < SelectedCards.Length; i++)
        AllSelectedCards[i] = SelectedCards[i];
      for (byte i = (byte)SelectedCards.Length; i < Extracted; i++)
      {
        AllSelectedCards[i] = new Card(0, 0, 0);
        /*
        AllSelectedCards[i] = new Card();
        AllSelectedCards[i].Color = 0;
        AllSelectedCards[i].Suit = 0;
        AllSelectedCards[i].Type = 0;
         */
      }
      return RatesByEnumeration(TotalCardsAmount, AllSelectedCards, OrderIsSignificant);
    }

    public static Fraction RatesByEnumeration(byte TotalCardsAmount, Card[] SelectedCards, bool OrderIsSignificant = true)
    {
      byte[] CardsPriority = new byte[SelectedCards.Length];
      //Определение приоритета карт, по правилу чем меньше вероятность встретить карту с заданными параметрами,
      //тем выше приоритет, раньше она должна обрабатываться
      for (byte i = 0; i < SelectedCards.Length; i++)
      {
        CardsPriority[i] = 0;
        if (SelectedCards[i].Type != Types.None)
          CardsPriority[i] += 3;
        if (SelectedCards[i].Suit != Suits.None)
          CardsPriority[i] += 2;
        if (SelectedCards[i].Color != Colors.None)
          CardsPriority[i] += 1;

      }
      //Сортировка карт, чем выше приоритет, тем раньше должна распологаться карта
      for (byte i = 0; i < SelectedCards.Length - 1; i++)
        for (byte j = Convert.ToByte(i + 1); j < SelectedCards.Length; j++)
          if (CardsPriority[i] < CardsPriority[j])
          {
            byte temp = CardsPriority[j];
            CardsPriority[j] = CardsPriority[i];
            CardsPriority[i] = temp;
            Card Temp = SelectedCards[j];
            SelectedCards[j] = SelectedCards[i];
            SelectedCards[i] = Temp;
          }




      //Отсеиваем пустые карты, так как на вероятность они не влияют
      byte k = (byte)SelectedCards.Length;
      for (int i = SelectedCards.Length - 1; i >= 0; i--)
        if (CardsPriority[i] == 0)
          k = (byte)i;
      Card[] FillingSelectedCards = new Card[k];
      for (byte i = 0; i < k; i++)
        FillingSelectedCards[i] = SelectedCards[i];

      //Для каждой выбранной карты - маска для определения карт, подходящих под заданные условия
      BitArray[] SuitableCards = new BitArray[k];
      for (byte i = 0; i < k; i++)
        SuitableCards[i] = new BitArray(TotalCardsAmount);
      GetSuitableCards(TotalCardsAmount, FillingSelectedCards, ref SuitableCards);

      Fraction Result = new Fraction();
      Result.Numerator = 0;
      Result.Denominator = 1;
      BitArray CardsArrangement = new BitArray(TotalCardsAmount);
      CompleteEnumeration((byte)SuitableCards.Length, ref SuitableCards, ref CardsArrangement, ref Result.Numerator);
      if (!OrderIsSignificant)
      //Если порядок не важен
      {
        Result.Numerator *= Factorial(SelectedCards.Length);
        byte RepeatedCards;
        for (byte i = 0; i < SelectedCards.Length; i++)
        {
          RepeatedCards = 1;
          for (byte j = 0; j < i; j++)
            if (SelectedCards[i].Color == SelectedCards[j].Color && SelectedCards[i].Type == SelectedCards[j].Type
                && SelectedCards[i].Suit == SelectedCards[j].Suit)
              RepeatedCards = 0;
          if (RepeatedCards != 0)
            for (byte j = (byte)(i + 1); j < SelectedCards.Length; j++)
              if (SelectedCards[i].Color == SelectedCards[j].Color && SelectedCards[i].Type == SelectedCards[j].Type
              && SelectedCards[i].Suit == SelectedCards[j].Suit)
                RepeatedCards++;
          Result.Numerator /= Factorial(RepeatedCards);
        }
      }


      for (byte i = 0; i < k; i++)
        if (Result.Numerator % (ulong)(TotalCardsAmount - i) == 0)
          Result.Numerator /= (ulong)(TotalCardsAmount - i);
        else
        {
          Result.Denominator *= (ulong)(TotalCardsAmount - i);
          FractionCalculation.Reduce(ref Result);
        }
      return Result;
    }

    private static void CompleteEnumeration(byte Extracted, ref BitArray[] SuitableCards, ref BitArray CardsArrangement, ref ulong k)
    {
      if (Extracted == 0)
        k = 0;
      if (Extracted == 1)
        for (int i = 0; i < CardsArrangement.Count; i++)
        {
          if ((!CardsArrangement.Get(i)) && SuitableCards[(byte)(SuitableCards.Length - Extracted)].Get(i))
          {
            k++;
          }
        }
      else
      {
        for (int i = 0; i < CardsArrangement.Count; i++)
        {
          if ((!CardsArrangement.Get(i)) && SuitableCards[(byte)(SuitableCards.Length - Extracted)].Get(i))
          {
            CardsArrangement.Set(i, true);
            CompleteEnumeration(Convert.ToByte(Extracted - 1), ref SuitableCards, ref CardsArrangement, ref k);
            CardsArrangement.Set(i, false);
          }
        }
      }
    }
    /// <summary>
    /// Структура для хранения разветвлений формулы полной вероятности
    /// </summary>
    private struct TreeBranch
    {
      public byte CardsAmount;
      public byte SelectedCard;
      public TreeBranch(byte CardsAmount, byte SelectedCard)
      {
        this.CardsAmount = CardsAmount;
        this.SelectedCard = SelectedCard;
      }
    }

    /// <summary>
    /// Получение вероятности выпадения последовательности карт, удовлетворяющей заданным условиям, по формуле полной вероятности
    /// </summary>
    /// <param name="TotalCardsAmount">Количество карт в колоде</param>
    /// <param name="SelectedCards">Заданные карты</param>
    /// <param name="OrderIsSignificant">Важен ли порядок, впрочем на текущий момент это не важно</param>
    /// <returns></returns>
    public static Fraction RatesByFractions(byte TotalCardsAmount, Card[] SelectedCards, bool OrderIsSignificant = true)
    {
      byte[] ExtractedCards = new byte[SelectedCards.Length];
      byte[][] SuitableCards;
      //Получение порядковых номеров карт, удовлетворяющиих заданным условиям
      GetSuitableCards(TotalCardsAmount, SelectedCards, out SuitableCards);
      TreeBranch[] CrossedCards = new TreeBranch[SelectedCards.Length - 1];
      Fraction Result = new Fraction(FractionEnumeration(0, ref SuitableCards, ref ExtractedCards, ref CrossedCards), 1);
      for (byte i = TotalCardsAmount; i > TotalCardsAmount - SelectedCards.Length; i--)
        Result /= i;
      return Result;
    }
    /// <summary>
    /// Рекурсивная функция получения числителя вероятности выпадения заданного набора карт
    /// На каждом вызове возвращает вероятность выпадения текущей карты и карт, следующих за ней(если такие имеются)
    /// </summary>
    /// <param name="CurrentCard">Рассматриваемая(текущая) карта</param>
    /// <param name="SuitableCards">Карты, подходящие под заданные условия</param>
    /// <param name="ExtractedCards">Вынутые на текущий момент карты</param>
    /// <param name="CrossedCards">Массив для промежуточных вычислений</param>
    /// <returns></returns>
    private static ulong FractionEnumeration(byte CurrentCard, ref byte[][] SuitableCards, ref byte[] ExtractedCards, ref TreeBranch[] CrossedCards)
    {
      ulong Result = 0;
      if (CurrentCard == SuitableCards.Length - 1)
      //Если текущая карта - последняя
      {
        //Подсчитываем количество оставшихся карт, удовлетворяющих заданной

        //Записываем число карт в колоде, подходящих для заданной
        Result = (ulong)SuitableCards[CurrentCard].Length;
        //Перебираем все подходящие карты
        for (byte i = 0; i < SuitableCards[CurrentCard].Length; i++)
          //Сравниваем со всеми выбранными ранее
          for (byte j = 0; j < CurrentCard; j++)
            //Если карта, подходящая под условие была выбрана раньше
            if (SuitableCards[CurrentCard][i] == ExtractedCards[j])
              //Уменьшаем число подходящих карт на единицу
              Result--;
      }
      else
      {
        //Если после текущей карты выбираются другие
        //Необходимо определить сколько ветвей рекурсии(n) нужно просчитать
        //Каждая карта либо удовлетворяет условиям последующих, либо нет
        //Карты, являющиеся удовлетворяющие условиям одних и тех же заданных карт, рассматриваются вместе,
        // хранится только информация о первой встретившейся из них, и их количество
        byte n = 0;
        for (byte i = 0; i < SuitableCards[CurrentCard].Length; i++)
        {
          bool CardWasExtracted = false;
          for (byte j = 0; j < CurrentCard && !CardWasExtracted; j++)
          {
            if (SuitableCards[CurrentCard][i] == ExtractedCards[j])
              CardWasExtracted = true;
          }
          if (!CardWasExtracted)
          {
            bool Flag = false;
            bool Flag1;
            bool Flag2;
            for (byte k = 0; k < n && !Flag; k++)
            {
              Flag = true;
              for (byte j = CurrentCard; j < SuitableCards.Length; j++)
              {
                Flag1 = false;
                Flag2 = false;
                for (byte t = 0; t < SuitableCards[j].Length; t++)
                {
                  if (SuitableCards[j][t] == CrossedCards[k].SelectedCard)
                    Flag1 = true;
                  if (SuitableCards[j][t] == SuitableCards[CurrentCard][i])
                    Flag2 = true;
                }
                Flag = Flag && (Flag1 && Flag2 || !Flag1 && !Flag2);
              }
              if (Flag)
                CrossedCards[k].CardsAmount++;
            }
            if (!Flag)
            {
              CrossedCards[n].SelectedCard = SuitableCards[CurrentCard][i];
              CrossedCards[n].CardsAmount = 1;
              n++;
            }
          }
        }

        TreeBranch[] ThisTree = new TreeBranch[n];
        for (byte i = 0; i < n; i++)
          ThisTree[i] = CrossedCards[i];
        for (byte i = 0; i < n; i++)
        {
          ExtractedCards[CurrentCard] = ThisTree[i].SelectedCard;
          Result += ThisTree[i].CardsAmount * FractionEnumeration((byte)(CurrentCard + 1), ref SuitableCards, ref ExtractedCards, ref CrossedCards);
        }
      }
      return Result;
    }
  }
}