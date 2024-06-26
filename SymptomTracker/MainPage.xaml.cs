using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Linq;

namespace SymptomTracker;

public partial class MainPage : ContentPage
{

    public ObservableCollection<Symptom> Symptoms { get; set; }

    public MainPage()
    {
        InitializeComponent();

        Symptoms = FetchIncidentsFromDB();
        BindingContext = this;
        lv.ItemsSource = Symptoms;
        //DB.ClearDatabase();

    }

    private ObservableCollection<Symptom> FetchIncidentsFromDB()
    {
        var incidentsList = DB.conn.Table<Symptom>().ToList();
        return new ObservableCollection<Symptom>(incidentsList);
    }

    private void OnSortChanged(object sender, CheckedChangedEventArgs e)
    {
        var radioButton = sender as RadioButton;
        var selectedValue = radioButton.Value as string;

        if (selectedValue == "date")
        {
            var sorted = Symptoms.OrderBy(i => i.Date).ToList();
            Symptoms.Clear();
            foreach (var item in sorted) Symptoms.Add(item);
        }
        else if (selectedValue == "intensity")
        {
            var sorted = Symptoms.OrderByDescending(i => i.Intensity).ToList();
            Symptoms.Clear();
            foreach (var item in sorted) Symptoms.Add(item);
        }
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedIncident = e.SelectedItem as Symptom;

        if (e.SelectedItem == null)
            return;

        var action = await DisplayActionSheet("Choose an action", "Cancel", null, "Modify", "Delete");

        switch (action)
        {
            case "Modify":
                await Navigation.PushAsync(new ModifyRecord(selectedIncident));
                break;

            case "Delete":
                bool userAccepts = await DisplayAlert("Confirm", "Do you wish to delete this record?", "Yes", "No");
                if (userAccepts)
                {
                    var selectedItem = e.SelectedItem as Symptom;
                    Symptoms.Remove(selectedItem);

                    DB.conn.Delete(selectedItem);
                }
                break;

        }

    ((ListView)sender).SelectedItem = null;
    }


    private void OnAddClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddRecord());
    }

    public void RefreshList()
    {
        Symptoms = FetchIncidentsFromDB();
        BindingContext = this;
        lv.ItemsSource = Symptoms;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshList();
    }

}
