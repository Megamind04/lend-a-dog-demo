using LendADogDemo.Entities.DataContexts;
using LendADogDemo.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace LendADogDemo.Entities.Helpers
{
    internal static class RandomGenerator
    {
        private static readonly Random random;
        private static readonly DateTime start;

        static RandomGenerator()
        {
            random = new Random();
            start = new DateTime(2018, 1, 1);
        }

        private static DateTime GetRandomDate()
        {
            DateTime randomDate = start
                        .AddDays(random.Next(1, 32))
                        .AddHours(random.Next(1, 25))
                        .AddMinutes(random.Next(1, 61))
                        .AddSeconds(random.Next(1, 61));
            return randomDate;
        }

        internal static void RandomDogOwners(this IdentityDb context, int numberOfDogOwners, int emailLength,int phoneLength)
        {
            string[] EmailExtensions = new string[]
            {
                "@gmail.com","@yahoo.com","@outlook.com","@yandex.com","@aol.com","@zoho.com","@mail.com","@tutanota.com"
            };

            string[] listFullNames = new string[]
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

            string RandomEmail(int numberOfLetters)
            {
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

                return Encoding.ASCII.GetString(allLetters) + EmailExtensions[random.Next(EmailExtensions.Length)];
            }

            string RandomPhoneNumber(int numberOfDigits)
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

            string RandomFullName()
            {
                return listFullNames[random.Next(listFullNames.Length)];
            }

            for (int i = 0; i < numberOfDogOwners; i++)
            {
                string userEmail = RandomEmail(emailLength);
                string password = "Password*1";
                string fullName = RandomFullName();

                if (!context.Users.Any(u => u.UserName == userEmail))
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var menager = new UserManager<ApplicationUser>(store);
                    var user = new ApplicationUser()
                    {
                        FirstName = fullName.Split(null)[0],
                        LastName = fullName.Split(null)[1],
                        PhoneNumber = RandomPhoneNumber(9),
                        UserName = userEmail,
                        Email = userEmail,
                        IsConfirmed = random.NextDouble() > 0.5,
                        LockoutEnabled = true
                    };
                    menager.Create(user, password);
                }
            }
        }

        internal static void RandomDogsPerDogOwner(this LendADogDemoDb context, int dogsPerOwner, int photosPerDog)
        {
            List<DogOwner> dogOwners = context.DogOwners.ToList();
            var currentDirName = @"C:\Users\karco\Desktop\DogPhotos";
            var aveablePhotos = Directory.GetFiles(currentDirName);

            string[] dogNames = new string[]
            {
                "Rufus","Ella","Luke","Dixie","Kona","Nala","Penelope","Jasmine","Lola",
                "Cisco","Maddie","Princess","Blue","Payton","Scooter","Marley","Buddy",
                "Gizmo","Brutus","Moose","Tiger","Teddy","Rex","Cocoa","Lacey","Coco",
                "Rocky","Hank","Rosie","Chance","Bubba","Benji","Ace","Zara","Scout",
                "Gigi","Tucker","Baxter","Gunner","Bruno"
            };

            string[] firstWord = new string[]
            {
                "active","adorable","adventurous","amazing","bright","charming","cheerful",
                "clever","delightful","energetic","intelligent","interesting","loving",
                "playful","positive","sensitive","friendly"
            };

            foreach (DogOwner user in dogOwners)
            {
                for (int i = 0; i < dogsPerOwner; i++)
                {
                    Dog newDog = new Dog()
                    {
                        DogName = dogNames[random.Next(dogNames.Length)],
                        DogSize = (Size)random.Next(1, 4),
                        DogOwnerID = user.Id,
                        Description = $" is {firstWord[random.Next(firstWord.Length)]} dog. Perfect guest for your home."
                    };
                    context.Dogs.Add(newDog);
                    context.SaveChanges();
                    var dogId = newDog.DogID;

                    for (int b = 0; b < photosPerDog; b++)
                    {
                        DogPhoto newDogPhoto = new DogPhoto()
                        {
                            DogID = dogId,
                            Photo = File.ReadAllBytes(aveablePhotos[random.Next(aveablePhotos.Length)])
                        };
                        context.DogPhotos.Add(newDogPhoto);
                        context.SaveChanges();
                    }
                }
            }
        }

        internal static void RandomRequestMessages(this LendADogDemoDb context, int numberOfRecords)
        {
            List<DogOwner> allOwners = context.DogOwners.ToList();

            foreach (DogOwner sender in allOwners)
            {
                var randomOwners = context.DogOwners
                    .SqlQuery("SELECT TOP(@numberOfRecords) * FROM AspNetUsers WHERE NOT Id = @Id ORDER BY NEWID();"
                    , new SqlParameter("Id", sender.Id)
                    , new SqlParameter("numberOfRecords", numberOfRecords)).ToList();

                foreach (DogOwner receiver in randomOwners)
                {
                    RequestMessage msg = new RequestMessage()
                    {
                        SendFromID = sender.Id,
                        ReceiverID = receiver.Id,
                        Message = "Dear " + receiver.FullName + " i like to be part of LendADog community.",
                        CreateDate = GetRandomDate()
                    };
                    context.RequestMessages.Add(msg);
                    context.SaveChanges();
                }
            }
        }

        internal static void RandomMainBoardMessages(this LendADogDemoDb context, int messagesPerNumberOfDogs)
        {
            List<DogOwner> allOwners = context.DogOwners.Include(d => d.Dogs).ToList();
            string[] numbersToLetters = { "two", "tre", "four", "five", "six", "seven", "eight", "nine" };

            foreach (DogOwner sender in allOwners)
            {
                if (sender.Dogs != null)
                {
                    var randomDogs = context.Dogs
                    .SqlQuery("SELECT TOP(@numberOfRecords) * FROM Dog ORDER BY NEWID();"
                    , new SqlParameter("numberOfRecords", messagesPerNumberOfDogs)).ToList();

                    foreach (Dog dog in randomDogs)
                    {
                        MainMessageBoard msg = new MainMessageBoard()
                        {
                            DogOwnerID = sender.Id,
                            RequestMessage = "Need Someone to Take Care of My Dog(" + dog.DogName + ") for " + numbersToLetters[random.Next(numbersToLetters.Length)] + " days",
                            CreateDate = GetRandomDate(),
                            Answered = random.NextDouble() > 0.5
                        };
                        context.MainMessages.Add(msg);
                        context.SaveChanges();
                    }
                }
            }
        }

        internal static void RandomPrivateBoardMessages(this LendADogDemoDb context, int numberOfFriends, int messagesPerFriend)
        {
            List<DogOwner> dogOwners = context.DogOwners.ToList();

            foreach (DogOwner sender in dogOwners)
            {
                var randomFriends = context.DogOwners
                    .SqlQuery("SELECT TOP(@numberOfRecords) * FROM AspNetUsers WHERE NOT Id = @Id ORDER BY NEWID();"
                    , new SqlParameter("Id", sender.Id)
                    , new SqlParameter("numberOfRecords", numberOfFriends)).ToList();
                foreach (DogOwner receiver in randomFriends)
                {
                    for (int i = 0; i < messagesPerFriend; i++)
                    {
                        PrivateMessageBoard sendMsg = new PrivateMessageBoard()
                        {
                            SenderID = sender.Id,
                            ReceiverID = receiver.Id,
                            CreateDate = GetRandomDate(),
                            Message = "Hi " + receiver.FirstName
                        };
                        context.PrivateMessages.Add(sendMsg);

                        PrivateMessageBoard RecMmsg = new PrivateMessageBoard()
                        {
                            SenderID = receiver.Id,
                            ReceiverID = sender.Id,
                            CreateDate = GetRandomDate(),
                            Message = "Hi " + sender.FirstName
                        };
                        context.PrivateMessages.Add(RecMmsg);
                        context.SaveChanges();
                    }
                    
                }
            }
        }
    }
}
