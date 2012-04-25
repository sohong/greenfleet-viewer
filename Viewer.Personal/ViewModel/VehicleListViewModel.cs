////////////////////////////////////////////////////////////////////////////////
// VehicleListViewModel.cs
// 2012.03.16, created by sohong
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
using Viewer.Common.ViewModel;
using System.ComponentModel;
using System.Windows.Data;
using Viewer.Personal.Model;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common.Util;
using Viewer.Common.Service;
using Viewer.Personal.View;

namespace Viewer.Personal.ViewModel
{
    /// <summary>
    /// View model for VehicleListView.
    /// </summary>
    public class VehicleListViewModel : DialogViewModel
    {
        #region constructors

        public VehicleListViewModel()
        {
            this.Vehicles = new ListCollectionView(PersonalDomain.Domain.Vehicles);
            this.Vehicles.CurrentChanged += new EventHandler(Vehicles_CurrentChanged);

            this.AddCommand = new DelegateCommand<object>(DoAdd, CanAdd);
            this.EditCommand = new DelegateCommand<object>(DoEdit, CanEdit);
            this.DeleteCommand = new DelegateCommand<object>(DoDelete, CanDelete);

            IsCancelable = true;
            CancelText = null; // cancel 버튼이 표시되지 않고 esc 키만 동작하도록.
            SubmitText = "닫기";
        }

        #endregion // constructors


        #region properties

        public ICollectionView Vehicles
        {
            get;
            private set;
        }

        public DelegateCommand<object> AddCommand
        {
            get;
            private set;
        }

        public DelegateCommand<object> EditCommand
        {
            get;
            private set;
        }

        public DelegateCommand<object> DeleteCommand
        {
            get;
            private set;
        }

        #endregion // properties


        #region internal methods

        private void Vehicles_CurrentChanged(object sender, EventArgs e)
        {
            Vehicle v = Vehicles.CurrentItem as Vehicle;
            CheckCommands();
        }

        private void CheckCommands()
        {
            AddCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }

        private void DoAdd(object data)
        {
            DialogService.Run("차량 추가", new VehicleView(), new VehicleViewModel(null), (obj) => {
                this.Vehicles.MoveCurrentTo(obj);
            });
        }

        private bool CanAdd(object data)
        {
            return true;
        }

        private void DoEdit(object data)
        {
            Vehicle current = Vehicles.CurrentItem as Vehicle;
            if (current != null) {
                DialogService.Run("차량 수정", new VehicleView(), new VehicleViewModel(current), (obj) => {
                });
            }
        }

        private bool CanEdit(object data)
        {
            return Vehicles.CurrentItem != null;
        }

        private void DoDelete(object data)
        {
            Vehicle current = Vehicles.CurrentItem as Vehicle;
            if (current != null) {
                if (MessageUtil.Conform("차량 삭제", "선택한 차량 정보를 삭제하시겠습니까?")) {
                    PersonalDomain.Domain.Vehicles.Remove(current);
                }
            }
        }

        private bool CanDelete(object data)
        {
            return Vehicles.CurrentItem != null;
        }

        #endregion // internal methods


        #region overriden methods

        protected override bool CanSubmit()
        {
            return true;
        }

        #endregion // overriden methods
    }
}
