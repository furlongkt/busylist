using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BusyList.Utilities.Validation.Behaviors
{
    /// <summary>
    /// This class allows a validation rule or a set of validation rules to be
    /// applied to a view as a behavior.
    /// </summary>
    public class ValidationBehavior : Behavior<View>
    {
        IErrorStyle _style = new BasicErrorStyle();
        View _view;
        /// <summary>
        /// Name of the property being validated
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Group this validation belongs to (null if not associated with a group)
        /// </summary>
        public ValidationGroupBehavior Group { get; set; }
        /// <summary>
        /// Rule set
        /// </summary>
        public ObservableCollection<IValidator> Validators { get; set; } = new ObservableCollection<IValidator>();

        /// <summary>
        /// Validate the view
        ///
        /// This method will validate the view based on the ruleset. If any of
        /// the rules fails to validate the error style will be applied.
        ///
        /// This is called on property change
        /// </summary>
        /// <returns>boolean indicating valid (true) or invalid (false)</returns>
        /// <seealso cref="BasicErrorStyle"/>
        /// <seealso cref="ValidationGroupBehavior"/>
        public bool Validate()
        {
            bool isValid = true;
            string errorMessage = "";

            foreach (IValidator validator in Validators)
            {
                bool result = validator.Check(_view.GetType()
                                       .GetProperty(PropertyName)
                                       .GetValue(_view) as string);

                isValid = isValid && result;

                if (!result)
                {
                    errorMessage = validator.Message;
                    break;
                }
            }

            if (!isValid)
            {
                _style.ShowError(_view, errorMessage);
                return false;
            }
            else
            {
                _style.RemoveError(_view);
                return true;
            }

        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            _view = bindable as View;
            _view.PropertyChanged += OnPropertyChanged;
            _view.Unfocused += OnUnFocused;

            if (Group != null)
            {
                Group.Add(this);
            }
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            base.OnDetachingFrom(bindable);

            _view.PropertyChanged -= OnPropertyChanged;
            _view.Unfocused -= OnUnFocused;

            if (Group != null)
            {
                Group.Remove(this);
            }
        }

        void OnUnFocused(object sender, FocusEventArgs e)
        {
            Validate();

            //if (Group != null)
            //{
            //    Group.Update();
            //}
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == PropertyName)
            {
                Validate();
                if (Group != null)
                {
                    Group.Update();
                }
            }
        }
    }
}
