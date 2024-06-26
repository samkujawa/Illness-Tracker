using Microsoft.Maui.Controls;
using System;

namespace SymptomTracker
{
    public partial class AddRecord : ContentPage
    {
        public AddRecord()
        {
            InitializeComponent();
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            if (intensityInput.SelectedItem == null)
            {
                return;
            }

            var newSymptom = new Symptom
            {
                Date = DateInput.Date,
                Time = TimeInput.Time,
                Intensity = (int)intensityInput.SelectedItem,
                Notes = NotesInput.Text
            };

            // Add to database
            DB.conn.Insert(newSymptom);

            Navigation.PopAsync();
        }

        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
