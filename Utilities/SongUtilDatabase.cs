using System;
using System.IO;
using System.Collections.Generic;
using api.models;
using api.FileHandleing;
using api.Interfaces;
using api.database;
using MySql;
using MySql.Data.MySqlClient;


namespace api.Utilities
{
    public class SongUtilDatabase : ISongUtilities
    {
        public List<Song> playlist { get; set; }
         public void PrintPlaylist() { // display all items in the playlist to the console
            Console.Clear();
            CreateSong.CreateSongTable(); // Call to create table if one has yet to be created
            IReadSongs readSongs = new ReadSongData(); 
            List<Song> allSongs = readSongs.GetAll();
            allSongs.Sort();
            foreach (Song song in allSongs) { // for every song in the playlist, write the song's ToString to the console
                Console.WriteLine(song.ToString());
            }
            Console.WriteLine();
            WriteToFile.WriteAllToFile(allSongs);
        }

        public void AddSong() { // allow the user to add a new song to the playlist
            Console.Clear();
            CreateSong.CreateSongTable(); // Call to create table if one has yet to be created
            IReadSongs readSongs = new ReadSongData();
            List<Song> allSongs = readSongs.GetAll();
            int newID;

            allSongs.Reverse();
                
            newID = allSongs[0].SongID + 1;
            
            Song mysong = new Song(){SongID = newID, SongTitle = PromptSongDetails(), SongTimestamp = DateTime.Now, Deleted = "n"};    
            WriteToFile.WriteAllToFile(allSongs); // write updated playlist to the songs.txt file    
            mysong.Create.Create(mysong);
        }

        public string PromptSongDetails() { // Ask user for title of the song to add
            Console.Clear();
            Console.WriteLine("What is the title of your song?");
            return Console.ReadLine();
        }

        public void DeleteSong() { // remove a song from the playlist given the SongID
            Console.Clear();
            IReadSongs readSongs = new ReadSongData();
            List<Song> playlist = readSongs.GetAll();
            int index;
            int IDToDelete;
            do {
                // find index
                IDToDelete = PromptSongToDelete(playlist);
            
                index = playlist.FindIndex(currentSong => currentSong.SongID == IDToDelete); // iterate through songs in playlist and compare their IDs to the ID the user wants to delete 

            } while (!CheckValidIndex(index)); // make sure the song ID exists - if playlist.FindIndex returns -1, the ID was not found in the list


            // remove song at index found
            string titleToDelete = playlist[index].SongTitle;
            playlist.RemoveAt(index);
            
            Song song = new Song(){SongID = index};
            song.Delete.Delete(IDToDelete);
            
            Console.Clear();
            Console.WriteLine("{0} has been removed.", titleToDelete);
            
        }

        public int PromptSongToDelete(List<Song> playlist) { // ask the user the ID of the song they want to delete
            
            string userInput;
            
            do {

                Console.Clear();
                PrintPlaylist();
                Console.WriteLine("What is the ID of the song you want to delete?");
                userInput = Console.ReadLine();

            } while (!CheckValidInput(userInput)); // ID entered must be an integer
            
            return int.Parse(userInput);
        }

        public bool CheckValidIndex(int index) {
            if (index == -1) { // if playlist.FindAt returns -1, the ID was not found in the list
                Console.WriteLine("\nID does not exist in the current playlist. Press any key to continue");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        public bool CheckValidInput(string userInput) { // check to see if user's input is an integer
            int parsedInput;

            if (!int.TryParse(userInput, out parsedInput)) {
                Console.WriteLine("Invalid input. Try again.");
                Console.ReadKey();
                return false;
            }

            return true;
        }

        public void EditSong()
        {
            Console.Clear();
            IReadSongs readSongs = new ReadSongData();
            List<Song> allSongs = readSongs.GetAll();
            int index;
            
            int IDToEdit;
            do {
                // find index
                Console.WriteLine("Plese enter the ID of the Song Title you would like to edit");
                IDToEdit = PromptSongToEdit(playlist);
            
                index = playlist.FindIndex(currentSong => currentSong.SongID == IDToEdit); // iterate through songs in playlist and compare their IDs to the ID the user wants to edit 

            } while (!CheckValidIndex(IDToEdit)); // make sure the song ID exists - if playlist.FindIndex returns -1, the ID was not found in the list


            // remove song at index found
            string titleToEdit = playlist[index].SongTitle;
            Console.WriteLine("What would you like to change the Title to?");
            
            Song song = new Song(){SongID = IDToEdit, SongTitle = Console.ReadLine(), SongTimestamp = playlist[index].SongTimestamp};
            song.Update.Update(song);
            
            Console.Clear();
            Console.WriteLine("{0} has been Updated.", titleToEdit);
        }

        public int PromptSongToEdit(List<Song> playlist)
        { // ask the user the ID of the song they want to Edit
            
            string userInput;
            
            do {

                Console.Clear();
                PrintPlaylist();
                Console.WriteLine("What is the ID of the song you want to Edit?");
                userInput = Console.ReadLine();

            } while (!CheckValidInput(userInput)); // ID entered must be an integer
            
            return int.Parse(userInput);
        }
    }
}