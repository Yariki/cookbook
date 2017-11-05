using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Cookbook.Client.Module.Interfaces.MVVM;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace Cookbook.Client.Module.Core.MVVM
{
    public abstract class BSBaseViewModel : IBSBaseViewModel
    {
        protected bool Disposed = false;
        protected readonly IUnityContainer UnityContainer;
        protected readonly IEventAggregator EventAggregator;

        protected readonly Dictionary<string, object> _values = new Dictionary<string, object>();


        protected BSBaseViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSView view)
        {
            UnityContainer = unityContainer;
            EventAggregator = eventAggregator;
            View = view;
            SetDataContext();
        }

        #region IARMViewModel Members

        public event PropertyChangedEventHandler PropertyChanged;

        public IBSView View { get; protected set; }

        public string Title  => GetTitle();
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual bool Closing()
        {
            return false;
        }

        public virtual void Initialize()
        {
            
        }


        #endregion IARMViewModel Members

        
        private void SetDataContext()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => View.DataContext = this));
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        
        protected void OnPropertyChanged<T>(Expression<Func<T>> exp)
        {
            var memberExpression = exp.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("Expression is empty");
            OnPropertyChanged(memberExpression.Member.Name);
        }

       
        protected string GetPropertyName<T>(Expression<Func<T>> exp)
        {
            var memExpression = exp.Body as MemberExpression;
            if (memExpression == null)
                return string.Empty;
            return memExpression.Member.Name;
        }

       
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed && disposing)
            {
                if (_values != null)
                {
                    _values.Clear();
                }
            }
            Disposed = true;
        }

        protected T Get<T>(Expression<Func<T>> expression)
        {
            return Get<T>(this.GetPropertyName(expression), default(T));
        }

      
        protected T Get<T>(Expression<Func<T>> expression, T defaultValue)
        {
            return Get<T>(this.GetPropertyName(expression), defaultValue);
        }

        
        protected T Get<T>([CallerMemberName]string name = null)
        {
            return Get<T>(name, default(T));
        }

        
        protected virtual T Get<T>(string name, T defaultValue)
        {
            if (_values.ContainsKey(name))
            {
                return (T)_values[name];
            }
            return defaultValue;
        }

       
        protected void Set<T>(Expression<Func<T>> expression, T val)
        {
            var name = GetPropertyName(expression);
            Set<T>(val, name);
        }

        
        protected virtual void Set<T>(T val, [CallerMemberName]string name = null)
        {
            _values[name] = val;
            OnPropertyChanged(name);
        }

        protected virtual string GetTitle()
        {
            return "You miss to set title";
        }

    }
}