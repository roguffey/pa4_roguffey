using System;
using System.Collections.Generic;
using api.Interfaces;
using api.models;

namespace api.Utilities
{
    public class Menu
    {
        public List<Song> Playlist;
        public ISongUtilities SongUtilities;
        public int DisplayMainMenu() {
            string userInputString;
            bool validInput = false;

            do { // attempted extra: error handling when the user enters an invalid input
                Console.Clear();
                Console.WriteLine("Welcome to the main menu. Please choose from the following:\n");
                Console.WriteLine("1) Show All Songs");
                Console.WriteLine("2) Add a Song");
                Console.WriteLine("3) Delete a Song");
                Console.WriteLine("4) Edit a Song");
                Console.WriteLine("5) Exit");

                userInputString = Console.ReadLine();
                validInput = CheckValidInput(userInputString, 5);
                
            } while(!validInput);

            return int.Parse(userInputString);
        }
        public bool RouteMainMenu(int userChoice) {
            SongUtilities.playlist = this.Playlist;
            switch (userChoice) {
                case 1: // Print Playlist
                    SongUtilities.PrintPlaylist();
                    PressKeyToReturn();
                    return true;
                case 2: // Add Song to Playlist
                    SongUtilities.AddSong();
                    PressKeyToReturn();
                    return true;
                case 3: // Delete Song from Playlist
                    SongUtilities.DeleteSong();
                    PressKeyToReturn();
                    return true;
                case 4:  // Edit Song in Playlist
                    SongUtilities.EditSong();
                    PressKeyToReturn();
                    return true;
                case 5: // Exit Program
                    DisplayQuitMessage();
                    return false;
                default:
                    Console.WriteLine("Sorry, incorrect input. Goodbye.");
                    return false;
            }
        }
        public bool CheckValidInput(string userInput, int maxOption) { // input must be between 1 and the max option
            int parsedInput;

            if (!int.TryParse(userInput, out parsedInput)) { // user input is not an integer
                DisplayInvalidInputMessage();

                return false;
            }
            else {
                
                if (parsedInput >= 1 && parsedInput <= maxOption) { // user input is valid
                    return true;
                }

                DisplayInvalidInputMessage();
                return false;
            }
            
        }

        public void DisplayInvalidInputMessage() {
            Console.WriteLine("Invalid input. Press any key to return and try again.");
            Console.ReadKey();
        }

        public void DisplayQuitMessage() {
            Console.Clear();
            Console.WriteLine("Goodbye.");
        }

        public void PressKeyToReturn() {
            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
        }
    }
}