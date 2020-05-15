using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;

using System.Diagnostics;

namespace MiniTC.ViewModel
{
    using MiniTC.ViewModel.BaseClasses;

    class ViewModelMain : ViewModelBase
    {
        
        public ViewModelMain()
        {
            LeftPanel = new Panel();
            RightPanel = new Panel();
        }

        #region Właściwości

        public Panel LeftPanel { get; set; }
        public Panel RightPanel { get; set; }


        #endregion

        #region Metody

        void CopyFunc(object sender)
        {
            if (File.Exists(LeftPanel.Path) && Directory.Exists(RightPanel.Path))
            {
                string fileName = Path.GetFileNameWithoutExtension(LeftPanel.Path);
                string fileExt = Path.GetExtension(LeftPanel.Path);
                string dstPath = RightPanel.Path + "\\" + fileName + fileExt;

                for(int i=1; i< Int32.MaxValue; i++)
                {
                    if (File.Exists(dstPath))
                    {
                        dstPath = RightPanel.Path + "\\" + fileName + $"({i})" + fileExt;
                    }
                    else break;
                }
                File.Copy(LeftPanel.Path, dstPath);
                // Dodanie do widoku
                RightPanel.PanelContent.Add(dstPath);
            }
            else if (File.Exists(RightPanel.Path) && Directory.Exists(LeftPanel.Path))
            {
                string fileName = Path.GetFileNameWithoutExtension(RightPanel.Path);
                string fileExt = Path.GetExtension(RightPanel.Path);
                string dstPath = LeftPanel.Path + "\\" + fileName + fileExt;

                for (int i = 1; i < Int32.MaxValue; i++)
                {
                    if (File.Exists(dstPath))
                    {
                        dstPath = LeftPanel.Path + "\\" + fileName + $"({i})" + fileExt;
                    }
                    else break;
                }
                File.Copy(RightPanel.Path, dstPath);
                // Dodanie do widoku
                LeftPanel.PanelContent.Add(dstPath);
            }
        }

        #endregion

        #region Komendy
        private ICommand copy = null;

        public ICommand Copy
        {
            get
            {
                if (copy == null)
                {
                    copy = new RelayCommand(
                        CopyFunc,
                        arg => ( 
                        (File.Exists(LeftPanel.Path) && Directory.Exists(RightPanel.Path))
                        || (File.Exists(RightPanel.Path) && Directory.Exists(LeftPanel.Path)) 
                        )
                    );
                }
                return copy;
            }
        }

        #endregion
    }
}
