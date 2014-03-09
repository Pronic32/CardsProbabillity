using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CardsProbability
{
  class Event
  {

    public string Name = "";

    public Bitmap bmp_one;
    public Bitmap bmp_second;
    public Bitmap bmp_third;
    public Bitmap bmp_selected;
    public Card[] cards = new Card[98];

    public List<Card> SelectedCards = new List<Card>();
    public byte CardsCount { get { return (byte)SelectedCards.Count(); } }
    private EventClassificaions classification = EventClassificaions.Invalid;
    public EventClassificaions Classification { get { return classification; } }
    public Event(string name, List<Card> selectedCards, Bitmap bmp1, Bitmap bmp2, Bitmap bmp3, Bitmap bmp_sel, Card[] cards)
    {
      Name = name;
      SelectedCards = selectedCards;
      bmp_one = bmp1;
      bmp_second = bmp2;
      bmp_third = bmp3;
      bmp_selected = bmp_sel;
      this.cards = cards;

    }

    public Fraction Probability(Experience Exp, out string error)
    {
      byte Total = Exp.TotalCount;
      byte Extracted = Exp.ExtractedCount;
      Fraction Result = new Fraction();
      byte[] argument;
      error = "";
      classification = defineEventClassification(out argument);
      switch (classification)
      {
        case EventClassificaions.Obviously:
          {
            Result = EventRates.ReversedArrangements(CardsCount, Total); 
            break;
          }
        case EventClassificaions.OneType:
          {
            Result = EventRates.OneTypeEventRate(Total, Extracted, CardsCount);
            break;
          }
        case EventClassificaions.OneSuit:
          {
            Result = EventRates.OneSuitEventRate(Total, Extracted, CardsCount);
            break;
          }
        case EventClassificaions.OneColor:
          {
            Result = EventRates.OneColorEventRate(Total, Extracted, CardsCount);
            break;
          }
        case EventClassificaions.MultiType:
          {
            Result = EventRates.MultiTypeEventRate(Total, Extracted, argument, out error);
            break;
          }
        case EventClassificaions.MultiSuit:
          {
            Result = EventRates.MultiSuitEventRate(Total, Extracted, argument, out error);
            break;
          }
        case EventClassificaions.MultiColor:
          {
            Result = EventRates.MultiColorEventRate(Total, Extracted, argument, out error);
            break;
          }
        /*case EventClassificaions.Invalid:
          {
            Result = EventRates.ReversedArrangements(CardsCount, Total);
            break;
          }*/
        // Извиниться перед пользователем и откляняться 

      }
      return Result;
    }

    static byte[] DelZeros(byte[] Arr)
    {
      List<byte> NonZeros = new List<byte>();
      foreach (byte item in Arr)
        if (item != 0) NonZeros.Add(item);
      return NonZeros.ToArray();
    }




    // Сделать потом по-людски, через методы List или как там еще делают
    /*
     * 22.02 22:21. Я только начинаю писать проверки для явно описанных карт, но уже чую, что пишу коряво.
     * Сейчас главное сделать стабильно работающий алгоритм. Потом займусь оптимизацией. Да, о ней нужно думать сразу.
     * Да, она нужна даже в рамках нашего маленького по вычислений проекта. Это скорее эстетика. Вернусь к ней позже за чашкой кофе
     */
    /// <summary>
    /// Определяет классификацию события по набору карт
    /// </summary>
    /// <returns>Классификация события</returns>
    private EventClassificaions defineEventClassification(out byte[] args)
    {
      args = null;
      bool Obviously = true; // все карты описаны явно
      /* Проверки для явно описанных карт - карт, у которых заданы масть и тип */
      foreach (Card item in SelectedCards)
        if ((item.Suit == Suits.None) || (item.Type == Types.None))
          Obviously = false;
      if (Obviously) return EventClassificaions.Obviously;


      /* 
       * Проверки для неявно описанных карт (красная карта, красный король, любая карта пик...)
       * Пусть, если все выбранные карты описаны однообразно (только короли, все карты красные), раскладку будем считать монотонной
       */
      bool OneTypeFinded = true;
      bool OneSuitFinded = true;
      bool OneColorFinded = true;
      #region Проверки на монотонность
      for (int first = 0; first < CardsCount; first++)
      {
        for (int second = first + 1; second < CardsCount; second++)
        {
          if (SelectedCards[first].Type != SelectedCards[second].Type) // Например, последовательность из любых королей
          {
            OneTypeFinded = false;
          }
          if (SelectedCards[first].Suit != SelectedCards[second].Suit) // или пик без типа
          {
            OneSuitFinded = false;
          }

          if (SelectedCards[first].Color != SelectedCards[second].Color)// или красные король, валет шестерка....
          {
            OneColorFinded = false;
          }
        }
      }
      #endregion

      if ((OneTypeFinded) && (SelectedCards[0].Type != Types.None)) return EventClassificaions.OneType;

      if ((OneSuitFinded) && (SelectedCards[0].Suit != Suits.None)) return EventClassificaions.OneSuit;

      if ((OneColorFinded) && (SelectedCards[0].Color != Colors.None)) return EventClassificaions.OneColor;

      /* Если раскладка не монотонная, то для нахождения в будущем вероятности нам необходимо
       * хранить последовательность каждого класса (2 короля, 1 валет, 3 дамы; две пики, 5 треф; 4 красные карты, 2 черных).
       * 
       * Чтобы определить, к какому из составных классов относится раскладка (составная по типу, масти, цвету),
       * необходимо выявить первую максимально длинную монотонную последовательность, выявить ее класс и за класс раскладки принять
       * сосотавной вид выявленного класса. 
       * Например, раскладка : 3 короля, 3 валета, 1 дама. 
       * В раскладке максимально длинная монотонная последовательность - 3 короля. Они имеют общий тип (король),
       * следовательно вся раскладка является составной по типу.
       * 
       * Решение необходимо выносить на основе того свойства, которое присутствует у всех карт. В вышеописанном примере все карт имели тип.
       * 
       * Рассмотрим такой пример:
       * Раскладка: 3 любых карты пик, 3 черных карты
       * В этом случае, максимально длинная монотонная последовательность 3 карты пик. Они имеют общую масть (пики),
       * следовательно, можно предположить, что вся раскладка является составной по масти.
       * Но у одной последовательности карт масть отсутствует - 3 черных карты. Эта последовательность имеет лишь свойство цвета, 
       * имеющееся также и у всех других карт, следовательно раскладка является составной по цвету - 3 красных, 3 черных.
       * 
       * Также, стоит отметить, что раскладка 3 любых карты пик, 1 король треф, 2 красных карты является невалидной в рамках нашего ПО, т.к.
       * приведение ее к общему свойству имеет несколько одинаковых по свойству последовательности: 3 красных (пики), 1 черная (трефы), 2 красных.
       * 
       * В таком случае, вероятность будем читать по формуле 1/36! + 1/35! ...1/(36-k+1), где k - количество карт в раскладке.
       * 
       * 
       */
      #region Проверки на составную последовательность

      // здесь будем хранить длины монотонностей
      byte[] arrType = new byte[15];
      byte[] arrSuit = new byte[5];
      byte[] arrColor = new byte[3];

      // здесь будем хранить максимальные длины монотонностей
      byte maxType, maxSuit, maxColor = 0;


      foreach (Card current in SelectedCards)
      {
        // засчитываем свойства каждой карты
        if (current.Type != Types.None)
        {
          arrType[(int)current.Type]++;
        }
        if (current.Suit != Suits.None)
        {
          arrSuit[(int)current.Suit]++;
        }
        if (current.Color != Colors.None)
        {
          arrColor[(int)current.Color]++;
        }

      }
      maxType = arrType.Max();
      maxSuit = arrSuit.Max();
      maxColor = arrColor.Max();





      /* После того, как найдена максимальная монотонность, необхдимо удостовериться, что все карты обладаюют этим свойством.
       * Если 0-й элемент массива свойства имеет значение больше нуля, следовательно этим свойствам обладают не все карты,
       * т.к. в перечислениях Types, Suits, Colors нулевое значение = None.
       * Обладают ли все карты определенным свойством или нет также можно определить, посчитав сумму всех элементов массива данного свойства
       * 
       * Говоря проще, мы просматриваем все три свойства. начиная с того, что содержит максимальную монотонность и выбираем то свойство, 
       * которым точно обладают все карты.
       */

      // Если свойство "тип"  содержит максимальную монотонность и этим свойством обладают все карты
      if ((maxType >= maxSuit) && (maxType >= maxColor) && (arrType[0] == 0))
      {
        args = DelZeros(arrType);
        return EventClassificaions.MultiType;
      }

      if ((maxSuit >= maxType) && (maxSuit >= maxColor) && (arrSuit[0] == 0))
      {
        args = DelZeros(arrSuit);
        return EventClassificaions.MultiSuit;
      }

      if ((maxColor >= maxType) && (maxColor >= maxSuit) && (arrColor[0] == 0))
      {
        args = DelZeros(arrColor);
        return EventClassificaions.MultiColor;
      }


      #endregion


      return EventClassificaions.Invalid;
    }
  }

  enum EventClassificaions
  {
    OneType = 0,
    OneSuit = 1,
    OneColor = 2,

    MultiType = 3,
    MultiSuit = 4,
    MultiColor = 5,
    Obviously = 127,
    Invalid = 255,
  }
}
