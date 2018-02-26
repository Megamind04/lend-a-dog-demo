using LendADogDemo.Entities.DataContexts;
using LendADogDemo.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Transactions;
using System.Drawing;

namespace LendADogDemo.Entities.Helpers
{
    internal static class RandomGenerator
    {
        #region Initialize

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

        #endregion

        internal static void RandomDogOwners(this IdentityDb context, int numberOfDogOwners, int emailLength, int phoneLength)
        {
            #region Helpers

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

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                using (context = new IdentityDb())
                {
                    var haser = new PasswordHasher();
                    for (int i = 0; i < numberOfDogOwners; i++)
                    {
                        string userEmail = RandomEmail(emailLength);
                        string password = "Password*1";
                        string fullName = RandomFullName();

                        var user = new ApplicationUser()
                        {
                            FirstName = fullName.Split(null)[0],
                            LastName = fullName.Split(null)[1],
                            PhoneNumber = RandomPhoneNumber(9),
                            UserName = userEmail,
                            Email = userEmail,
                            IsConfirmed = random.NextDouble() > 0.5,
                            LockoutEnabled = true,
                            PasswordHash = haser.HashPassword(password)
                        };

                        context.Users.Add(user);

                        if (i % 100 == 0)
                        {
                            context.SaveChanges();
                            context.Dispose();
                            context = new IdentityDb();
                        }
                    }
                    context.SaveChanges();
                }
                scope.Complete();
            }
        }

        internal static void RandomDogsPerDogOwner(this LendADogDemoDb context, int dogsPerOwner, int photosPerDog)
        {
            #region Helpers
            List<DogOwner> dogOwners = context.DogOwners.ToList();

            var currentDirName = @"C:\Users\karco\Desktop\DogPhotos";

            var aveablePhotos = Directory.GetFiles(currentDirName);

            byte[][] ImagesBytes = new byte[aveablePhotos.Length][];

            for (int i = 0; i < aveablePhotos.Length; i++)
            {
                using (Image img = Image.FromFile(aveablePhotos[i]))
                {
                    var imgBiteNew = img.ResizeImageConvertInBytes(360, 270);
                    ImagesBytes[i] = imgBiteNew;
                }
            }

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

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                using (context = new LendADogDemoDb())
                {
                    int a = 0;
                    foreach (DogOwner user in dogOwners)
                    {
                        a++;
                        for (int i = 0; i < dogsPerOwner; i++)
                        {
                            Dog newDog = new Dog()
                            {
                                DogName = dogNames[random.Next(dogNames.Length)],
                                DogSize = (Size)random.Next(1, 4),
                                DogOwnerID = user.Id,
                                Description = $" is {firstWord[random.Next(firstWord.Length)]} dog. Perfect guest for your home.",
                                DogPhotos = new List<DogPhoto>()
                            };

                            for (int b = 0; b < photosPerDog; b++)
                            {
                                DogPhoto newDogPhoto = new DogPhoto()
                                {
                                    DogID = newDog.DogID,                                    
                                    Photo = ImagesBytes[random.Next(ImagesBytes.Length)]
                                };
                                newDog.DogPhotos.Add(newDogPhoto);
                            }

                            context.Dogs.Add(newDog);
                        }
                        if (a % 10 == 0)
                        {
                            context.SaveChanges();
                            context.Dispose();
                            context = new LendADogDemoDb();
                        }
                    }
                    context.SaveChanges();
                }
                scope.Complete();
            }
        }

        internal static void RandomRequestMessages(this LendADogDemoDb context, int numberOfRecords)
        {
            List<DogOwner> allOwners = context.DogOwners.ToList();

            using (TransactionScope scope = new TransactionScope())
            {
                using (context = new LendADogDemoDb())
                {
                    int a = 0;
                    foreach (DogOwner sender in allOwners)
                    {
                        a++;
                        var randomOwners = context.DogOwners
                            .SqlQuery("SELECT TOP(@numberOfRecords) * FROM AspNetUsers WHERE NOT Id = @Id ORDER BY NEWID();"
                            , new SqlParameter("Id", sender.Id)
                            , new SqlParameter("numberOfRecords", numberOfRecords)).ToList();

                        foreach (DogOwner receiver in randomOwners)
                        {
                            RequestMessage msg = new RequestMessage()
                            {
                                SendFromID = sender.Id,
                                ReciverID = receiver.Id,
                                Message = "Dear " + receiver.FullName + " i like to be part of LendADog community.",
                                CreateDate = GetRandomDate()
                            };
                            context.RequestMessages.Add(msg);
                        }
                        if (a % 10 == 0)
                        {
                            context.SaveChanges();
                            context.Dispose();
                            context = new LendADogDemoDb();
                        }
                    }
                    context.SaveChanges();
                }
                scope.Complete();
            }
        }

        internal static void RandomMainBoardMessages(this LendADogDemoDb context, int messagesPerNumberOfDogs)
        {
            #region Helpers

            List<DogOwner> allOwners = context.DogOwners.Include(d => d.Dogs).ToList();

            string[] numbersToLetters = { "two", "tre", "four", "five", "six", "seven", "eight", "nine" };

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                using (context = new LendADogDemoDb())
                {
                    int a = 0;
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
                                    Answered = random.NextDouble() > 0.5,
                                    DogID = dog.DogID,
                                };
                                context.MainMessages.Add(msg);
                            }
                        }
                        if (a % 10 == 0)
                        {
                            context.SaveChanges();
                            context.Dispose();
                            context = new LendADogDemoDb();
                        }
                    }
                    context.SaveChanges();
                }
                scope.Complete();
            }

        }

        internal static void RandomPrivateBoardMessages(this LendADogDemoDb context, int numberOfFriends, int messagesPerFriend)
        {
            List<DogOwner> dogOwners = context.DogOwners.ToList();

            using (TransactionScope scope = new TransactionScope())
            {
                using (context = new LendADogDemoDb())
                {
                    int a = 0;
                    foreach (DogOwner sender in dogOwners)
                    {
                        a++;
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
                                    SendFromID = sender.Id,
                                    RrecivedFromID = receiver.Id,
                                    CreateDate = GetRandomDate(),
                                    Message = "Hi " + receiver.FirstName
                                };
                                context.PrivateMessages.Add(sendMsg);

                                PrivateMessageBoard RecMmsg = new PrivateMessageBoard()
                                {
                                    SendFromID = receiver.Id,
                                    RrecivedFromID = sender.Id,
                                    CreateDate = GetRandomDate(),
                                    Message = "Hi " + sender.FirstName
                                };
                                context.PrivateMessages.Add(RecMmsg);
                            }
                        }
                        if (a % 10 == 0)
                        {
                            context.SaveChanges();
                            context.Dispose();
                            context = new LendADogDemoDb();
                        }
                    }
                    context.SaveChanges();
                }
                scope.Complete();
            }

        }
    }
}
