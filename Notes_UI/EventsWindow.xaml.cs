using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Notes_UI
{
    public partial class EventsWindow : Window
    {
        public Event111 SelectedEvent { get; set; }

        public EventsWindow()
        {
            InitializeComponent();
            DataContext = this;

            RefreshEvents();
        }

        private async void RefreshEvents()
        {
            try
            {
                List<Event111> events = await ApiClientEvent.GetAllEvents();
                eventsListBox.ItemsSource = events;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EventsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEvent = (Event111)eventsListBox.SelectedItem;
            if (SelectedEvent != null)
            {
                titleTextBox.Text = SelectedEvent.Title;
                descriptionTextBox.Text = SelectedEvent.Description;
                startDatePicker.SelectedDate = SelectedEvent.StartDate;
                endDatePicker.SelectedDate = SelectedEvent.EndDate;

                updateButton.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                updateButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            Event111 newEvent = new Event111
            {
                Title = titleTextBox.Text,
                Description = descriptionTextBox.Text,
                StartDate = startDatePicker.SelectedDate ?? DateTime.Now,
                EndDate = endDatePicker.SelectedDate ?? DateTime.Now,
                IsVisible = true
            };

            try
            {
                Event111 addedEvent = await ApiClientEvent.AddEvent(newEvent);
                MessageBox.Show($"Event added with ID: {addedEvent.Id}", "Event Added", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateEvent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEvent != null)
            {
                SelectedEvent.Title = titleTextBox.Text;
                SelectedEvent.Description = descriptionTextBox.Text;
                SelectedEvent.StartDate = startDatePicker.SelectedDate ?? DateTime.Now;
                SelectedEvent.EndDate = endDatePicker.SelectedDate ?? DateTime.Now;

                try
                {
                    Event111 updatedEvent = await ApiClientEvent.UpdateEvent(SelectedEvent.Id, SelectedEvent);
                    MessageBox.Show($"Event updated with ID: {updatedEvent.Id}", "Event Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEvent != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this event?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await ApiClientEvent.DeleteEvent(SelectedEvent.Id);
                        MessageBox.Show($"Event deleted with ID: {SelectedEvent.Id}", "Event Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        RefreshEvents();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}

