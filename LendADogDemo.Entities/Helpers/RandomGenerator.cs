using LendADogDemo.Entities.DataContexts;
using LendADogDemo.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;


namespace LendADogDemo.Entities.Helpers
{
    public enum FirstOrLastName
    {
        firstName = 0,
        lastName = 1
    }

    public static class RandomGenerator
    {
        private static Random random = new Random();

        public static string GetRandomFirstOrLastName(FirstOrLastName firstOrLast)
        {
            List<string> listFullNames = new List<string>
            {
                "Lisette Gerhardt","Kandis Paredes","Carolee Ardis","Debbi Monti","John Chapman","Francine Rosalez",
                "Rosette Hawkin","Josie Poff","Nadene Arnette","Quinn Sharpe","Breanna Costigan","Reinaldo Tyra",
                "Stuart Easter","Georgiana Molina","Kira Pilkenton","Olevia Coogan","Cassaundra Breen","Rosana Towner",
                "Georgetta Edinger","Monique Bottomley","Nathanial Nemitz","Cheri Fender","Claire Deibert","Indira Flatt",
                "Sterling Kunz","Lakendra Sowers","Tandra Siguenza","Angelique Nott","Valrie Mcmiller","Amina Paulos",
                "Rosetta Cervone","Edgar Deharo","Jacinta Mays","Aldo Parsley","Nelda Oritz","Elroy Hallee","Kathryn Enochs",
                "Thi Clift","Arturo Vrba","Forrest Natali","Lorilee Herdt","Marry Cottrell","Manuela Musto","Sheena Finney",
                "Audie Penny","Ja Keogh","Margurite Linney","Ying Eble","Tory Hennessy","Somer Breed"
            };

            string randomFullName = listFullNames[random.Next(listFullNames.Count)];
            var firstOrLastName = randomFullName.Split(null)[(int)firstOrLast];

            return firstOrLastName;
        }

        public static string GetRandomPhoneNumer(int numberOfDigits)
        {
            char[] phoneLenght = new char[numberOfDigits];
            phoneLenght[0] = '0';
            phoneLenght[1] = '7';
            for (int i = 2; i < numberOfDigits; i++)
            {
                if (i == 2)
                {
                    phoneLenght[i] = random.Next(4).ToString()[0];
                    continue;
                }

                phoneLenght[i] = random.Next(10).ToString()[0];
            }

            return new string(phoneLenght);
        }

        public static string GetRandomEmail(int numberOfLetters)
        {
            List<string> EmailExtensions = new List<string>()
            {
                "@gmail.com","@yahoo.com","@outlook.com","@yandex.com","@aol.com","@zoho.com","@mail.com","@tutanota.com"
            };

            byte[] allLetters = new byte[numberOfLetters];

            for (int letter = 0; letter < numberOfLetters; letter++)
            {
                if (letter == 0)
                {
                    allLetters[letter] = (byte)random.Next(65, 91);
                    continue;
                }
                allLetters[letter] = (byte)random.Next(97, 123);
            }

            return Encoding.ASCII.GetString(allLetters) + EmailExtensions[random.Next(EmailExtensions.Count)];
        }

        public static string GetRandomDogName()
        {
            List<string> dogNames = new List<string>()
            {
                "Rufus","Ella","Luke","Dixie","Kona","Nala","Penelope","Jasmine","Lola",
                "Cisco","Maddie","Princess","Blue","Payton","Scooter","Marley","Buddy",
                "Gizmo","Brutus","Moose","Tiger","Teddy","Rex","Cocoa","Lacey","Coco",
                "Rocky","Hank","Rosie","Chance","Bubba","Benji","Ace","Zara","Scout",
                "Gigi","Tucker","Baxter","Gunner","Bruno"
            };

            string dogRandomName = dogNames[random.Next(dogNames.Count)];

            return dogRandomName;
        }

        public static Size GetRandomDogSize()
        {
            return (Size)random.Next(1, 4);
        }

        public static string GetRandomDogDescription()
        {
            List<string> firstWord = new List<string>()
            {
                "active","adorable","adventurous","amazing","bright","charming","cheerful",
                "clever","delightful","energetic","intelligent","interesting","loving",
                "playful","positive","sensitive","friendly"
            };

            string description = " is " + firstWord[random.Next(firstWord.Count)] + " dog. "
                + "Perfect guest for your home.";

            return description;
        }

        public static byte[] GetRandomDogPhoto()
        {
            var currentDirName = @"C:\Users\karco\Desktop\DogPhotos";
            var aveablePhotos = Directory.GetFiles(currentDirName);

            var selectetPhoto = aveablePhotos[random.Next(aveablePhotos.Length)];

            //FileInfo f1 = new FileInfo("~/ImageUploads/" + dogOwnerName + dogName + ".jpg");
            //string destFile = Path.Combine(HttpContext.Current.Server.MapPath("~/ImageUploads/"), dogOwnerName + dogName + ".jpg");
            //File.Copy(selectetPhoto, destFile, true);

            return File.ReadAllBytes(selectetPhoto);
        }
    }

    internal class RandomRequestMessage
    {
        private LendADogDemoDb _context;

        internal RandomRequestMessage(LendADogDemoDb context)
        {
            _context = context;
        }

        internal void EditMessages()
        {
            List<DogOwner> allOwners = _context.DogOwners.ToList();
            foreach (DogOwner sender in allOwners)
            {
                List<DogOwner> randomOwners = _context.DogOwners
                    .Where(o => o.Id != sender.Id)
                    .OrderBy(x => Guid.NewGuid())
                    .Take(5).ToList();

                foreach (DogOwner receiver in randomOwners)
                {
                    RequestMessage msg = new RequestMessage()
                    {
                        SendFromID = sender.Id,
                        ReceiverID = receiver.Id,
                        Message = "Dear " + receiver.FullName + " i like to be part of LendADog community."
                    };
                    _context.RequestMessages.AddOrUpdate(m => m.RequestMessID, msg);
                    _context.SaveChanges();
                }
            }
        }
    }
}
