using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using System.Windows.Input;

namespace MiniTC.ViewModel
{
    using BaseClasses;
    class Panel : ViewModelBase
    {

        public Panel()
        {
            // Załaduj dyski
            Drives = Directory.GetLogicalDrives();
            // Ustaw dyski na pierwszą pozycje
            DriveSelected = Drives[0];
            // Załaduj foldery i pliki
            PanelContent = new ObservableCollection<string>();
            LoadContent(DriveSelected);

            Path = DriveSelected;
        }

        #region Właściwości
        
        // Drives
        public string[] Drives { get; set; }

        // Content
        public ObservableCollection<string> PanelContent { get; set; }


        // Path
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; onPropertyChanged(nameof(Path)); }
        }

        // ComboBox SelectedItem
        private string driveSelected;
        public string DriveSelected
        {
            get { return driveSelected; }
            set { driveSelected = value; onPropertyChanged(nameof(DriveSelected)); }
        }

        // ListBox SelectedItem
        private string listSelection;
        public string ListSelection
        {
            get { return listSelection; }
            set { listSelection = value; onPropertyChanged(nameof(ListSelection)); }
        }
        #endregion

        #region Metody

        private void LoadContent(string path)
        {
            PanelContent.Clear();
            // Dodaj cofanie jeśli ścieżka inna niż drive
            if (path != Drives[0] && path != Drives[1])
            {
                PanelContent.Add("..");
            }

            string[] dirs = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            foreach (var dir in dirs)
            {
                PanelContent.Add("<D>"+dir);
            }
            foreach (var file in files)
            {
                PanelContent.Add(file);
            }
        }

        void SetDefaultPath(object sender)
        {
            Path = DriveSelected;
            LoadContent(DriveSelected);
        }

        void ChangePath(object sender)
        {
            if (ListSelection != null)
            {
                if (ListSelection.Equals(".."))
                {
                    FileAttributes tmp = File.GetAttributes(Path);
                    DirectoryInfo parent = Directory.GetParent(Path);
                    // Jeśli folder
                    if (tmp.HasFlag(FileAttributes.Directory))
                    {
                        Path = parent.FullName;
                        LoadContent(Path);
                    }
                    else
                    {
                        parent = Directory.GetParent(parent.FullName);
                        Path = parent.FullName;
                        LoadContent(Path);
                    }

                }
                else if (ListSelection[0] == '<')
                {
                    Path = ListSelection.Substring(3);
                    LoadContent(Path);
                }
                else
                {
                    Path = ListSelection;
                }
            }

        }

        #endregion

        #region Komendy
        private ICommand driveLoad = null;
        public ICommand DriveLoad
        {
            get
            {
                if (driveLoad == null)
                {
                    driveLoad = new RelayCommand(SetDefaultPath, arg => true);
                }
                return driveLoad;
            }
        }


        private ICommand contentLoad = null;
        public ICommand ContentLoad
        {
            get
            {
                if (contentLoad == null)
                {
                    contentLoad = new RelayCommand(ChangePath, arg => true);
                }
                return contentLoad;
            }
        }

        #endregion
    }
}
