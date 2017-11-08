using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Cookbook.Client.Module.Core.Events;
using Cookbook.Client.Module.Core.Extensions;
using Cookbook.Client.Module.Interfaces.MVVM;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Cookbook.Client.Module.Core.MVVM
{
    public abstract  class BSDataViewModel : BSBaseViewModel, IBSDataViewModel
    {
        private object dataObject;

        protected BSDataViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSView view)
                : base(unityContainer, eventAggregator, view)
        {
            SaveCommand = new BSRelayCommand(SaveExecute, CanSaveExecte);
            CancelCommand = new BSRelayCommand(o => EventAggregator.GetEvent<BSCloseRecipeEvent>().Publish(this));
            HasChanges = false;
        }
        
        public virtual void SetBusinessObject(ViewMode mode, object data)
        {
            Mode = mode;
            dataObject = data;
        }

        
        public ViewMode Mode { get; private set; }
        public bool HasChanges { get; set; }

        public override bool Closing()
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
            if (dataObject != null && HasProperty(name))
            {
                var pi = dataObject.GetType().GetPublicProperty(name);
                var val = pi != null ? pi.GetPropertyValue<T>(dataObject) : defaultValue;
                return val;
            }
            else
                return base.Get<T>(name, defaultValue);
        }

        
        protected override void Set<T>(T val, [CallerMemberName] string name = null)
        {
            if (dataObject != null && HasProperty(name))
            {
                var pi = dataObject.GetType().GetPublicProperty(name);
                if (pi != null)
                {
                    pi.SetPropertyValue(dataObject, val);
                    HasChanges = true;
                }
                OnPropertyChanged(name);
            }
            else
                base.Set(val, name);
        }

        
        public TObj GetBusinessObject<TObj>()
        {
            return (TObj)dataObject;
        }
       
        protected bool HasProperty(string name)
        {
            return dataObject.GetType().HasProperty(name);
        }

       
        protected PropertyInfo GetPropertyInfo(string name)
        {
            return dataObject.GetType().GetPublicProperty(name);
        }
        
        protected virtual bool CanSaveExecte(object arg)
        {
            return HasChanges;
        }

       
        protected virtual void SaveExecute(object arg)
        {
            HasChanges = false;
        }

       
        public object DataObject
        {
            get { return dataObject; }
        }

        #region [commands]

        public ICommand SaveCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        #endregion

        #region [dispose]

        protected override void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                dataObject = null;
            }
            base.Dispose(disposing);
        }

        #endregion


        #region [private]

        #endregion


    }
}