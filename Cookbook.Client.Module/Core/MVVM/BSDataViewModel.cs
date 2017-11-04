using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Cookbook.Client.Module.Core.Extensions;
using Cookbook.Client.Module.Interfaces.MVVM;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Cookbook.Client.Module.Core.MVVM
{
    public abstract  class BSDataViewModel : BSBaseViewModel, IBSDataViewModel
    {
        private object _dataObject;

        protected BSDataViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSView view)
                : base(unityContainer, eventAggregator, view)
        {
            SaveCommand = new BSRelayCommand(SaveExecute, CanSaveExecte);
            HasChanges = false;
        }
        
        public virtual void SetBusinessObject(ViewMode mode, object data)
        {
            Mode = mode;
            _dataObject = data;
        }

        
        public ViewMode Mode { get; private set; }
        public bool HasChanges { get; set; }

        public  bool Closing()
        {
            if (HasChanges)
            {
                var result = MessageBox.Show("Do you want to save changes?", "Save", MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveExecute(null);
                }
            }
            return false;
        }


        /// <returns></returns>
        protected override T Get<T>(string name, T defaultValue)
        {
            if (_dataObject != null && HasProperty(name))
            {
                var pi = _dataObject.GetType().GetPublicProperty(name);
                var val = pi != null ? pi.GetPropertyValue<T>(_dataObject) : defaultValue;
                return val;
            }
            else
                return base.Get<T>(name, defaultValue);
        }

        
        protected override void Set<T>(string name, T val)
        {
            if (_dataObject != null && HasProperty(name))
            {
                var pi = _dataObject.GetType().GetPublicProperty(name);
                if (pi != null)
                {
                    pi.SetPropertyValue(_dataObject, val);
                    HasChanges = true;
                }
                OnPropertyChanged(name);
            }
            else
                base.Set(name, val);
        }

        
        public TObj GetBusinessObject<TObj>()
        {
            return (TObj)_dataObject;
        }
       
        protected bool HasProperty(string name)
        {
            return _dataObject.GetType().HasProperty(name);
        }

       
        protected PropertyInfo GetPropertyInfo(string name)
        {
            return _dataObject.GetType().GetPublicProperty(name);
        }

        
        
        protected virtual bool CanSaveExecte(object arg)
        {
            return true;
        }

       
        protected virtual void SaveExecute(object arg)
        {
            HasChanges = false;
        }

       
        public object DataObject
        {
            get { return _dataObject; }
        }

        #region [commands]

        public ICommand SaveCommand { get; private set; }

        #endregion

        #region [dispose]

        protected override void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                _dataObject = null;
            }
            base.Dispose(disposing);
        }

        #endregion


        #region [private]

        #endregion


    }
}