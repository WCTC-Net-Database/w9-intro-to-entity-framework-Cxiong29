﻿using Microsoft.EntityFrameworkCore;
using W9_assignment_template.Data;
using W9_assignment_template.Models;

namespace W9_assignment_template.Services;

public class GameEngine
{
    private readonly GameContext _context;

    public GameEngine(GameContext context)
    {
        _context = context;
    }

    public void DisplayRooms()
    {
        var rooms = _context.Rooms.Include(r => r.Characters).ToList();

        foreach (var room in rooms)
        {
            Console.WriteLine($"Room: {room.Name} - {room.Description}");
            foreach (var character in room.Characters)
            {
                Console.WriteLine($"    Character: {character.Name}, Level: {character.Level}");
            }
        }
    }

    public void DisplayCharacters()
    {
        var characters = _context.Characters.ToList();
        if (characters.Any())
        {
            Console.WriteLine("\nCharacters:");
            foreach (var character in characters)
            {
                Console.WriteLine($"Character ID: {character.Id}, Name: {character.Name}, Level: {character.Level}, Room ID: {character.RoomId}");
            }
        }
        else
        {
            Console.WriteLine("No characters available.");
        }
    }
    public void AddRoom()
    {
        Console.Write("Enter room name: ");
        var name = Console.ReadLine();

        Console.Write("Enter room description: ");
        var description = Console.ReadLine();

        var room = new Room
        {
            Name = name,
            Description = description
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();

        Console.WriteLine($"Room '{name}' added to the game.");
    }
    public void Add_A_Character() //Not sure if this is correct, but maybe it is??? Look into this some more.  
    {
        Console.Write("Enter character name: ");
        var name = Console.ReadLine();

        Console.Write("Enter character level: ");
        var level = int.Parse(Console.ReadLine());

        Console.Write("Enter room ID for the character: ");
        var roomId = int.Parse(Console.ReadLine());

        var character = new Character
        {
            Name = name,
            Level = level,
            RoomId = roomId
        };

        _context.Characters.Add(character);
        _context.SaveChanges();

        Console.WriteLine($"Character '{name}' added.");
    }

    public void FindCharacter(string characterName) //Hmmmm....can't seem to run this after this was written.  Look into why it's not running.
    {
        Console.Write("Enter character name to search: ");
        var name = Console.ReadLine();

        var character = _context.Characters.FirstOrDefault(c => c.Name == characterName); // Maybe try something different????

        if (character != null)
        {
            Console.WriteLine($"Character Found: {character.Name}, {character.Level}");
        }
        else
        {
            Console.WriteLine("Cannot find Character.");
        }
    }

}