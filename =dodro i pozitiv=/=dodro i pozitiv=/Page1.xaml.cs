using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _dodro_i_pozitiv_
{
    public partial class AddNoteWindow : Window
    {
        public zametki NewNote { get; private set; }

        public AddNoteWindow()
        {
            InitializeComponent();
            NewNote = new zametki();
        }
        public AddNoteWindow(zametki note) : this()
        {
            NewNote = note;
            titleTextBox.Text = note.Zametka;
            descriptionTextBox.Text = note.Opisanie;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание новой заметки с данными из текстовых полей и выбранной датой
            NewNote = new zametki
            {
                Zametka = titleTextBox.Text,
                Opisanie = descriptionTextBox.Text,
                Vrema = datePicker.SelectedDate ?? DateTime.Now // Используем выбранную дату, если она есть, или текущую дату
            };

            // Установка результата диалога и закрытие окна
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close(); // Закрывает окно при отмене
        }
    }
}

