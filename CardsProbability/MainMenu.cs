using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CardsProbability
{

  public partial class MainScreen : Form
  {
    public MainScreen() { InitializeComponent(); }

    /// <summary>
    /// Список выбранных карт для всех событий
    /// </summary>
    new List<Event> Events = new List<Event>(); // new скрывает наследуемый член "System.ComponentModel.Component.Events"
    /// <summary>
    /// Текущий опыт. Опыт может быть только один. После добавления событий, опыт можно только удалять.
    /// </summary>
    Experience CurrentExperience;

    /// <summary>
    /// Удаление выбранного события из листбокса и списка выбранных карт
    /// </summary>
    void DeleteSelectedEvent()
    {

      if (lstbx_events.SelectedIndex != -1) // если выбрано событие
      {
        string EventName = "\"" + Events[lstbx_events.SelectedIndex].Name;
        if (EventName.ToLower().IndexOf("событие") == -1)
        {
          EventName = "Событие " + EventName;
        }
        EventName += "\"";
        // и пользователь уверен в своих действиях
        if (MessageBox.Show("Вы действительно хотите удалить " + EventName + "?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          Events.RemoveAt(lstbx_events.SelectedIndex);
          lstbx_events.Items.RemoveAt(lstbx_events.SelectedIndex);
        }

      }
      else
      {

        MessageBox.Show("Выберите опыт или событие для удаления");
        return;

      }




    }

    /// <summary>
    /// Загружает раскладку кард выбранного события
    /// </summary>
    void ShowSelectedEvent()
    {

      WorkSheet ExperienceSheet = new WorkSheet();

      try
      {
        ExperienceSheet.selectedCards = Events[lstbx_events.SelectedIndex].SelectedCards;
        ExperienceSheet.bmp_one = Events[lstbx_events.SelectedIndex].bmp_one;
        ExperienceSheet.bmp_second = Events[lstbx_events.SelectedIndex].bmp_second;
        ExperienceSheet.bmp_third = Events[lstbx_events.SelectedIndex].bmp_third;
        ExperienceSheet.selected_bmp = Events[lstbx_events.SelectedIndex].bmp_selected;
        ExperienceSheet.cards = Events[lstbx_events.SelectedIndex].cards;
      }
      catch (ArgumentOutOfRangeException) // список событий пуст
      {

        MessageBox.Show("Выберите опыт или событие для изменения набора карт");
        return;

      }

      ExperienceSheet.ShowDialog(); // если хоть что-то есть - открываем рабочее пространство с раскладкой

    }

    private void btn_addExperience_Click(object sender, EventArgs e)
    {
      ExperienceForm ExpForm = new ExperienceForm();
      if (CurrentExperience != null) // если опыт  добавляли
      {
        ExpForm.Exp = CurrentExperience;
      }
      ExpForm.ShowDialog();
      if (ExpForm.Exp.TotalCount != 0)
      {
        CurrentExperience = ExpForm.Exp;
        btn_addEvent.Enabled = true;
      }


    }


    private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      ShowSelectedEvent();
    }

    private void btn_addEvent_Click(object sender, EventArgs e)
    {

      string eventName = Microsoft.VisualBasic.Interaction.InputBox("Укажите название события:", "Cards Probability", "");
      if (eventName == "") return;
      WorkSheet ExperienceSheet = new WorkSheet();
      ExperienceSheet.ShowDialog();
      if (ExperienceSheet.selectedCards.Count() != 0)
      {
        Events.Add(new Event(eventName, ExperienceSheet.selectedCards,
            ExperienceSheet.bmp_one, ExperienceSheet.bmp_second,
            ExperienceSheet.bmp_third, ExperienceSheet.selected_bmp, ExperienceSheet.cards)); // в список всех событий
        lstbx_events.Items.Add(Events.Last().Name); // имя - в listbox
        btn_editEvent.Enabled = btn_delEvent.Enabled = btn_showProbs.Enabled = true;   // до этого событий не было, нечего было редактировать и т.д.
      }


    }

    private void btn_editEvent_Click(object sender, EventArgs e)
    {
      ShowSelectedEvent();
    }

    private void btn_delEvent_Click(object sender, EventArgs e)
    {

      DeleteSelectedEvent(); // удаляем событие из списка событий и листбокса


      if (lstbx_events.Items.Count == 0) // если больше нет событий
      {

        btn_delEvent.Enabled = btn_editEvent.Enabled = btn_showProbs.Enabled = false;
      }
    }

    private void btn_showProbs_Click(object sender, EventArgs e)
    {
      string s;
      Fraction f = Events[0].Probability(CurrentExperience, out s);
      if (s == "")
      {
        MessageBox.Show("Класс события: " + Events[0].Classification.ToString() +
            "\nВероятность события: " + f.Numerator.ToString() + "/" + f.Denominator.ToString());
      }
      else
      {
        MessageBox.Show(s);
      }
    }

  }
}
