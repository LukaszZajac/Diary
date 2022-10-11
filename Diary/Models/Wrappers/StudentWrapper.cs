using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Wrappers
{
    public class StudentWrapper : IDataErrorInfo
    {
        public StudentWrapper()
        {
            //trzeba tak zrobić bo wtedy id będzie 0, a jak tego nie zrobimy to będzie nullem i będzie waliło błędm
            Group = new GroupWrapper();
        }



        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
        public bool Activities { get; set; }
        public GroupWrapper Group { get; set; }


        private bool _isFirstNameValid;
        private bool _isLastNameValid;

        //ustawiamy warunki jakie mają zostać spełnione dla konkretnej kolumny
        public string this[string columnName]
        {
            get 
            {
                switch (columnName)
                {
                    //jeżeli została zmieniona kolumna FirstName; zapis "case nameof(FirstName):" jest równoznaczny z takim zapisem: "case "FirstName""
                    case nameof(FirstName):
                        //jeżeli imię jest puste
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole Imię jest wymagane.";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            //jeśli jest ok, wyczyść pole Error
                            Error = String.Empty;
                            _isFirstNameValid = true;
                        }
                        break;

                    case nameof(LastName):
                        //jeżeli nazwisko jest puste
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole Nazwisko jest wymagane.";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            //jeśli jest ok, wyczyść pole
                            Error = String.Empty;
                            _isLastNameValid = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        public string Error { get; set; }

        //Sprawdzenie przed kliknięciem przycisku Zapisz czy walidacja została spełniona
        public bool IsValid 
        {
            get
            {
                return _isFirstNameValid && _isLastNameValid && Group.IsValid;   
            }
        }
    }
}
