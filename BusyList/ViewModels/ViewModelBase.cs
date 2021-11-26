using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BusyList.Utilities;
using Xamarin.Forms;

namespace BusyList.ViewModels
{
    /// <summary>
    /// Base class for all view models
    ///
    /// This class simplifies boilerplate code to create commands and can execute command methods,
    /// notifying of property changes and can execute commands changed
    ///
    /// The extension classes should do the following:
    /// <example>
    /// To implement an observable property:
    /// <code>
    /// public string MyProperty
    /// {
    ///		get=>GetValue<string>();
    ///		set=>SetValue(value);
    ///	}
    /// </code>
    /// </example>
    ///
    ///
    /// <example>
    /// To implement a command simply prefix a method with "Execute" and its "CanExecute" counterpart:
    /// <code>
    /// public async void ExecuteMyCommand(object state)
    /// {
    ///		//Do something cool
    ///	}
    ///	...
    ///	public bool CanExecuteMyCommand(object state)
    ///	{
    ///	    bool ThisBoolIndicatesICanDoSomethingCool = true;
    ///	    return ThisBoolIndicatesICanDoSomethingCool;
    ///	}
    /// </code>
    /// </example>
    ///
    /// The <c>CanExecute</c> method is optional and will default to always true if no method is provided.
    ///
    /// <example>
    /// To use a command via binding:
    /// <code>
    /// <Button ... Command="{Binding [MyCommand]}" ... />
    /// </code>
    /// </example>
    ///
    /// <example>
    /// To access the command via code behind:
    /// <code>
    /// ["MyCommand"].ChangeCanExecute();
    /// </code>
    /// </example>
    /// </summary>
    public abstract class ViewModelBase : ObservableProperty
    {
        private readonly Dictionary<string, object> properties = new Dictionary<string, object>();
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        private const string EXECUTECOMMAND_PREFIX = "Execute_";
        private const string CANEXECUTECOMMAND_PREFIX = "CanExecute_";

        public ViewModelBase()
        {
            this.commands = this.GetType().GetTypeInfo().DeclaredMethods.Where(dm => dm.Name.StartsWith(EXECUTECOMMAND_PREFIX)).ToDictionary(k => GetCommandName(k), v => GetCommand(v));
        }

        /// <summary>
        /// Initialize data into the view model
        /// </summary>
        /// <param name="navigationParameter">Parameter passed in by the NavigationService</param>
        /// <returns></returns>
        /// <seealso cref="BusyList.Navigation.NavigationService"/>
        public abstract Task InitializeAsync(object navigationParameter);

        /// <summary>
        /// Sets a property value, fires OnPropertyChanged event, and notifies CanExecute commands of a change
        /// </summary>
        /// <typeparam name="T">value's type</typeparam>
        /// <param name="value">value</param>
        /// <param name="propertyName">name of the property, defaults to the code name of the property</param>
        protected void SetValue<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (!properties.ContainsKey(propertyName))
            {
                properties.Add(propertyName, default(T));
            }

            var oldValue = GetValue<T>(propertyName);
            if (!EqualityComparer<T>.Default.Equals(oldValue, value))
            {
                properties[propertyName] = value;
                OnPropertyChanged(propertyName);
                NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// Get property value
        /// </summary>
        /// <typeparam name="T">value's type</typeparam>
        /// <param name="propertyName">name of property, defaults to the code name of the propert</param>
        /// <returns></returns>
        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (!properties.ContainsKey(propertyName))
            {
                return default(T);
            }
            else
            {
                return (T)properties[propertyName];
            }
        }

        /// <summary>
        /// Given a method, extract the root name of the command without the fluff of the prefix.
        /// For example: <c>Execute_Save</c> will return <c>Save</c>
        /// </summary>
        /// <param name="mi">method information</param>
        /// <returns>name of command</returns>
        private string GetCommandName(MethodInfo mi)
        {
            return mi.Name.Replace(EXECUTECOMMAND_PREFIX, "");
        }

        /// <summary>
        /// Creates a new command based on the reflection of the method signature
        /// </summary>
        /// <param name="mi">method</param>
        /// <returns>Bindable ICommand implementation</returns>
        /// <see cref="ICommand"/>
        private ICommand GetCommand(MethodInfo mi)
        {
            var canExecute = this.GetType().GetTypeInfo().GetDeclaredMethod(CANEXECUTECOMMAND_PREFIX + GetCommandName(mi));
            var executeAction = (Action<object>)mi.CreateDelegate(typeof(Action<object>), this);
            var canExecuteAction = canExecute != null ? (Func<object, bool>)canExecute.CreateDelegate(typeof(Func<object, bool>), this) : state => true;
            return new Command(executeAction, canExecuteAction);
        }

        /// <summary>
        /// Fire a <c>CanExecuteChanged</c> event.
        ///
        /// If a command name is given (without prefix), the event will only fire for that particulat command
        /// If no command name is given, the event will fire for all commands in this view model
        /// </summary>
        /// <param name="name">root name of command</param>
        private void NotifyCanExecuteChanged(string name = null)
        {
            foreach (var cmd in commands)
            {
                if (name == null || cmd.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    ((Command)cmd.Value).ChangeCanExecute();
            }
        }

        /// <summary>
        /// Field used to simplify access to auto-generated commands
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ICommand this[string name]
        {
            get
            {
                return commands[name];
            }
        }
    }
}