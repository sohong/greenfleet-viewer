////////////////////////////////////////////////////////////////////////////////
// ImportCommand.cs
// 2012.03.19, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Viewer.Personal.Command
{
    public abstract class SimpleCommand : ICommand
    {
        #region ICommand

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        #endregion // ICommand


        #region methods

        /*
         이 커맨드에 바인딩하는 컨트롤(버튼 등)들이 이벤트에 subscribe하는 
         시점을 viewmodel 쪽에서 잡아낼 수 없다. 즉, subscribe한 후 바로 이 함수를 호출하고
         싶은데 못하고 있다(viewmodel이 view의 datacontext로 설정되는 초기 시점(등)에 command parameter를
         넘기지 못하고 있다). 위에서 commandManager 메카니즘을 이용한다.
        public void RaiseCanExeuteChanged() {
            EventHandler eh = CanExecuteChanged;
            if (eh != null) {
                 eh(this, EventArgs.Empty);
            }
        }
         */

        #endregion // methods
    }
}
