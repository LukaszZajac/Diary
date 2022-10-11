using Diary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models
{
    public class SettingsDb:IDataErrorInfo
    {
        //public string this[string columnName] => throw new NotImplementedException();

        private bool _isServerAddressValid;
        private bool _isServerNameValid;
        private bool _isDatabaseValid;
        private bool _isUserValid;
        private bool _isPasswordValid;

        public string ServerAddress 
        {
            get 
            {
                return Settings.Default.ServerAddress;
            }
            set 
            {
                Settings.Default.ServerAddress = value;
            } 
        }
        public string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public string Database
        {
            get
            {
                return Settings.Default.Database;
            }
            set
            {
                Settings.Default.Database = value;
            }
        }
        public string User
        {
            get
            {
                return Settings.Default.User;
            }
            set
            {
                Settings.Default.User = value;
            }
        }
        public string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }


        public string Error { get; set; }


        public bool IsValid
        {
            get
            {
                return _isServerAddressValid && _isServerNameValid && _isDatabaseValid && _isUserValid && _isPasswordValid;
            }
        }


        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    
                    case nameof(ServerAddress):
                        if (string.IsNullOrWhiteSpace(ServerAddress))
                        {
                            Error = "Pole Adres serwera jest wymagane.";
                            _isServerAddressValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isServerAddressValid = true;
                        }
                        break;

                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Pole Nazwa serwera jest wymagane.";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isServerNameValid = true;
                        }
                        break;
                    case nameof(Database):
                        if (string.IsNullOrWhiteSpace(Database))
                        {
                            Error = "Pole Baza danych jest wymagane.";
                            _isDatabaseValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isDatabaseValid = true;
                        }
                        break;
                    case nameof(User):
                        if (string.IsNullOrWhiteSpace(User))
                        {
                            Error = "Pole Użytkownik jest wymagane.";
                            _isUserValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isUserValid = true;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Pole Hasło jest wymagane.";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            Error = String.Empty;
                            _isPasswordValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        
        
        public void SaveSettings()
        {
            Settings.Default.Save();
        }
    }
}
