using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusyList.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusyList.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListCell : ViewCell
    {
        /// <summary>
        /// Event handler invoked when the Edit button is clicked (context action)
        /// </summary>
        public event EventHandler<EventArgs> OnEdit;
        /// <summary>
        /// Event handler invoked when the delete button is clicked (context action)
        /// </summary>
        public event EventHandler<EventArgs> OnDelete;

        public static readonly BindableProperty TitleProperty =
           BindableProperty.Create("Title", typeof(string), typeof(TodoListCell), "Title");
        public static readonly BindableProperty SubtitleProperty =
            BindableProperty.Create("Subtitle", typeof(string), typeof(TodoListCell), "Subtitle");
        public static readonly BindableProperty ColorAsHexProperty =
            BindableProperty.Create("ColorAsHex", typeof(string), typeof(TodoListCell), "ColorAsHex");

        public TodoListCell()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Observable title of the list
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Observable subtitle of the list
        /// </summary>
        public string Subtitle
        {
            get { return (string)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }

        /// <summary>
        /// Observable indicator color of the list as a hex string
        /// </summary>
        public string ColorAsHex
        {
            get { return (string)GetValue(ColorAsHexProperty); }
            set { SetValue(ColorAsHexProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                if (string.IsNullOrEmpty(ColorAsHex))
                    lblColor.BackgroundColor = (Color)Application.Current.Resources["green"];
                else
                    lblColor.BackgroundColor = Color.FromHex(ColorAsHex);

                lblTitle.Text = Title;
                lblSubtitle.Text = Subtitle;
            }
        }

        /// <summary>
        /// Public handler to invoke <c>OnEdit</c> event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnEditHandler(object sender, EventArgs e)
        {
            OnEdit?.Invoke(sender, e);
        }

        /// <summary>
        /// Public handler to invoke <c>OnDelete</c>event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDeleteHandler(object sender, EventArgs e)
        {
            OnDelete?.Invoke(sender, e);
        }
    }
}
