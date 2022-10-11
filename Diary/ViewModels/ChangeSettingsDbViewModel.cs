 using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models;
using Diary.ViewModels;
using MahApps.Metro.Controls;

namespace Diary.ViewModels
{
    public class ChangeSettingsDbViewModel : ViewModelBase
    {
        private SettingsDb _settingsDb;
        public SettingsDb SettingsDb 
        { 
            get { return _settingsDb; } 
            set 
            { 
                _settingsDb = value;
                OnPropertyChanged(); 
            } 
        }
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        
        public ChangeSettingsDbViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            _settingsDb = new SettingsDb();
        }

        private void Confirm(object obj)
        {
            if (SettingsDb.IsValid)
            {
                SettingsDb.SaveSettings();
                Restart();
            }
            else
                return;

            
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Restart()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
