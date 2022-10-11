using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Wrappers
{
    public class GroupWrapper : IDataErrorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }


        //ustawiamy warunki jakie mają zostać spełnione dla konkretnej kolumny
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    //jeżeli została zmieniona kolumna FirstName; zapis "case nameof(FirstName):" jest równoznaczny z takim zapisem: "case "FirstName""
                    case nameof(Id):
                        //jeżeli imię jest puste
                        if (Id==0)
                        {
                            Error = "Pole Grupa jest wymagane.";
                        }
                        else
                        {
                            //wyczyść pole
                            Error = String.Empty;
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
                return string.IsNullOrWhiteSpace(Error);//czy właściwość Error jest pusta, zwracane jest true jeśli jest Pusta
            }
        }
    }
}
