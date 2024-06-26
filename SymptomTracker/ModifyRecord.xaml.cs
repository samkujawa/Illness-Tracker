using Microsoft.Maui.Controls;
using System;

namespace SymptomTracker
{
    public partial class ModifyRecord : ContentPage
    {
        private Symptom _currentSymptom;

        public ModifyRecord(Symptom symptom)
        {
            InitializeComponent();
            _currentSymptom = symptom;
            DateInput.Date = symptom.Date;
            TimeInput.Time = symptom.Time;

            intensityInput.SelectedItem = symptom.Intensity;

            NotesInput.Text = symptom.Notes;
        }

        private void OnUpdateClicked(object sender, EventArgs e)
        {
            if (intensityInput.SelectedItem == null)
            {
                return;
            }

            _currentSymptom.Date = DateInput.Date;
            _currentSymptom.Time = TimeInput.Time;
            _currentSymptom.Intensity = (int)intensityInput.SelectedItem;
            _currentSymptom.Notes = NotesInput.Text;

            // Update database
            DB.conn.Update(_currentSymptom);

            Navigation.PopAsync();
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Warning", "Do you wish to discard this?", "Yes", "No");

            if (result) await Navigation.PopAsync(); 
        }
    }
}
