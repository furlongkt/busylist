using Xamarin.Forms;
using System.Collections.Generic;

namespace BusyList.Utilities.Validation.Behaviors
{
    /// <summary>
    /// This class groups validation behaviors together so that they can be
    /// validated as a whole. For instance, you may want to validate fields in a data entry form
    /// The form may have a name, address, phonenumber...etc. In this case you would
    /// want to apply a standard validation behavior on the individual items (name, address and phone),
    /// but in order to have all the validations report back to say disable/enable
    /// a button, you can use the group validation behavior
    /// </summary>
    public class ValidationGroupBehavior: Behavior<View>
    {
        IList<ValidationBehavior> _validationBehaviors;
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create("IsValid",
                                    typeof(bool),
                                    typeof(ValidationGroupBehavior),
                                    false);

        public ValidationGroupBehavior()
        {
            _validationBehaviors = new List<ValidationBehavior>();
        }

        /// <summary>
        /// Add a validation behavior to the group
        /// </summary>
        /// <param name="validationBehavior">validation behavior</param>
        /// <seealso cref="ValidationBehavior"/>
        public void Add(ValidationBehavior validationBehavior)
        {
            _validationBehaviors.Add(validationBehavior);
        }

        /// <summary>
        /// Remove a validation behavior from the group
        /// </summary>
        /// <param name="validationBehavior">validation behavior</param>
        /// <seealso cref="ValidationBehavior"/>
        public void Remove(ValidationBehavior validationBehavior)
        {
            _validationBehaviors.Remove(validationBehavior);
        }

        /// <summary>
        /// Update group validation property based on current view values
        /// </summary>
        public void Update()
        {
            bool isValid = true;

            foreach (ValidationBehavior validationItem in _validationBehaviors)
            {
                isValid = isValid && validationItem.Validate();
            }

            IsValid = isValid;
        }

        /// <summary>
        /// This field indicates whether or not all validation behaviors in the group
        /// yeild a successful validation (true) or if one or more validation
        /// behaviors in the group yeild a failed validation (false)
        /// </summary>
        public bool IsValid
        {
            get
            {
                return (bool)GetValue(IsValidProperty);
            }
            set
            {
                SetValue(IsValidProperty, value);
            }
        }
    }
}
