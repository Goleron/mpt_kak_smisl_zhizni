using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace _dodro_i_pozitiv_
{
    public interface CRUD
    {
        void Create(DateTime vrema1, TextBox zametka, TextBox opicanie, ListBox vivod, List<zametki> list);
        void Read(List<zametki> list, DateTime vrema1, string hh, List<zametki> list2);
        void Update(DateTime vrema1, TextBox zametka, TextBox opicanie, ListBox vivod, List<zametki> list);
        void Delete(TextBox zametka, TextBox opicanie, ListBox vivod, List<zametki> list, zametki noteToDelete);
    }
}
