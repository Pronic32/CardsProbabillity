using System;
using System.Collections.Generic;
using System.Text;

namespace CardsProbability
{

  public struct Fraction
  {
    /// <summary>
    /// Числитель
    /// </summary>
    public UInt64 Numerator;
    /// <summary>
    /// Знаменатель
    /// </summary>
    public UInt64 Denominator;
    /// <summary>
    /// Создает дробь с указанными числителем и знаменателем
    /// </summary>
    /// <param name="Num">Числитель</param>
    /// <param name="Denom">Знаменатель</param>        
    public Fraction(UInt64 Num, UInt64 Denom)
    {
      Numerator = Num;
      Denominator = Denom;
    }

    public static Fraction operator +(Fraction F1, Fraction F2)
    {
      return FractionCalculation.Sum(F1, F2);
    }

    public static Fraction operator +(Fraction F1, UInt64 N)
    {
      return FractionCalculation.Sum(F1, N);
    }

    public static Fraction operator +(UInt64 N, Fraction F1)
    {
      return FractionCalculation.Sum(F1, N);
    }

    public static Fraction operator *(Fraction F1, Fraction F2)
    {
      return FractionCalculation.Multiplication(F1, F2);
    }

    public static Fraction operator *(Fraction F1, UInt64 N)
    {
      return FractionCalculation.Multiplication(F1, N);
    }

    public static Fraction operator *(UInt64 N, Fraction F1)
    {
      return FractionCalculation.Multiplication(F1, N);
    }

    public static Fraction operator /(Fraction F1, Fraction F2)
    {
      return FractionCalculation.Division(F1, F2);
    }

    public static Fraction operator /(Fraction F1, UInt64 N)
    {
      return FractionCalculation.Division(F1, N);
    }


  }

  public static class FractionCalculation
  {

    /// <summary>
    /// Возвращает сокращенную дробь 
    /// </summary>
    public static Fraction Reduce(Fraction ReducingFraction)
    {

      if (ReducingFraction.Numerator == 0 || ReducingFraction.Denominator == 0)
      {
        ReducingFraction.Numerator = 0;
        ReducingFraction.Denominator = 1;
      }
      else
      {
        if (ReducingFraction.Numerator % ReducingFraction.Denominator == 0)
        {
          ReducingFraction.Numerator /= ReducingFraction.Denominator;
          ReducingFraction.Denominator = 1;
        }
        if (ReducingFraction.Denominator % ReducingFraction.Numerator == 0)
        {
          ReducingFraction.Denominator /= ReducingFraction.Numerator;
          ReducingFraction.Numerator = 1;
        }
        UInt64 i = 2;
        while (i * i <= ReducingFraction.Numerator && i * i <= ReducingFraction.Denominator)
        {
          if (ReducingFraction.Denominator % i == 0 && ReducingFraction.Numerator % i == 0)
          {
            ReducingFraction.Denominator /= i;
            ReducingFraction.Numerator /= i;
          }
          else
            i++;
        }

      }
      return ReducingFraction;
    }

    /// <summary>
    /// Сокращает передаваемую по ссылке дробь 
    /// </summary>
    public static void Reduce(ref Fraction ReducingFraction)
    {
      ReducingFraction = Reduce(ReducingFraction);
    }
    /// <summary>
    /// Сложение дробей
    /// </summary>
    /// <param name="Fraction1"></param>
    /// <param name="Fraction2"></param>
    /// <returns></returns>
    public static Fraction Sum(Fraction Fraction1, Fraction Fraction2)
    {
      Fraction Result;
      Result.Numerator = Fraction1.Numerator * Fraction2.Denominator + Fraction2.Numerator * Fraction1.Denominator;
      Result.Denominator = Fraction2.Denominator * Fraction1.Denominator;
      return Reduce(Result);
    }
    /// <summary>
    /// Сложение дроби и числа
    /// </summary>
    /// <param name="Fraction1"></param>
    /// <param name="Number"></param>
    /// <returns></returns>
    public static Fraction Sum(Fraction Fraction1, UInt64 Number)
    {
      Fraction1.Numerator += Fraction1.Denominator * Number;
      return Fraction1;
    }
    /// <summary>
    /// Умножение дробей
    /// </summary>
    /// <param name="Fraction1"></param>
    /// <param name="Fraction2"></param>
    /// <returns></returns>
    public static Fraction Multiplication(Fraction Fraction1, Fraction Fraction2)
    {
      Fraction1.Numerator *= Fraction2.Numerator;
      Fraction1.Denominator *= Fraction2.Denominator;
      return Reduce(Fraction1);
    }
    /// <summary>
    /// Умножение дроби на число
    /// </summary>        
    public static Fraction Multiplication(Fraction Fraction1, UInt64 Number)
    {
      Fraction1.Numerator *= Number;
      return Reduce(Fraction1);
    }
    /// <summary>
    /// Деление дроби на дробь
    /// </summary>
    /// <param name="Fraction1">Делимое</param>
    /// <param name="Fraction2">Делитель</param>
    /// <returns></returns>
    public static Fraction Division(Fraction Fraction1, Fraction Fraction2)
    {
      Fraction1.Numerator *= Fraction2.Denominator;
      Fraction1.Denominator *= Fraction2.Numerator;
      return Reduce(Fraction1);
    }
    /// <summary>
    /// Деление дроби на число
    /// </summary>
    /// <param name="Fraction1">Делимое</param>
    /// <param name="Number">Делитель</param>
    /// <returns></returns>
    public static Fraction Division(Fraction Fraction1, UInt64 Number)
    {
      Fraction1.Denominator *= Number;
      return Reduce(Fraction1);
    }


  }


}
