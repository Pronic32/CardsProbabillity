using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CardsProbability
{
  public class Card
  {
    public int maxCount = 1;      // Максимальное количество карт в выбранных

    const int DEFAULT_WIDTH = 70; // Ширина и высота картинки
    const int DEFAULT_HEIGHT = 105;
    /// <summary>
    /// Указывает, выбрана ли эта карта из колоды
    /// </summary>
    public Boolean choosen = false;
    /// <summary>
    /// Указывает, выбрана ли вкладка с картой
    /// </summary>
    public Boolean active;

    public Boolean nil = false; // Может можно как-нибудь без этого поля обойтись? По мне, так оно не совсем оправдано.
    /// <summary>
    /// Хранит изображение карты
    /// </summary>
    public Image Picture
    {
      get
      {
        if (nil == true)
        {
          return Image.FromFile("Cards\\noCard.png");
        }
        string Path = "";

        if (Suit != Suits.None) // Если задана масть
          Path = (Type <= (Types)10) ?
              "Cards\\" + (int)Type + Suit + ".png"  // и тип карты от 2 до 10 (2Clubs, ...)
              : "Cards\\" + Type.ToString()[0] + Suit + ".png"; // тип A,J,K,Q (AClubs, ...)
        else if (Color != Colors.None)  // если масть не задана, но задан цвет
        {
          Path = (Type <= (Types)10) ?
              "Cards\\" + (int)Type + Color.ToString()[0] + ".png"  // и тип карты от 2 до 10 
              : "Cards\\" + Type.ToString()[0] + Color.ToString()[0] + ".png"; // тип A,J,K,Q
        } // если ничего не задано, карта "серая" - тип+G
        else Path = (Type <= (Types)10) ?
            "Cards\\" + (int)Type + "G" + ".png"
            : "Cards\\" + Type.ToString()[0] + "G" + ".png";

        if (!"".Equals(Path))
        {
          return Image.FromFile(Path);
        }
        return null;


      }
    }
    private Suits suit = Suits.None;
    public Suits Suit { get { return suit; } set { suit = value; } } // нужно именно свойство, а не поле! Наверное...
    // заменить на поле, потестить...

    private Types type = Types.None;
    public Types Type
    {
      get { return type; }
      set { type = value; }
    }

    private Colors color = Colors.None;
    public Colors Color
    {
      get
      {
        return color;
      }
      set
      {
        if ((suit == Suits.None) & ((int)value <= 2) & ((int)value >= 0)) color = value; // если не задана масть, тогда можно менять цвет, если он [0..2]
        else color = (suit == Suits.Clubs) || (suit == Suits.Spades) ? Colors.Black : Colors.Red; // иначе цвет черный или белый
      }
    }

    /// <summary>
    /// Инициализирует объект Card
    /// </summary>
    /// <param name="suit">Масть</param>
    /// <param name="type">Тип</param>
    /// <param name="Color">Цвет</param>
    /// <param name="name">Выводимое имя</param>
    /// <param name="x">Координата выводимой картинки</param>
    /// <param name="у">Координата выводимой картинки</param>
    /// <param name="w">Ширина выводимой картинки (по умолчанию 70)</param>
    /// <param name="h">Высота выводимой картинки (по умолчанию 105)</param>
    public Card(Suits suit, Types type, Colors color, int x = 0, int y = 0, int w = DEFAULT_WIDTH, int h = DEFAULT_HEIGHT)
    {
      //this.Name = name;
      this.Suit = suit;
      this.Type = type;
      this.Color = color;
      this.X = x;
      this.Y = y;
      this.Width = w;
      this.Height = h;
    }



    #region Свойства
    /// <summary>
    /// Название карты
    /// </summary>
    public string Name
    {
      get
      {
        string result = "";
        if (this.type == Types.None)
        {
          result = TypesNames[0]; // "без типа"
        }
        else
          result = (int)this.type <= 10 ? ((int)this.type).ToString() : TypesNames[(int)this.type - 10]; // 2-10, "валет ", "дама "...
        result += " ";
        if (this.suit == Suits.None)
        {
          result += ColorsNames[(int)this.color];

        }
        else result += SuitsNames[(int)this.suit];

        return result;

      }
    }
    public int X; // { get; set; }  // пустые аксессоры - ни к чему, они дают такое же открытое поле
    public int Y;// { get; set; }
    public int Width; //{ get; set; }
    public int Height; //{ get; set; }

    public Size Size
    {
      get { return new Size(Width, Height); }
    }

    public Point Location
    {
      get { return new Point(X, Y); }
    }

    #endregion


    public void Draw(Graphics g)
    {

      g.DrawImage(Picture, X, Y, Width, Height);
      g.DrawString(Name, SystemFonts.DefaultFont, SystemBrushes.WindowText, new Point(X, Y + Height));

    }
    // Синонимы свойств карты для вывода сообщений и именования карт
    string[] SuitsNames = { "без масти", "треф",  "бубен", "пик","червей" };
    string[] TypesNames = { "Без типа", "Валет", "Дама", "Король", "Туз" };
    string[] ColorsNames = { "любого цвета", "красного цвета", "черного" };
  }

  // Вложенные перечисления
  public enum Suits
  {
    None = 0,
    /// <summary>Трефы</summary>
    Clubs = 1,
    /// <summary>Бубны</summary>
    Diamonds = 2,
    /// <summary>Пики</summary>
    Spades = 3,
    /// <summary>Червы</summary>
    Hearts = 4,
  }

  public enum Types
  {
    /// <summary>Туз </summary>
    Ace = 14,
    /// <summary>Король </summary>
    King = 13,
    /// <summary>Дама </summary>
    Queen = 12,
    /// <summary>Валет </summary>
    Jack = 11,
    /// <summary>Десятка </summary>
    Ten = 10,
    /// <summary>Девятка </summary>
    Nine = 9,
    /// <summary>Восьмерка </summary>
    Eight = 8,
    /// <summary>Семерка </summary>
    Seven = 7,
    /// <summary>Шестерка </summary>
    Six = 6,
    /// <summary>Пятерка </summary>
    Five = 5,
    /// <summary>Четверка </summary>
    Four = 4,
    /// <summary>Тройка </summary>
    Three = 3,
    /// <summary>Двойка </summary>
    Two = 2,
    /// <summary>Тип карты не выбран </summary>
    None = 0,
  }

  public enum Colors
  {
    None = 0,
    Red = 1,
    Black = 2,

  }

}
