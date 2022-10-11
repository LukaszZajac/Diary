using Diary.Commands;
using Diary.Models;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            //RelayCommand przyjmuje metodę, która ma zostać wywołana - "RefreshStudents" oraz metodę "CanRefreshStudents", która wskazuje czy ta poprzednia może zostać wywołana
            //możemy stworzyć obiekt relayCommand bo on implementuje ICommand
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            ChangeSettingsDbCommand = new RelayCommand(ChangeSettingsDb);
            LoadApplicationCommand = new AsyncRelayCommand(LoadApplication);

        }

        



        //właściwość ICommand dlatego że RelayCommand implementuje interfejs
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }

        public ICommand ChangeSettingsDbCommand { get; set; }
        public ICommand LoadApplicationCommand { get; set; }


        private StudentWrapper _selectedStudent;
        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        /*
         Poniżej w WPF do wiązania danych zamiast list (bo miała to być lista studentów) używa się ObservableCollection.
        Jest to po prostu zwykła lista ale implementuje jeszcze dwa dodatkowe intefejsy INotifyCollectionChanged oraz INotifyPropertyChanged
        dzięki którym DataGrid będzie informowany o tym gdy jakiś element zostanie dodany czy usunięty i będzie aktualizował widok.
         */
        private ObservableCollection<StudentWrapper> _students;
        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set 
            { 
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;
        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            //chcemy żeby oprócz tego co pobierze z db, wyświetlała się domyślnie grupa o nazwie "Wszystkie". Trzeba wskazać indeks i dane obiektu
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);
            
            SelectedGroupId = 0;
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        //sprawdzamy czy uczeń został zaznaczony i jeżeli tak to zwraca true
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia", 
                $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", 
                MessageDialogStyle.AffirmativeAndNegative);

            //jeżeli nie został kliknięty przycisk ok to od razu wychodzimy z tej metody i to co jest dalej nie będzie wykonywane
            if (dialog != MessageDialogResult.Affirmative)
            {
                return;
            }

            //usuwanie ucznia z bazy
            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }

        //private void EditStudent(object obj)
        //{
            
        //}

        //metoda otwierająca nowe okno
        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void ChangeSettingsDb(object obj)
        {
            var changeSettingsDbWindow = new ChangeSettingsDbView();
            changeSettingsDbWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));         
        }

        private bool ConnectToDatabase()
        {
            try
            {
                using(var contextDb = new ApplicationDbContext())
                {
                    contextDb.Database.Connection.Open();
                    contextDb.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task LoadApplication(object obj)
        {
            if (ConnectToDatabase())
            {
                RefreshDiary();
                InitGroups();
            }
            else
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync(
                    "Błędne dane do połączenia z bazą danych",
                    $"Czy chcesz podać prawidłowe dane dostępowe do bazy danych?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (dialog != MessageDialogResult.Affirmative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var changeSettingsDbView = new ChangeSettingsDbView();
                    changeSettingsDbView.ShowDialog();
                }
            }
        }

    }
}
